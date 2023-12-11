using System.Collections.Generic;
namespace Interface
{
    using Models;
    public interface InterfaceUser
    {
        List<User> GetAll();
        User Get(long id);
        void Post(User t);
        void Delete(int id);

    }
}
