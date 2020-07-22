using System.Collections.Generic;
using Commander.Models;
using Commander.Data;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    // route: api/commands
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly MockCommanderRepo _repository = new MockCommanderRepo();

        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAppCommands();
            return Ok(commandItems);
        }

        //GET api/commands/{id}
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem = _repository.GetAppCommandById(id);
            return Ok(commandItem);
        }
    }
}