using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;
using WebEng.Properties.Repositories;

namespace WebEng.Properties.Controllers;

[ApiController]
[Route("[controller]")]

public class StatisticsController : AbstractController{
    private readonly StatisticsRepository _stats;

    public StatisticsController(StatisticsRepository stats){
        _stats = stats;
    }

    [HttpGet("/statistics")]
    public ActionResult<IEnumerable<ApiModels.Statistics>> GetListASync(
         [FromQuery] RequestModels.Filter filter
    ){ 
        if (!ModelState.IsValid)
            return BadRequest();
        var statistics = Models.IApiQuery<Statistics>.ApplyAll(_stats.FullCollection,filter);
        return Ok(statistics
                    .AsAsyncEnumerable()
                    .Select(ApiModels.Statistics.FromDatabase));
    }

    [HttpGet("/statistics/{city}")]
    public async Task<ActionResult<ApiModels.Statistics>> GetSingleAsync (string city)=> await _stats.FindAsync(city) switch{
        null => NotFound(),
        var stat => Ok(ApiModels.Statistics.FromDatabase(stat)),
    };

        public class RequestModels{

        public class Filter: Models.IApiQuery<Statistics>{
            public string? City { get; set; }

            public IQueryable<Statistics> Apply(IQueryable<Statistics> statistics)
            {
                if (City != null) statistics = statistics.Where(p => p.City == City);


                return statistics;
            }

        }
    }
    

}