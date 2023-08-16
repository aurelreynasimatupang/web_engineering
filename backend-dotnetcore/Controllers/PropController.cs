using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;
using WebEng.Properties.Repositories;

namespace WebEng.Properties.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertyController : AbstractController{
    private readonly PropertyRepository _properties;
    private readonly UserRepository _users;
    private readonly StatisticsRepository _stats;
    
    public PropertyController(PropertyRepository properties, UserRepository users, StatisticsRepository stats){
        _properties = properties;
        _users = users;
        _stats = stats;
    }

    private static double medianArray(int[] arr){
        int len = arr.Count();
        if (len%2==0) {
            int a = arr[len/2];
            int b = arr[(len/2)-1];
            return (a+b)/2;
        }
        return arr[len/2];
    }

    [HttpGet("/")]
    public ActionResult<IEnumerable<ApiModels.Property>> GetListASync(
        [FromQuery] RequestModels.Order order, // we support sorting
        [FromQuery] RequestModels.Filter filter // and filtering
    ){
        if (!ModelState.IsValid)
            return BadRequest();
        var properties = Models.IApiQuery<Property>.ApplyAll(_properties.FullCollection, order,filter);
        return Ok(properties
                    .AsAsyncEnumerable()
                    .Select(ApiModels.Property.FromDatabase));
    }

    [HttpDelete("/")]
    public async Task<ActionResult> DeletePropertyAsync(
         [FromQuery] RequestModels.Filter filter
         ){ 
             List<Property> properties = Models.IApiQuery<Property>.ApplyAll(_properties.FullCollection, filter).ToList();

        var statis = await _stats.FindAsync(properties[0].City);
        var num = 0;
        //edit sum of rent and deposit
        foreach(Property prop in properties) {
                statis.SumRent = statis.SumRent-prop.Rent;
                statis.SumDeposit = statis.SumDeposit-prop.Deposit;
                num++;
                await _properties.DeleteAsync(prop);
        }

        //edit num
        statis.NumRent = statis.Properties.Count()-num;
        if(statis.NumRent == 0) {
                statis.SumRent = 0;
                statis.SumDeposit = 0;
                statis.NumDeposit = 0;
                statis.MeanRent = 0;
                statis.MeanDeposit = 0;
                statis.SdDeposit = 0;
                statis.MedianDeposit = 0;
                statis.SdRent = 0;
        } else {
        statis.NumDeposit = statis.NumRent;

         //finding mean
        statis.MeanRent = statis.SumRent/statis.NumRent;
        statis.MeanDeposit = statis.SumDeposit/statis.NumDeposit;

        //finding sd
        var summRent = 0.00;
        var temp = 0.00;
        foreach ( Property item in statis.Properties){
            temp = (double)(item.Rent-statis.MeanRent);
            summRent = summRent + Math.Pow(temp, 2);
        }
        var tempNum = 0;
        if(summRent!=null) temp=summRent;
        if(statis.NumRent != null) tempNum =(int) statis.NumRent;
        statis.SdRent = (double)(Math.Sqrt(temp/tempNum));
        var summDepo = 0.00;
        foreach ( Property item in statis.Properties){
            temp = (double)(item.Deposit-statis.MeanDeposit);
            summDepo = summDepo + Math.Pow(temp, 2);
        }
        if(summRent!=null) temp=summDepo;
        if(statis.NumRent != null) tempNum =(int) statis.NumDeposit;
        statis.SdDeposit = Math.Sqrt(temp/tempNum);
        var index = (int) (statis.NumRent);
        List<Property> props = statis.Properties.OrderBy(o=>o.Rent).ToList();  
        List<Property> propies = statis.Properties.OrderBy(o=>o.Deposit).ToList();
        if(index % 2 == 0){
            var first = (int)(props[(index/2)].Rent);
            var second = (int)(props[(index/2)-1].Rent);
            statis.MedianRent = (first+second)/2;
            index =(int) statis.NumDeposit;
            first = (int)(propies[(index/2)].Deposit);
            second = (int)(propies[(index/2)-1].Deposit);
            statis.MedianDeposit = (first+second)/2;
        } else {
            var only = (int)(props[((index-1)/2)].Rent);
            statis.MedianRent = only;
            only = (int)(propies[((index-1)/2)].Deposit);
            statis.MedianDeposit = only;
        }
        }
        await _stats.UpdateAsync(statis);

        return Ok();
    }

