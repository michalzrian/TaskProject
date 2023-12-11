using Interface;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;



namespace services{
    using Models;
   public class UserServices:InterfaceUser
   {
       List<User> users{get;}
        private IWebHostEnvironment webHost;
        private string filePath;
        public UserServices(IWebHostEnvironment webHost )
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "User.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                users = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

        }
          private void changeInFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(users));
        }
         public List<User> GetAll() => users;
        public User Get(long Id) => users?.FirstOrDefault(t => t.UserId== Id);
         public void Post(User u)
        {
            u.UserId = users[users.Count()-1].UserId+1;
            u.Manager = false;
            users.Add(u);
            changeInFile();
        }
        public void Delete(int id)
        {
            var user = Get(id);
            if (user is null)
                return;
            users.Remove(user);
            changeInFile();
        }
   } 
}