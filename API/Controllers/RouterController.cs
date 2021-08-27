using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouterController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var data = Repository.Router.Get();
                return Ok(new
                {
                    Status = "OK",
                    Message = "Success",
                    data = data
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = e.Message
                });
            }
        }

        [HttpGet("[action]")]
        public ActionResult Menu()
        {
            try
            {
                var data = Repository.Router.Get(true);
                return Ok(new
                {
                    Status = "OK",
                    Message = "Success",
                    data = data
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = e.Message
                });
            }
        }
    }
}
