using Application.Actions.UserActions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHasingService _hashing;

        public UserController(IUserRepository repository, IMapper mapper, IHasingService hashing)
        {
            _repository = repository;
            _mapper = mapper;
            _hashing = hashing;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            List<User> query = _repository.GetAll();
            var items = query.ToList();

            return Ok(items);
        }

        [HttpPost]
        public ActionResult<ReadUser> Post(CreateUser item)
        {
            var userModel = _mapper.Map<User>(item);

            userModel.CreatedAt = DateTime.Now;
            userModel.Password = _hashing.HashPassword(userModel.Password);

            _repository.Add(userModel);

            var userOutput = _mapper.Map<ReadUser>(userModel);

            return Created("/user", userOutput);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, JsonPatchDocument<UpdateUser> patchDocument)
        {
            var userModel =  _repository.Get(id);


            if(userModel == null)
            {
                return NotFound();
            }

            var modelToPatch = _mapper.Map<UpdateUser>(userModel);
            patchDocument.ApplyTo(modelToPatch, ModelState);

            if(!TryValidateModel(modelToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(modelToPatch, userModel);
            _repository.Update(id, userModel);
            
            return Ok();
        }
    }
}
