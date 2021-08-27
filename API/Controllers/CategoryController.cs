using API.Model;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryEnt ent;

        public CategoryController()
        {
            ent = new CategoryEnt();
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                ent.Id = id;
                var data = Category.GetById(ent).FirstOrDefault();
                return Ok(new
                {
                    Status = "OK",
                    Message = "Success",
                    Category = data
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

        [HttpPost]
        public ActionResult Post([FromBody] Model.GetCategory input)
        {
            try
            {
                ent.Name = input.Name;
                ent.IsActive = input.IsActive;
                var data = Category.Get(ent);
                return Ok(new
                {
                    Status = "OK",
                    Message = "Success",
                    Data = data.ToArray()
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

        [HttpPost("[action]")]
        public ActionResult Save([FromBody] SaveCategory input)
        {
            try
            {
                ent.Id = input.Id;
                ent.Name = input.Name;
                ent.IsActive = input.IsActive;
                ent = Category.Save(ent);
                return Ok(new
                {
                    Status = ent.RowsAffected > 0 ? "OK" : "Failed",
                    Message = ent.RowsAffected > 0 ? "Success" : "No Data Saved.",
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

        [HttpPost("[action]")]
        public ActionResult Delete([FromBody] BaseModel input)
        {
            try
            {
                ent.Id = input.Id;
                ent = Category.Delete(ent);
                return Ok(new
                {
                    Status = ent.RowsAffected > 0 ? "OK" : "Failed",
                    Message = ent.RowsAffected > 0 ? "Success" : "No Data Deleted.",
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
