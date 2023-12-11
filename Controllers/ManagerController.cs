using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Interface;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Security.Claims;
using services;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Controllers{
using Models;

[ApiController]
// [Route("[action]")]

[Route("[controller]")]
public class ManagerController: ControllerBase
{
        InterfaceUser userService;

    // private List<User> users;
        public ManagerController(InterfaceUser userService)
        {
        this.userService = userService;
        }


    
       [HttpPost]
        [Route("Login")]
        public ActionResult<String> Login([FromBody] User User)
        {
            var userIsExist = userService.GetAll().FirstOrDefault(u => u.Username == User.Username&&u.Password == User.Password);
            if(userIsExist== null){
                Console.WriteLine(userIsExist);
                return Unauthorized();
            }
            var claim = new List<Claim>{
                new Claim ("UserType","User"),
                new Claim ("UserId",userIsExist.UserId.ToString())

            };

            if(userIsExist.Manager){
                claim.Add(new Claim("UserType","Manager"));
            }    
    System.Console.WriteLine(userIsExist.UserId.ToString());
            var token = TaskServicesToken.GetToken(claim);

            return new OkObjectResult(TaskServicesToken.WriteToken(token));
        }
    }
}
