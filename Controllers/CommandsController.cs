using AutoMapper;
using System.Collections.Generic;
using Commander.Models;
using Commander.Data;
using Commander.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    // route: api/commands
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandItem = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandItem);
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 933bfbc75ed8d1a35e2a11afbb8c538f9dab3789
            _repository.SaveChanges();
            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandItem);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
        }
<<<<<<< HEAD
=======
=======

            if (_repository.SaveChanges())
            {
                var commandReadDTO = _mapper.Map<CommandReadDTO>(commandItem);
                return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
            }
            return BadRequest();
        }

>>>>>>> 6c3102a43a73965e33000e74848a4b319833fa99
>>>>>>> 933bfbc75ed8d1a35e2a11afbb8c538f9dab3789
    }
}