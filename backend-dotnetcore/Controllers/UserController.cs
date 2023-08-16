using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebEng.Properties.Models;
using WebEng.Properties.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;

namespace WebEng.Properties.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : AbstractController{
    private readonly UserRepository _users;
    public UserController(UserRepository users){
        _users = users;
    }

/* removed some endpoints, api updated */

    [HttpGet("/users")]
    public async Task<ActionResult<IEnumerable<ApiModels.User>>> GetAllASync(){
        return Ok((await _users.FullCollection
                    .ToListAsync())
                    .Select(ApiModels.User.FromDatabase));
    }

    [HttpGet("/users/{id}")]
    public async Task<ActionResult<ApiModels.User>> GetSingleAsync (int id)=> await _users.FindAsync(id) switch{
        null => NotFound(),
        var user => Ok(ApiModels.User.FromDatabase(user)),
    };
}