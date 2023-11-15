using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using services;
using Interface;
using Microsoft.AspNetCore.Authorization;

namespace Controllers{

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Agent")]

public class ControllerTask:ControllerBase{

    private long userId;

    private InterfaceTask TaskServise;

public ControllerTask(InterfaceTask TaskServise)
{
    this.TaskServise = TaskServise;
    this.userId = long.Parse(User.FindFirst("UserId")?.Value ?? "");


}
[HttpGet]

    public ActionResult<List<Task>> Get() =>
        TaskServise.Get(userId);

[HttpGet("{id}")]

    public ActionResult<Task> GetById(int id)
    {
        var indexTask = TaskServise.GetById(userId,id);
        if (indexTask is null)
            return NotFound();
        return indexTask;

    }

[HttpPost]

    public IActionResult CreateTask(Task task)
    {
       TaskServise.Add(userId,task);
       return CreatedAtAction(nameof(CreateTask),new {id=task.Id}, task);
    }

[HttpPut("{id}")]

    public IActionResult UpdateTask(int id,Task task)
    {
        if (id != task.Id)
            return BadRequest();
        var IsId = TaskServise.GetById(userId,id);
        if (IsId is null)return NotFound();

        TaskServise.Update(userId,task);

        return NoContent();

    }

[HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isId = TaskServise.GetById(userId,id);
        if (isId is null)
            return NotFound();
        TaskServise.Delete(userId,id);
        return Content(TaskServise.Count(userId).ToString());
    }

}
}