    [HttpPut("/")]
    public async Task<ActionResult<ApiModels.Property>> UpdateMultiPropertyAsync(
        [FromBody] ApiModels.Property apiProp, [FromQuery] RequestModels.Filter filter
        ){ List<Property> properties = Models.IApiQuery<Property>.ApplyAll(_properties.FullCollection, filter).ToList();
        // We verify that the provided movie is valid
        if (!ModelState.IsValid)
            return BadRequest();

        // We find the existing prop
        var statis = await _stats.FindAsync(apiProp.City);
        foreach(Property prop in properties) {
            Console.WriteLine("Updating");
            // and update it
            var oldRent = prop.Rent;
            var oldDeposit = prop.Deposit;

            prop.Title = apiProp.Title;
            prop.PropertyType = apiProp.PropertyType;
            prop.AreaSqm = apiProp.AreaSqm;
            prop.CostSqm = apiProp.CostSqm;
            prop.PostalCode = apiProp.PostalCode;
            prop.Latitude = apiProp.Latitude;
            prop.Longitude = apiProp.Longitude;
            prop.City = apiProp.City;
            prop.Rent = apiProp.Rent;
            prop.IsActive = apiProp.IsActive;
            prop.Deposit = apiProp.Deposit;
            prop.AdditionalCosts = apiProp.AdditionalCosts;

            prop.PlaceInfo.RentDetails = apiProp.PlaceInfo.RentDetails;
            prop.PlaceInfo.Smoking = apiProp.PlaceInfo.Smoking;
            prop.PlaceInfo.Pets = apiProp.PlaceInfo.Pets;
            prop.PlaceInfo.Roommates = apiProp.PlaceInfo.Roommates;
            prop.PlaceInfo.Toilet = apiProp.PlaceInfo.Toilet;
            prop.PlaceInfo.Gender = apiProp.PlaceInfo.Gender;

            prop.MatchTenant.MatchAge = apiProp.MatchTenant.MatchAge;
            prop.MatchTenant.MatchCapacity = apiProp.MatchTenant.MatchCapacity;
            prop.MatchTenant.MatchGender = apiProp.MatchTenant.MatchGender;
            prop.MatchTenant.MatchLanguages = apiProp.MatchTenant.MatchLanguages;
            prop.MatchTenant.MatchStatus = apiProp.MatchTenant.MatchStatus;
            await _properties.UpdateAsync(prop);

            //update statistics sum and num
            statis.SumRent = statis.SumRent-oldRent+apiProp.Rent;
            statis.SumDeposit = statis.SumDeposit-oldDeposit+apiProp.Deposit;
        }

            //update mean
            statis.MeanRent = statis.SumRent/statis.NumRent;
            statis.MeanDeposit = statis.SumDeposit/statis.NumDeposit;

            //update sd
            var summRent = 0.00;
            var temp =0.00;
            foreach ( Property item in statis.Properties){
                if(item.Rent != null){ 
                    temp = (double)(item.Rent-statis.MeanRent);
                    summRent =(double)(summRent + Math.Pow(temp, 2));
                }
            }
            temp =0.00;
            var tempNum = 0;
            if(summRent!=null) temp=summRent;
            if(statis.NumRent != null) tempNum =(int) statis.NumRent;
            statis.SdRent = (double)(Math.Sqrt(temp/tempNum));
            var summDepo = 0.00;
            foreach ( Property item in statis.Properties){
                if(item.Deposit != null){
                    temp = (double)(item.Deposit-statis.MeanDeposit);
                    summDepo = (double)(summDepo + Math.Pow(temp, 2));
                }
            }
            if(statis.NumDeposit != null) {
                tempNum = (int)statis.NumDeposit;
                statis.SdDeposit = Math.Sqrt(summDepo/tempNum);
            }

            var index = (int) (statis.NumRent);
            List<Property> props = statis.Properties.OrderBy(o=>o.Rent).ToList();  
        List<Property> propies = statis.Properties.OrderBy(o=>o.Deposit).ToList();
        if(index % 2 == 0){
            var first = (int)(props[(index/2)].Rent);
            var second = (int)(props[(index/2)-1].Rent);
            statis.MedianRent = (first+second)/2;
            index = (int)statis.NumDeposit;
            first = (int)(propies[(index/2)].Deposit);
            second = (int)(propies[(index/2)-1].Deposit);
            statis.MedianDeposit = (first+second)/2;
        } else {
            var only = (int)(props[((index-1)/2)].Rent);
            statis.MedianRent = only;
            only = (int)(propies[((index-1)/2)].Deposit);
            statis.MedianDeposit = only;
        }

        await _stats.UpdateAsync(statis);
        return Ok();
    }
 

