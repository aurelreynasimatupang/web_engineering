using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;

namespace WebEng.Properties.Repositories;

public class UserRepository : IRepository<Models.User, int>{
    protected DatabaseContext _context;

    public UserRepository(DatabaseContext context){
        _context = context;
    }

    public IQueryable<Models.User> SimpleCollection => _context.Users;
    public IQueryable<Models.User> FullCollection => _context.Users.Include(m=>m.Properties);

    public async Task<bool> CreateAsync(Models.User user){
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteAsync(Models.User user){
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task <bool> UpdateAsync (Models.User user){
        _context.Update(user);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<Models.User?> FindAsync(int id) => await FullCollection.FirstOrDefaultAsync(m=>m.Id==id);
}