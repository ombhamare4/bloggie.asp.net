using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController
{
                                                                                //Om.so
    // private readonly DataContext _context;
    // public UsersController(DataContext context)
    // {
    //     _context = context;
    // }
                                                                                //Om.eo
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = context.Users.ToList();

        return users;
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<AppUser> GetUser(int id)
    {
        var user = context.Users.Find(id);
        return user;
    }
}
