using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;

namespace WebEng.Properties.Repositories;

public class StatisticsRepository : IRepository<Models.Statistics, string>{
     protected DatabaseContext _context;

    public StatisticsRepository(DatabaseContext context){
        _context = context;
    }

    public IQueryable<Models.Statistics> SimpleCollection => _context.Statistics;
    public IQueryable<Models.Statistics> FullCollection => _context.Statistics.Include(m=>m.Properties);

    public async Task<bool> CreateAsync(Models.Statistics stats){
        _context.Statistics.Add(stats);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Models.Statistics stats){
    }

    public async Task <bool> UpdateAsync (Models.Statistics stats){
        _context.Update(stats);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Models.Statistics?> FindAsync(string city){
    var found = await FullCollection.FirstOrDefaultAsync(m=>m.City==city);
    if (found == null)
    {
        return null; //this never gets fired when empty
    }
    return found;
    }
}