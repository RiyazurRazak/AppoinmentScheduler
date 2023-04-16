using AppoinmentScheduler.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using AppoinmentScheduler.Dto;
using AppoinmentScheduler.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace AppoinmentScheduler.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
  
    public class OrganizationController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;

        public OrganizationController( ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] OrganizationRegister registerData)
        {
            try
            {

                if (_dbContext.RootUsers.Any(u => u.EmailAddress == registerData.EmailAddress))
                {
                    return BadRequest(new
                    {
                        status = false,
                        message = "user already exist"
                    });
                }

                RootUsers user = new RootUsers();
                user.EmailAddress = registerData.EmailAddress;
                user.Address = registerData.Address;
                user.OrganizationName = registerData.OrganizationName;
                user.ContactNumber = registerData.ContactNumber;
                user.Username = registerData.Username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(registerData.Password);
                user.Id = Guid.NewGuid().ToString();
                user.Token = Guid.NewGuid().ToString();
                user.IamSlug = $"{registerData.OrganizationName}-{Guid.NewGuid().ToString().Substring(0, 5)}";

                _dbContext.RootUsers.Add(user);

                await _dbContext.SaveChangesAsync();

                return Ok(new
                {
                    status = true,
                    data = new
                    {
                        username = user.Username,
                        emailAddress = user.EmailAddress,
                        slug = user.IamSlug,
                        organizationName = user.OrganizationName,
                        token = user.Token,
                    }
                });

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

        public  IActionResult Login([FromForm] OrganizationLogin loginData)
        {
            Console.WriteLine(loginData.Username);
            try
            {

               var user = _dbContext.RootUsers.Where(user => user.Username.Equals(loginData.Username)).ToList();
               if(user.Count == 1)
                {
                    
                    if(BCrypt.Net.BCrypt.Verify(loginData.Password, user[0].Password))
                    {
                      
                        return Ok(new
                        {
                            status = true,
                            data = new
                            {
                                username = user[0].Username,
                                emailAddress = user[0].EmailAddress,
                                slug = user[0].IamSlug,
                                token = user[0].Token,
                                organizationName = user[0].OrganizationName

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

            }catch(Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpPost("appoinment")]
        public async Task<IActionResult> AddAppoinment([FromForm] AddAppoinment appoinmentData)
        {
            try
            {
                if (HttpContext.Items["User"] != null)
                {

                    Appoinments appoinment = new Appoinments();
                    appoinment.Name = appoinmentData.Name;
                    appoinment.Description = appoinmentData.Description;
                    appoinment.StartRange = appoinmentData.StartRange.AddHours(5).AddMinutes(30);
                    appoinment.EndRange = appoinmentData.EndRange.AddHours(5).AddMinutes(30);
                    appoinment.Intreval = appoinmentData.Intreval;
                    appoinment.Id = Guid.NewGuid().ToString();
                    appoinment.OrganizationId = (string)HttpContext.Items["User"];

                    _dbContext.Appoinments.Add(appoinment);
                    await _dbContext.SaveChangesAsync();

                    decimal intreval = appoinmentData.Intreval;
                    List<Slots> slots = new List<Slots>();
                    for (DateTime appointmentTime = appoinmentData.StartRange.AddHours(5).AddMinutes(30); appointmentTime < appoinmentData.EndRange.AddHours(5).AddMinutes(30); appointmentTime = appointmentTime.AddMinutes(60))
                    {
                        slots.Add(new Slots
                        {
                            Id = Guid.NewGuid().ToString(),
                            AppoinmentId = appoinment.Id,
                            SlotTime = appointmentTime,
                            BookedBy = "",
                        });
                    }
                    _dbContext.Slots.AddRange(slots);
                    await _dbContext.SaveChangesAsync();
                    return Ok(new
                    {
                        status = true,
                        data = new
                        {
                            appoinment
                        }
                    });
                    
                }
                else
                {
                    return Unauthorized();
                }
                

            }catch(Exception err)
            {
                return BadRequest(new
                {
                    status = false,
                    message = err.ToString()
                });
            }
        }

        [HttpGet("appoinment")]

        public async Task<IActionResult> Appoinment()
        {
            try
            {
                if (HttpContext.Items["User"] != null)
                {
                    string orgId = (string)HttpContext.Items["User"];
                   var appoinments = _dbContext.Appoinments.Where(appoinment => appoinment.OrganizationId.Equals(orgId)).ToList();
                    return Ok(new
                    {
                        status = true,
                        data =  appoinments
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

        [HttpGet("slot")]

        public async Task<IActionResult> Slots([FromQuery] string id)
        {

            try
            {
                if (HttpContext.Items["User"] != null)
                {
                    string orgId = (string)HttpContext.Items["User"];
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
    }
}
