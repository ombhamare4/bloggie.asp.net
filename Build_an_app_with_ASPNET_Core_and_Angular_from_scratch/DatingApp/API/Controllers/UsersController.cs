using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
    //Om.so
    // private readonly DataContext _context;
    // public UsersController(DataContext context)
    // {
    //     _context = context;
    // }
    //Om.eo
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();
        return Ok(users);
    }

    // [HttpGet("{id}")]
    // public ActionResult<AppUser> GetUser(int id)
    // {
    //     var user = context.Users.Find(id);
    //     return user;
    // }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDTO>> GetUserByUsername(string username)
    {
        var user = await userRepository.GetMembersAsync(username);
        if (user == null) return NotFound();
        return Ok(user);
    }
}
