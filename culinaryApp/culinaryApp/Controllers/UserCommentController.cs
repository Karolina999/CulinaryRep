using Microsoft.AspNetCore.Mvc;
using culinaryApp.Models;
using culinaryApp.Interfaces;
using AutoMapper;
using culinaryApp.Dto;

namespace culinaryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCommentController : ControllerBase
    {
        private readonly IUserCommentRepository _userCommentRepository;
        private readonly IMapper _mapper;

        public UserCommentController(IUserCommentRepository userCommentRepository, IMapper mapper)
        {
            _userCommentRepository = userCommentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserComment>))]
        public IActionResult GetuserComments()
        {
            var userComments = _mapper.Map<List<UserCommentDto>>(_userCommentRepository.GetUserComments());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userComments);
        }

        [HttpGet("{userCommentId}")]
        [ProducesResponseType(200, Type = typeof(UserComment))]
        [ProducesResponseType(400)]
        public IActionResult GetuserComment(int userCommentId)
        {
            if (!_userCommentRepository.UserCommentExists(userCommentId))
                return NotFound();

            var userComment = _mapper.Map<UserCommentDto>(_userCommentRepository.GetUserComment(userCommentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(userComment);
        }
    }
}
