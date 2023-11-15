using Models;
using System.Collections.Generic;
using Task = Models.Task;

namespace   Interface
{
    public interface InterfaceTask{

        List<Task> Get();
        Task GetById(int id);
        void Add(Task task);
        void Delete(int id);
        void Update(Task task);
        // int Count();
    }
}