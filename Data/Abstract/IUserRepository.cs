using FitGain.Entity;

namespace FitGain.Data.Abstract;

public interface IUserRepository
{
    IQueryable<User> Users { get; }
    void CreateUser(User user);
}