using AppoinmentScheduler.Data;
using AppoinmentScheduler.Dto;
using AppoinmentScheduler.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppoinmentScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;

        public UserController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserRegister registerData)
        {
            try
            {

                if (_dbContext.Users.Any(u => u.EmailAddress == registerData.EmailAddress))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "user already exist"
                    });
                }
                var org = _dbContext.RootUsers.Where(org => org.IamSlug.Equals(registerData.OrganizationSlug)).ToList();
                if (org.Count == 1)
                {
                    Users user = new Users();
                    user.EmailAddress = registerData.EmailAddress;
                    user.ContactNumber = registerData.ContactNumber;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
                    user.Id = Guid.NewGuid().ToString();
                    user.OrganizationId = org[0].Id;
                    user.Name = registerData.Name;

                    _dbContext.Users.Add(user);

                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        status = true,
                        data = new
                        {
                            emailAddress = user.EmailAddress,
                            name = user.Name,
                            contact = user.ContactNumber,
                            token = user.Id,
                        }
                    });
                }
                else
                {
                    return NotFound();
                }


            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }

        }

        [HttpPost("login")]

        public IActionResult Login([FromForm] UserLogin loginData)
        {

            try
            {

                var user = _dbContext.Users.Where(user => user.EmailAddress.Equals(loginData.EmailAddress)).ToList();
                if (user.Count == 1)
                {

                    if (BCrypt.Net.BCrypt.Verify(loginData.Password, user[0].Password))
                    {

                        return Ok(new
                        {
                            status = true,
                            data = new
                            {
                                emailAddress = user[0].EmailAddress,
                                name = user[0].Name,
                                token = user[0].Id,
                                contact = user[0].ContactNumber

                            },
                        });
                    }
                    else
                    {
                        return BadRequest(new
                        {
                            status = false,
                            message = "Invalid Credentials"
                        });
                    }
                }
                else
                {
                    Console.WriteLine("Err");
                    return NotFound();
                }

            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpGet("appoinment")]

        public async Task<IActionResult> Appoinment([FromQuery] string orgSlug)
        {
            try
            {
                if (orgSlug != null)
                {
                    var org = _dbContext.RootUsers.Where(org => org.IamSlug.Equals(orgSlug)).ToList();
                    if (org.Count == 1)
                    {
                        string orgId = org[0].Id;
                        var appoinments = _dbContext.Appoinments.Where(appoinment => appoinment.OrganizationId.Equals(orgId)).ToList();
                        return Ok(new
                        {
                            status = true,
                            data = appoinments
                        });
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpGet("slot")]

        public async Task<IActionResult> Slots([FromQuery] string id)
        {

            try
            {
                if (id != null)
                {
                    var slots = _dbContext.Slots.Where(slot => slot.AppoinmentId.Equals(id)).ToList();
                    return Ok(new
                    {
                        status = true,
                        data = slots
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpGet("myslots")]

        public async Task<IActionResult> MySlots([FromQuery] string id)
        {

            try
            {
                if (id != null)
                {
                    var query = from slot in _dbContext.Slots join appoinment in _dbContext.Appoinments on slot.AppoinmentId equals appoinment.Id where slot.BookedBy == id select new {slot.SlotTime, appoinment.Name };
                    var slots = query.ToList();
                    return Ok(new
                    {
                        status = true,
                        data = slots
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpPut("slot")]

        public async Task<IActionResult> BookSlot([FromForm] UserSlotBook data)
        {

            try
            {
                if (data.Id != null)
                {
                   _dbContext.Slots.Where(slot => slot.Id.Equals(data.Id)).ToList().ForEach(slot =>
                    {
                        slot.IsBooked = true;
                        slot.BookedBy = data.BookedBy;
                    });
                    await _dbContext.SaveChangesAsync();
                    return Ok(new
                    {
                        status = true,
                        message = "Slot Updated"
                    });
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }


    }
}
