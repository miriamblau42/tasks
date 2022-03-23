using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Jobs2.Modules;
using Jobs2.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Jobs2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Jobs2Controller : ControllerBase
    {
        // private User MyUser;
        private IJobService Jobs2Service;
        public Jobs2Controller(IJobService Jobs2Service)
        {
         //   MyUser=user;
            this.Jobs2Service = Jobs2Service;
        }
      
        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<List<Job>> GetAll() {
           return  Jobs2Service.GetAll();
        }
            
             
        

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult<Job> Get(int id)
        {
            var job = Jobs2Service.Get(id,Jobs2Service.CurrentUser.Id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }
        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "User")]
        public IActionResult Create(Job item)
        {
            //not sure about that way
            //maybe it is right to leave it with getting full job//
            Job job = new Job();
            job.NameOfJob = item.NameOfJob;
            job.IsDone = item.IsDone;
            job.UserId = Jobs2Service.CurrentUser.Id;
            Jobs2Service.Add(job);
             return CreatedAtAction(nameof(Create), new { id = job.Id }, job);
        }
        [HttpPut("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Update(int id,Job job)
        {
            if(id!=job.Id)
            {
                return BadRequest();
            }
            var findJob=Jobs2Service.Get(id, Jobs2Service.CurrentUser.Id);
            if(findJob is null)
            {
                return NotFound();
            }
            Jobs2Service.Update(job);
            return Content("its me!!!");
        }
        [HttpDelete("{id}")]
       [Authorize(Policy = "User")]
        public ActionResult Delete(int id)
        {
            var job=Jobs2Service.Get(id,Jobs2Service.CurrentUser.Id);
            if(job is null)
            {
                return NotFound();
            }
            Jobs2Service.Delete(id,Jobs2Service.CurrentUser.Id);
            return Content(Jobs2Service.Count.ToString());
        }                     



    }
}