using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;

public class ErrorController(DataContext context) : BaseApiController
{
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secrest test";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var things = context.Users.Find(-1);
        if (things == null)
        {
            return NotFound();
        }

        return things;
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = context.Users.Find(-1).ToString();

        return "secrest test";
    }

    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secrest test";
    }

    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secrest test";
    }
}
