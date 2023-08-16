using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;

namespace WebEng.Properties.Repositories;

public class PropertyRepository : IRepository<Models.Property, string>{
    protected DatabaseContext _context;

    public PropertyRepository(DatabaseContext context){
        _context = context;
    }

    public IQueryable<Models.Property> SimpleCollection => _context.Properties;
    public IQueryable<Models.Property> FullCollection => _context.Properties.Include(m=>m.User);

    public async Task<bool> CreateAsync(Models.Property property){
        _context.Properties.Add(property);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Models.Property property){
        _context.Properties.Remove(property);
        await _context.SaveChangesAsync();
    }

    public async Task <bool> UpdateAsync (Models.Property property){
        _context.Update(property);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Models.Property?> FindAsync(string id) => await FullCollection.FirstOrDefaultAsync(m=>m.Id==id);
}