    [HttpPost("/")]
    public async Task<ActionResult<string>> CreateAsync([FromBody] ApiModels.Property apiProp)
    {
        if (!ModelState.IsValid)
            return BadRequest();  
        var prop = new Property
        {
            Id = apiProp.Id,
            Title = apiProp.Title,
            PropertyType = apiProp.PropertyType,
            AreaSqm = apiProp.AreaSqm,
            CostSqm = apiProp.CostSqm,
            PostalCode = apiProp.PostalCode,
            Latitude = apiProp.Latitude,
            Longitude = apiProp.Longitude,
            City = apiProp.City,
            Rent = apiProp.Rent,
            
            IsActive = apiProp.IsActive,
            Deposit = apiProp.Deposit,
            AdditionalCosts = apiProp.AdditionalCosts,

            PlaceInfo = new PlaceInfo{
                RentDetails = apiProp.PlaceInfo.RentDetails,
                Smoking = apiProp.PlaceInfo.Smoking,
                Pets = apiProp.PlaceInfo.Pets,
                Roommates = apiProp.PlaceInfo.Roommates,
                Toilet = apiProp.PlaceInfo.Toilet,
                Gender = apiProp.PlaceInfo.Gender
            },

            MatchTenant = new MatchTenant{
                MatchAge = apiProp.MatchTenant.MatchAge,
                MatchCapacity = apiProp.MatchTenant.MatchCapacity,
                MatchGender = apiProp.MatchTenant.MatchGender,
                MatchLanguages = apiProp.MatchTenant.MatchLanguages,
                MatchStatus = apiProp.MatchTenant.MatchStatus,
            },


        };
        prop.User = await _users.FindAsync(apiProp.User.Id);
        if(prop.User == null){
            var u = new User{
                    Id=apiProp.User.Id,
                    DisplayName=apiProp.User.DisplayName,
                    MemberSince = "0",
                    LastLoggedOn = "0",
                };
            var adduser = await _users.CreateAsync(u);
            prop.User = u;
        }

        var statis = await _stats.FindAsync(apiProp.City);
        if(statis == null){
            var statistic = new Statistics{
                City=apiProp.City,
                SumRent = apiProp.Rent,
                SumDeposit = apiProp.Deposit,
                NumRent = 1,
                NumDeposit = 1,
                MeanRent = apiProp.Rent,
                MedianRent= apiProp.Rent,
                SdRent = 0,
                MeanDeposit = apiProp.Deposit,
                MedianDeposit = apiProp.Deposit,
                SdDeposit = 0,
            };
            var addstat = await _stats.CreateAsync(statistic);
            var result = await _properties.CreateAsync(prop);
            if (!result)
            return Conflict();
        }else {
            var result = await _properties.CreateAsync(prop);
        if (!result)
            return Conflict();
        
        //edit statistics of that city (start with sum and num)
        statis.SumRent = statis.SumRent+apiProp.Rent;
        statis.NumRent = statis.NumRent + 1;
        statis.NumDeposit = statis.NumDeposit + 1;
        if(statis.SumDeposit != null) statis.SumDeposit = statis.SumDeposit+apiProp.Deposit;

        //edit mean
        statis.MeanRent = statis.SumRent/statis.NumRent;
        if(statis.MeanDeposit != null) statis.MeanDeposit = statis.SumDeposit/statis.NumDeposit;

        //edit sd
        var summRent = 0.00;
        var temp =0.00;
        foreach ( Property item in statis.Properties){
            if(item.Rent != null){ 
                temp = (double)(item.Rent-statis.MeanRent);
                summRent =(double)(summRent + Math.Pow(temp, 2));
            }
        }
        temp =0.00;
        var tempNum = 0;
        if(summRent!=null) temp=summRent;
        if(statis.NumRent != null) tempNum =(int) statis.NumRent;
        statis.SdRent = (double)(Math.Sqrt(temp/tempNum));
        var summDepo = 0.00;
        foreach ( Property item in statis.Properties){
            if(item.Deposit != null){
                temp = (double)(item.Deposit-statis.MeanDeposit);
                summDepo = (double)(summDepo + Math.Pow(temp, 2));
            }
        }
        if(statis.NumDeposit != null) {
            tempNum = (int)statis.NumDeposit;
        statis.SdDeposit = Math.Sqrt(summDepo/tempNum);
        }
        var index = (int) (statis.NumRent);
        List<Property> props = statis.Properties.OrderBy(o=>o.Rent).ToList();  
        List<Property> propies = statis.Properties.OrderBy(o=>o.Deposit).ToList();
        if(index % 2 == 0){
            var first = (int)(props[(index/2)].Rent);
            var second = (int)(props[(index/2)-1].Rent);
            statis.MedianRent = (first+second)/2;
            index =(int) statis.NumDeposit;
            first = (int)(propies[(index/2)].Deposit);
            second = (int)(propies[(index/2)-1].Deposit);
            statis.MedianDeposit = (first+second)/2;
        } else {
            var only = (int)(props[((index-1)/2)].Rent);
            statis.MedianRent = only;
            only = (int)(propies[((index-1)/2)].Deposit);
            statis.MedianDeposit = only;
        }

        await _stats.UpdateAsync(statis);
        }
        
        
        return Ok(prop.Id);
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<ApiModels.Property>> GetSingleAsync (string id)=> await _properties.FindAsync(id) switch{
        null => NotFound(),
        var property => Ok(ApiModels.Property.FromDatabase(property)),
    };


    [HttpPut("/{id}")]
    public async Task<ActionResult<ApiModels.Property>> UpdatePropertyAsync(string id, [FromBody] ApiModels.Property apiProp)
    {
        // We verify that the provided movie is valid
        if (!ModelState.IsValid)
            return BadRequest();

        // We find the existing prop
        var prop = await _properties.FindAsync(id);
        var oldRent = prop.Rent;
        var oldDeposit = prop.Deposit;
        if (prop == null)
            return NotFound();

        // and update it
        prop.Title = apiProp.Title;
        prop.PropertyType = apiProp.PropertyType;
        prop.AreaSqm = apiProp.AreaSqm;
        prop.CostSqm = apiProp.CostSqm;
        prop.PostalCode = apiProp.PostalCode;
        prop.Latitude = apiProp.Latitude;
        prop.Longitude = apiProp.Longitude;
        prop.City = apiProp.City;
        prop.Rent = apiProp.Rent;
        prop.IsActive = apiProp.IsActive;
        prop.Deposit = apiProp.Deposit;
        prop.AdditionalCosts = apiProp.AdditionalCosts;

        prop.PlaceInfo.RentDetails = apiProp.PlaceInfo.RentDetails;
        prop.PlaceInfo.Smoking = apiProp.PlaceInfo.Smoking;
        prop.PlaceInfo.Pets = apiProp.PlaceInfo.Pets;
        prop.PlaceInfo.Roommates = apiProp.PlaceInfo.Roommates;
        prop.PlaceInfo.Toilet = apiProp.PlaceInfo.Toilet;
        prop.PlaceInfo.Gender = apiProp.PlaceInfo.Gender;

        prop.MatchTenant.MatchAge = apiProp.MatchTenant.MatchAge;
        prop.MatchTenant.MatchCapacity = apiProp.MatchTenant.MatchCapacity;
        prop.MatchTenant.MatchGender = apiProp.MatchTenant.MatchGender;
        prop.MatchTenant.MatchLanguages = apiProp.MatchTenant.MatchLanguages;
        prop.MatchTenant.MatchStatus = apiProp.MatchTenant.MatchStatus;

        prop.User.Id = apiProp.User.Id;
        prop.User.DisplayName = apiProp.User.DisplayName;
        await _properties.UpdateAsync(prop);

        // edit statistics
        var statis = await _stats.FindAsync(apiProp.City);
        statis.SumRent = statis.SumRent-oldRent+apiProp.Rent;
        if(statis.SumDeposit != null) statis.SumDeposit = statis.SumDeposit-oldDeposit+apiProp.Deposit;
        statis.MeanRent = statis.SumRent/statis.NumRent;
        if(statis.MeanDeposit != null) statis.MeanDeposit = statis.SumDeposit/statis.NumDeposit;
        var summRent = 0.00;
        var temp =0.00;
        foreach ( Property item in statis.Properties){
            if(item.Rent != null){ 
                temp = (double)(item.Rent-statis.MeanRent);
                summRent =(double)(summRent + Math.Pow(temp, 2));
            }
        }
        temp =0.00;
        var tempNum = 0;
        if(summRent!=null) temp=summRent;
        if(statis.NumRent != null) tempNum =(int) statis.NumRent;
        statis.SdRent = (double)(Math.Sqrt(temp/tempNum));
        var summDepo = 0.00;
        foreach ( Property item in statis.Properties){
            if(item.Deposit != null){
                temp = (double)(item.Deposit-statis.MeanDeposit);
                summDepo = (double)(summDepo + Math.Pow(temp, 2));
            }
        }
        if(statis.NumDeposit != null) {
            tempNum = (int)statis.NumDeposit;
        statis.SdDeposit = Math.Sqrt(summDepo/tempNum);
        }
        var index = (int) (statis.NumRent);
        List<Property> props = statis.Properties.OrderBy(o=>o.Rent).ToList();  
        List<Property> propies = statis.Properties.OrderBy(o=>o.Deposit).ToList();
        if(index % 2 == 0){
            var first = (int)(props[(index/2)].Rent);
            var second = (int)(props[(index/2)-1].Rent);
            statis.MedianRent = (first+second)/2;
            index = (int)statis.NumDeposit;
            first = (int)(propies[(index/2)].Deposit);
            second = (int)(propies[(index/2)-1].Deposit);
            statis.MedianDeposit = (first+second)/2;
        } else {
            var only = (int)(props[((index-1)/2)].Rent);
            statis.MedianRent = only;
            only = (int)(propies[((index-1)/2)].Deposit);
            statis.MedianDeposit = only;
        }
        await _stats.UpdateAsync(statis);
        //end of editing statistics

        return ApiModels.Property.FromDatabase(prop);
    }

    [HttpDelete("/{id}")]
    public async Task<ActionResult> DeletePropertyAsync(string id){
        var prop = await _properties.FindAsync(id);
        if(prop == null)
        {
            return NotFound();
            
        }else {
            var cit = prop.City;
            var ren = prop.Rent;
            var dep = prop.Deposit;
            await _properties.DeleteAsync(prop);
            Console.WriteLine("cit, ren, dep "+cit+" "+ren+" " +dep);
            var statis = await _stats.FindAsync(cit);

            statis.SumRent = statis.SumRent-ren;
            statis.SumDeposit -= dep;
            statis.NumRent -= 1 ;
            if(statis.NumRent == 0) {
                statis.SumRent = 0;
                statis.SumDeposit = 0;
                statis.NumDeposit = 0;
                statis.MeanRent = 0;
                statis.MeanDeposit = 0;
                statis.SdDeposit = 0;
                statis.MedianDeposit = 0;
                statis.SdRent = 0;
            } else {
            statis.NumDeposit -= 1;
            statis.MeanRent = statis.SumRent/statis.NumRent;
            statis.MeanDeposit = statis.SumDeposit/statis.NumDeposit;
            

            var summRent = 0.00;
            var temp =0.00;
            foreach ( Property item in statis.Properties){
                if(item.Rent != null){ 
                    temp = (double)(item.Rent-statis.MeanRent);
                    summRent =(double)(summRent + Math.Pow(temp, 2));
                }
            }
            temp =0.00;
            var tempNum = 0;
            if(summRent!=null) temp=summRent;
            if(statis.NumRent != null) tempNum =(int) statis.NumRent;
            statis.SdRent = (double)(Math.Sqrt(temp/tempNum));
            var summDepo = 0.00;
            foreach ( Property item in statis.Properties){
                if(item.Deposit != null){
                    temp = (double)(item.Deposit-statis.MeanDeposit);
                    summDepo = (double)(summDepo + Math.Pow(temp, 2));
                }
            }
            if(statis.NumDeposit != null) {
                tempNum = (int)statis.NumDeposit;
            statis.SdDeposit = Math.Sqrt(summDepo/tempNum);
            }
            var index = (int) (statis.NumRent);
        List<Property> props = statis.Properties.OrderBy(o=>o.Rent).ToList();  
        List<Property> propies = statis.Properties.OrderBy(o=>o.Deposit).ToList();
        if(index % 2 == 0){
            var first = (int)(props[(index/2)].Rent);
            var second = (int)(props[(index/2)-1].Rent);
            statis.MedianRent = (first+second)/2;
            index = (int)statis.NumDeposit;
            first = (int)(propies[(index/2)].Deposit);
            second = (int)(propies[(index/2)-1].Deposit);
            statis.MedianDeposit = (first+second)/2;
        } else {
            var only = (int)(props[((index-1)/2)].Rent);
            statis.MedianRent = only;
            only = (int)(propies[((index-1)/2)].Deposit);
            statis.MedianDeposit = only;
        }
        }
        await _stats.UpdateAsync(statis);
            return Ok();
            }
        
    }

    public class RequestModels{
        public class Order : Models.IApiQuery<Property>
        {
            [BindProperty(Name = "order-by")] // we indicate this is the name to find in the URL
            public Column? By { get; set; }
            [BindProperty(Name = "order-dir")]
            public Dir? Direction { get; set; }

            public enum Dir {Asc, Desc}
            public enum Column {CostSqm, Rent}

           public IQueryable<Property> Apply(IQueryable<Property> properties) =>
               (By, Direction) switch
               {
                    (Column.CostSqm, Dir.Asc) => properties.OrderBy(m => m.CostSqm),
                    (Column.CostSqm, Dir.Desc) => properties.OrderByDescending(m => m.CostSqm),
                    (Column.Rent, Dir.Asc) => properties.OrderBy(m => m.Rent),
                    (Column.Rent, Dir.Desc) => properties.OrderByDescending(m => m.Rent),
                    _ => properties,
                };

        }

        public class Filter: Models.IApiQuery<Property>{
            public string? City { get; set; }
            public int? Min {get;set;}
            public int? Max {get;set;}
            public double? Longitude { get; set; }
            public double? Latitude {get;set;}
            public string? IsActive {get;set;}
            public int? Limit {get;set;}

            public IQueryable<Property> Apply(IQueryable<Property> properties)
            {
                if (City != null) properties = properties.Where(p => p.City == City);
                if (Longitude != null) properties = properties.Where(p => p.Longitude == Longitude);
                if(Latitude!=null) properties = properties.Where(p=>p.Latitude==Latitude);
                if(IsActive !=null) properties = properties.Where(p=>p.IsActive==IsActive);
                if(Min !=null)properties = properties.Where(p=>p.Rent>Min);
                if(Max !=null)properties = properties.Where(p=>p.Rent<Max);
                if(Limit!=null)  properties = properties.Take(Limit.Value);
                return properties;
            }

        }

    }
}