using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interface;
using DataAccessEF;
using Domain;
using System.IO;
using System.Text;

namespace WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public UserApiController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(unitOfWork.User.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest();

            User user = unitOfWork.User.GetById((int)id);
            return (user != null) ? Ok(user) : NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            try
            {
                User newUser = unitOfWork.User.Create(user);
                unitOfWork.Commit();
                return Ok(newUser);
            }
            catch 
            {
                unitOfWork.Rollback();
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int? id,[FromBody] User user)
        {
            if (id == null || id != user.IdUser)
                return BadRequest();

            try
            {
                User updatedUser = unitOfWork.User.Update(user);
                unitOfWork.Commit();
                return Ok(updatedUser);
            }
            catch 
            {
                unitOfWork.Rollback();
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest();

            User user = unitOfWork.User.GetById((int)id);
            if (user == null)
                return NotFound();

            unitOfWork.User.Delete(user);
            unitOfWork.Commit();
            return NoContent();
        }

    }
}
