using System.Collections.Generic;
using Task = Models.Task;

namespace   Interface
{
    public interface InterfaceTask{

        List<Task> Get(long userId);
        Task GetById(long userId,int id);
        void Add(long userId,Task task);
        void Delete(long userId,int id);
        void Update(long userId,Task task);
        int Count();
    }
}