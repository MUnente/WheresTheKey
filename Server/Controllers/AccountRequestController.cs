using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Context;
using Microsoft.AspNetCore.Authorization;
using System.Text;

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
        public ActionResult ListUsers([FromQuery] FilterUserDto filter = null)
        {
            try
            {
                IEnumerable<Person> users = _context.People.ToList();

                if (!String.IsNullOrEmpty(filter.Id))
                    users = users.Where(x => x.Id == filter.Id).ToList();

                if (!String.IsNullOrEmpty(filter.Name))
                    users = users.Where(x => x.Name == filter.Name).ToList();

                if (filter.RolePersonId != null && filter.RolePersonId != 0)
                    users = users.Where(x => x.RolePersonId == filter.RolePersonId).ToList();

                if (filter.AccountStatusId != null && filter.AccountStatusId != 0)
                    users = users.Where(x => x.AccountStatusId == filter.AccountStatusId).ToList();

                if (users.Count() > 0)
                    return Ok(users);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateUserStatus"), HttpPut]
        public async Task<ActionResult> UpdateUserStatus([FromBody] UserChangeStatus user)
        {
            try
            {
                if (String.IsNullOrEmpty(user.Id) || user.AccountStatusId == 0)
                    throw new Exception("Campos inválidos.");

                var users = await _context.People.FindAsync(user.Id);

                if (users != null)
                {
                    users.AccountStatusId = user.AccountStatusId;
                    _context.People.Update(users);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}