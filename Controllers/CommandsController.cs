using AutoMapper;
using System.Collections.Generic;
using Commander.Models;
using Commander.Data;
using Commander.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

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
            var commands = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(command));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var command = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(command);
            _repository.SaveChanges();
            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);
            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }

            _mapper.Map(commandUpdateDTO, command);
            _repository.UpdateCommand(command); // This is not neccesary, because of _mapper.Map is tracking the changes in the "command" variable.
            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/commads/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(command);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, command);
            _repository.UpdateCommand(command);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var command = _repository.GetCommandById(id);
            if (command == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(command);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}