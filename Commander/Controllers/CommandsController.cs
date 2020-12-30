using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commander.Data;
using Commander.Models;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase

    {

        private readonly ICommanderRepo _repositoryRepo;

        public CommandsController(ICommanderRepo commanderRepo)
        {
            _repositoryRepo = commanderRepo;

        }

        //GET Example: api/commands
        [HttpGet]
            public ActionResult <IEnumerable<Command>>GetAllCommands()
            {

                var commandItems =  _repositoryRepo.GetAppCommands();

                return Ok(commandItems);


            }
            //GET Example: api/commands/5
            [HttpGet("{Id}")]
            public ActionResult<Command> GetCommandById(int Id)
            {
                var commandItem = _repositoryRepo.GetCommandById(Id);

                return Ok(commandItem);


            }

    }
}
