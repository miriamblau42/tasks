using Jobs2.Modules;
using System.Collections.Generic;


namespace Jobs2.interfaces
{
    public interface IUserService
    {
        void Delete(int userId);
        void saveToFile();
        List<User> GetAll();
        User Get(int id);
        User GetUser(string name, string password);
        User postUser(User User);
        int Count { get; }
    }
}