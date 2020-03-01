using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{

    public class DepositController : ControllerBase
    {
        

        AuthenticationContext _db;
        public IConfiguration Configuration { get; }
        public DepositController(IConfiguration configuration, AuthenticationContext db)
        {
            Configuration = configuration;
            _db = db;
        }

        [HttpPost]
        [Route("api/Amount")]
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

                        int p = name + register.Deposit;
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

    }
    
}