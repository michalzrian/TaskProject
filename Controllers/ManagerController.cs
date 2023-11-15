using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models;
using Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using services;

namespace Controllers{
[ApiController]
[Route("[controller]")]
public class StoreManagerController: ControllerBase
{
    private List<User> users;
        public StoreManagerController()
        {
            users = new List<User>
            {
                 new User { UserId = 1, Username = "michal", Password = "A1234!", Manager = true},
                new User { UserId = 2, Username = "rut", Password = "Y1234@"},
                new User { UserId = 3, Username = "malki", Password = "Y1234#"}
            };


        }


    
       [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
            var dt = DateTime.Now;

            var user = users.FirstOrDefault(u =>
                u.Username == User.Username 
                && u.Password == User.Password
                
            );        

            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim("UserType", user.Manager ? "StoreManager" : "Agent"),
                new Claim("userId", user.UserId.ToString()),

            };

            var token = TaskServicesToken.GetToken(claims);

            return new OkObjectResult(TaskServicesToken.WriteToken(token));
        }
    }
}
