using FitGain.Data.Abstract;
using FitGain.Data.Concrete.EfCore;
using FitGain.Entity;

namespace FitGain.Data.Concrete;

public class EfUserRepository : IUserRepository
{
    private readonly GymContext _context;
    public EfUserRepository(GymContext context)
    {
        _context = context;
    }

    public IQueryable<User> Users => _context.Users;

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }
}