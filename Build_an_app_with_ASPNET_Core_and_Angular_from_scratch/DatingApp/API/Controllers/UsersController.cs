using System.Security.Claims;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController
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

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDTO memberUpdateDTO)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null) return BadRequest("Username not found in token");

        var user = await userRepository.GetUserByUsernameAsync(username);

        if (user == null) return BadRequest("User not found");

        mapper.Map(memberUpdateDTO, user);

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDTO>> AddPhoto(IFormFile file)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Cannot Update the user");

        var result = await photoService.AddPhotoAsync(file);
        if (result.Error != null) return BadRequest("Error " + result.Error.Message);

        var photo = new Photo
        {
            URL = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.Photos.Add(photo);

        if (await userRepository.SaveAllAsync())
        {
            return CreatedAtAction(nameof(GetUserByUsername),
                                        new { username = user.Username },
                                        mapper.Map<PhotoDTO>(photo));
        }
        return BadRequest("Problem Adding the photo");
    }
}
