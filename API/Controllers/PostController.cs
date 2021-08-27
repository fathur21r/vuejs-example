using API.Model;
using Entity;
using Repository;
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
    public class PostController : ControllerBase
    {
        PostEnt ent;

        public PostController()
        {
            ent = new PostEnt();
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            try
            {
                ent.Id = id;
                var data = Repository.Post.GetById(ent).FirstOrDefault();
                return Ok(new
                {
                    Status = "OK",
                    Message = "Success",
                    Post = data
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
        public ActionResult Post([FromBody] Model.GetPost input)
        {
            try
            {
                ent.CategoryId = input.CategoryId;
                ent.IsActive = input.IsActive;
                var data = Repository.Post.Get(ent);
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
        public ActionResult Save([FromBody] SavePost input)
        {
            try
            {
                ent.Id = input.Id;
                ent.CategoryId = input.CategoryId;
                ent.Content = input.Content;
                ent.Title = input.Title;
                ent.IsActive = input.IsActive;
                ent = Repository.Post.Save(ent);
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
                ent = Repository.Post.Delete(ent);
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
