
using System;
using System.Linq;
using System.Buffers;
using System.Buffers.Text;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace WebEng.Properties.Models;

// This class is just written to import the movie data from the json file
// into the database. It first parses the JSON into a list of objects (of
// type JsonMovieModel) and then adds each of these items to the Database.
// It is painfully slow.
public class DatabaseSeeder
{
    // A custom JsonConverter that is able to properly parse the "number of votes"
    // field in the JSON data that has a pesky thousands separator and is formatted
    // as a string. By default, .NET can parse numbers from strings, and can parse
    // thousands separators, but not both simultaneously.

    private class JsonPropModel
    {
        public string? externalId {get;set;}
        public string? title { get; set; }
        public int? areaSqm {get;set;}
        public string? propertyType { get; set; }
        public string city {get;set;} = "";
        public string? isRoomActive {get;set;}
        public string? postalCode {get;set;}
        public string? latitude {get;set;}
        public string? longitude {get;set;}
        public int? rent {get;set;}
        public int? deposit {get;set;}
        public int? additionalCosts {get;set;}

/*PlaceInfo*/
        public string? rentDetails{get;set;}
        public string? smoking {get;set;}
        public string? pets {get;set;}
        public string? roommates{get;set;}
        public string? toilet {get;set;}
        public string? gender{get;set;}

/*MatchTenant*/
        public string? matchAge{get;set;}
        public string? matchCapacity {get;set;}
        public string? matchGender{get;set;}
        public string? matchLanguages {get;set;}
        public string? matchStatus {get;set;}

/*User*/        
        public int userId {get;set;}
        public string? userDisplayName {get;set;}
        public string? userLastLoggedOn {get;set;}
        public string? userMemberSince{get;set;}
    }

    private class StatsModel{
        public string City{get;set;} ="";
        public List<int?> Rents{get;set;} = new List<int?>();
        public List<int?> Deposits{get;set;}= new List<int?>();
    }

    static double sd(int[] arr){
        double average = arr.Average();
        double sum = 0;
        foreach (var item in arr){
            double temp = item-average;
            temp = temp*temp;
            sum= sum+temp;
        }
        double result = Math.Sqrt((sum) / (arr.Count()));
        return result;
    }

    static double medianArray(int[] arr){
        int len = arr.Count();
        if (len%2==0) {
            int a = arr[len/2];
            int b = arr[(len/2)-1];
            return (a+b)/2;
        }
        return arr[len/2];
    }
    public static async Task InitializeAsync(DatabaseContext context){
        await context.Database.EnsureCreatedAsync();
        /*if (await context.Properties.AnyAsync())
        {   
            Console.WriteLine("prop exsist");
            return;
        }*/
        /*var dataFile = File.OpenRead("../data/properties.json");
        //THE ORIGINAL PARSER CODE, can't parse our jason file
        var results = await JsonSerializer.DeserializeAsync<List<JsonPropModel>>(dataFile, new JsonSerializerOptions
        {
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        });*/

        //WORK ON PROCESS PARSER CODE, copied from stack overflow but installation of Newtonsoft needed.
        List<JsonPropModel?> results = new List<JsonPropModel?>();
        String JSONtxt = File.ReadAllText("../data/properties.json");

        //Capture JSON string for each object, including curly brackets 
        Regex regex = new Regex(@".*(?<=\{)[^}]*(?=\}).*", RegexOptions.IgnoreCase);
        MatchCollection matches = regex.Matches(JSONtxt);
        Console.WriteLine("Parsing Json...");
        foreach(Match match in matches){
            string objStr = match.ToString();
            JsonPropModel? model = Newtonsoft.Json.JsonConvert.DeserializeObject<JsonPropModel>(objStr);
            results.Add(model);
        }

        var users = new List<User>();
        var stats = new List<StatsModel>();

        //Convert each property model into property class and record users
        Console.WriteLine("Parsing Properties...");
        foreach (var item in results){
            if(item==null)continue;
            if (item.title==null)continue;
            if (item.city==null)continue;
            if (item.userDisplayName==null)continue;
            if (item.externalId==null)continue;
            if(item.rent==null)continue;

            var u = users.FirstOrDefault(s=>s.Id==item.userId);
            if (u==null) {
                u = new User{
                    Id=item.userId,
                    DisplayName=item.userDisplayName,
                    MemberSince = item.userMemberSince,
                    LastLoggedOn = item.userLastLoggedOn,
                };
                users.Add(u);              
            }
            var stat = stats.FirstOrDefault(s=>s.City.ToLower()==item.city.ToLower());
            if (stat==null){
                var rentList = new List<int?>();
                rentList.Add(item.rent);
                var depositList = new List<int?>();
                var dep = (item.deposit == null) ? 0 : item.deposit;
                depositList.Add(dep);
                stat = new StatsModel{
                    City = item.city,
                    Rents = rentList,
                    Deposits = depositList,
                };
                
                stats.Add(stat);
            } else {
                stat.Rents.Add(item.rent);
                stat.Deposits.Add(item.deposit);
            }
            var property = new Property{
                Id = item.externalId,
                Title = item.title,
                PropertyType = item.propertyType,
                AreaSqm = item.areaSqm,
                CostSqm = (float) (item.rent)/(item.areaSqm),
                City = item.city,
                IsActive = item.isRoomActive,
                PostalCode = item.postalCode,
                Latitude = double.Parse(item.latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(item.longitude, CultureInfo.InvariantCulture),
                Rent = item.rent,
                Deposit =(item.deposit == null) ? 0 : item.deposit,
                AdditionalCosts = item.additionalCosts,
                PlaceInfo = new PlaceInfo{
                    Gender = item.gender,
                    Pets=item.pets,
                    Smoking=item.smoking,
                    RentDetails=item.rentDetails,
                    Toilet=item.toilet,
                    Roommates=item.roommates,
                },
                MatchTenant = new MatchTenant{
                    MatchAge = item.matchAge,
                    MatchCapacity = item.matchCapacity,
                    MatchGender = item.matchGender,
                    MatchLanguages = item.matchLanguages,
                    MatchStatus = item.matchStatus,
                },
                User = u,

            };

            context.Properties.Add(property);
        }
        //Convert statistics model into statistics class
        Console.WriteLine("Parsing Statistics...");
        foreach (var item in stats){
            if (item.City==null)continue;
            item.Rents.Sort();
            item.Deposits.Sort();
            List<int> listR = item.Rents.Where(x => x != null).Cast<int>().ToList();
            List<int> listD = item.Deposits.Where(x => x != null).Cast<int>().ToList();
            int[] r = listR.ToArray();
            int[] d = listD.ToArray();
            
            double ar, mr, sdr, ad, md, sdd;
            int val = r.Count();  
            if(val!=0){
                ar = r.Average();
                mr = medianArray(r);
                sdr = sd(r);
            }else{
                
                ar = 0;
                mr = 0;
                sdr = 0;
            }
            
            if(d.Count()!=0){
                ad = d.Average();
                md = medianArray(d);
                sdd = sd(d);
            }else{
                ad = 0;
                md = 0;
                sdd = 0;
            }
            var statistic = new Statistics{
                City=item.City,
                SumRent = r.Sum(),
                SumDeposit = d.Sum(),
                NumRent = r.Length,
                NumDeposit = d.Length,
                MeanRent = ar,
                MedianRent= mr,
                SdRent = sdr,
                MeanDeposit = ad,
                MedianDeposit = md,
                SdDeposit = sdd,
            };
            context.Statistics.Add(statistic);
        }
        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }
}