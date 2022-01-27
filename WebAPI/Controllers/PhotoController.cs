using Application.Actions.PhotoActions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IUploadImageService _uploadImage;

        // GET: api/<PhotoController>
        public PhotoController(IPhotoRepository repository, IMapper mapper, 
            IUserRepository userRepository, IUploadImageService uploadImage)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
            _uploadImage = uploadImage;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Photo>> Get()
        {
            List<Photo> query = _repository.GetAllWithUser();
            var items = query;

            var output = _mapper.Map<IEnumerable<ReadPhotoWithUser>>(items);

            return Ok(output);
        }

        // GET api/<PhotoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            var photo = _repository.GetWithUser(id);

            if (photo == null)
            {
                return NotFound();
            }

            var photoOutput = _mapper.Map<ReadPhotoWithUser>(photo);

            return Ok(photoOutput);

        }

        [HttpGet("search_by_label")]
        public ActionResult<IEnumerable<ReadPhotoWithUser>> Search (string term = null)
        {
            if(term == null)
            {
                return NoContent();
            }

            List<Photo> photos = _repository.SearchByLabel(term);
            var items = photos;

            var output = _mapper.Map<IEnumerable<ReadPhotoWithUser>>(items);

            return Ok(output);
        }

        // POST api/<PhotoController>
        [HttpPost]
        [Authorize]
        public ActionResult<ReadPhotoWithUser> Post([FromForm] PhotoUpload item)
        {
            var photoModel = new Photo()
            {
                Label = item.Label
            };

            byte[] bytes;

            using(var memoryStream = new MemoryStream())
            {
                item.ImageFile.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            var imageUrl = _uploadImage.UploadImage(bytes);

            var user = _userRepository.GetByEmailOrUsername(User.Identity.Name);

            photoModel.CreatedAt = DateTime.Now;
            photoModel.Url = imageUrl;
            photoModel.UserId = user.Id;

            _repository.Add(photoModel);

            var photWithUser = _repository.GetWithUser(photoModel.Id);

            var photoOutput = _mapper.Map<ReadPhotoWithUser>(photWithUser);

            return Created("/photo", photoOutput);
        }

        // PUT api/<PhotoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PhotoController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var photo = _repository.Get(id);

            if (photo == null)
            {
                NotFound();
            }

            var user = _userRepository.GetByEmailOrUsername(User.Identity.Name);

            if (user.Id != photo.UserId)
            {
                Conflict();
            }


            _repository.Delete(photo);

            return Ok();
        }
    }
}
