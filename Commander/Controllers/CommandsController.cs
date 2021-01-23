using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Models;
using Commander.Dtos;

namespace Commander.Controllers
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase

    {
        private readonly ICommanderRepo _repositoryRepo;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo commanderRepo, IMapper mapper)
        {
            _repositoryRepo = commanderRepo;
            _mapper = mapper;

        }

        //GET Example: api/commands
        [HttpGet]
            public ActionResult <IEnumerable<CommandReadDto>>GetAllCommands()
            {

                var commandItems =  _repositoryRepo.GetAppCommands();
                if(commandItems != null)
                {
                    return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
                }
                else
                {
                    return NoContent();
                }


            }
            //GET Example: api/commands/{Id}}
            [HttpGet("{Id}")]
            public ActionResult<CommandReadDto> GetCommandById(int Id)
            {
                var commandItem = _repositoryRepo.GetCommandById(Id);
                if (commandItem != null)
                {
                    return Ok(_mapper.Map<CommandReadDto>(commandItem));
                }

                return NotFound();
            }

        //POST Example: api/commands/
        [HttpPost]
        public ActionResult<CommandReadDto> NewCommand (CommandWriteDto cmd) {

            var commandModel = _mapper.Map<Command>(cmd);

            _repositoryRepo.NewCommand(commandModel);

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), commandReadDto.Id, commandReadDto);
          
        
        }
    }
}
