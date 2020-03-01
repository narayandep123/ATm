using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{

    public class WithdrowController : ControllerBase
    {

        AuthenticationContext _db;
        public IConfiguration Configuration { get; }
        public WithdrowController(IConfiguration configuration, AuthenticationContext db)
        {
            Configuration = configuration;
            _db = db;
        }

        [HttpPost]
        [Route("api/AmountWithdrow")]
        [EnableCors("EnableCors")]

        public async Task<IActionResult> Insert([FromBody]Deposita register)
        {
            try
            {
                string error = "Invalid";
                if (register == null)
                {
                    return StatusCode((int)(HttpStatusCode.BadRequest));
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        register.DateTime = DateTime.Now;
                        var rowColl = _db.Deposit.AsEnumerable();
                        int name = (from r in rowColl
                                    select r.current_balance).First<int>();

                        int p = name - register.withdrow;
                        register.current_balance = p;
                        var update = _db.Deposit.Add(register);
                        _db.SaveChanges();
                        return Ok(register);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode((int)(HttpStatusCode.InternalServerError));
                    }
                }
                else
                {
                    return StatusCode((int)(HttpStatusCode.NoContent));
                }
            }
            catch
            {
                return StatusCode((int)(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("api/balance")]
        [EnableCors("EnableCors")]

        public async Task<IActionResult> balance([FromBody]Deposita register)
        {
            try
            {
                string error = "Invalid";
                if (register == null)
                {
                    return StatusCode((int)(HttpStatusCode.BadRequest));
                }

                if (ModelState.IsValid)
                { 
                    try
                    {
                        register.DateTime = DateTime.Now;
                        var rowColl = _db.Deposit.AsEnumerable();
                        int name = (from r in rowColl
                                    select r.current_balance).First<int>();

                        
                        return Ok(name);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode((int)(HttpStatusCode.InternalServerError));
                    }
                }
                else
                {
                    return StatusCode((int)(HttpStatusCode.NoContent));
                }
            }
            catch
            {
                return StatusCode((int)(HttpStatusCode.InternalServerError));
            }
        }

    }
}