using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Administrator")]
    public class AccountRequestController : ControllerBase
    {
        private readonly WheresthekeyContext _context;

        public AccountRequestController(WheresthekeyContext context)
        {
            _context = context;
        }

        [Route("ListUsers"), HttpGet]
        public async Task<ActionResult> ListUsers([FromQuery] FilterUserDto filter = null)
        {
            try
            {
                IEnumerable<Person> users = await _context.People.ToListAsync();

                if (!String.IsNullOrEmpty(filter.Id))
                    users = users.Where(x => x.Id == filter.Id).ToList();

                if (!String.IsNullOrEmpty(filter.Name))
                    users = users.Where(x => x.Name == filter.Name).ToList();

                if (filter.RolePersonId != null && filter.RolePersonId != 0)
                    users = users.Where(x => x.RolePersonId == filter.RolePersonId).ToList();

                if (filter.AccountStatusId != null && filter.AccountStatusId != 0)
                    users = users.Where(x => x.AccountStatusId == filter.AccountStatusId).ToList();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateUserStatus"), HttpPut]
        public async Task<ActionResult> UpdateUserStatus([FromBody] UserChangeStatusDto userToChange)
        {
            try
            {
                if (ModelState.IsValid && userToChange.AccountStatusId > 0)
                {
                    var user = await _context.People.FindAsync(userToChange.Id);

                    if (user != null)
                    {
                        user.AccountStatusId = userToChange.AccountStatusId;
                        _context.People.Update(user);
                        await _context.SaveChangesAsync();

                        return Ok("O status do usuário foi atualizado com sucesso.");
                    }
                    else
                    {
                        return NotFound("Usuário não encontrado.");
                    }
                }
                else
                {
                    throw new Exception("Campos inválidos.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}