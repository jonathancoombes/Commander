using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Commander.Data;
using Commander.Models;
using Commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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

        // GET: api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {

            var commandItems = _repositoryRepo.GetAppCommands();
            if (commandItems != null)
            {
                return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
            }
            else
            {
                return NoContent();
            }


        }
        // GET: api/commands/{Id}}
        [HttpGet("{Id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int Id)
        {
            var commandItem = _repositoryRepo.GetCommandById(Id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        // POST: api/commands/
        [HttpPost]
        public ActionResult<CommandReadDto> NewCommand(CommandWriteDto cmd) {

            var commandModel = _mapper.Map<Command>(cmd);

            _repositoryRepo.NewCommand(commandModel);

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);

        }

        // PUT: api/commands/{Id}

        [HttpPut("{Id}")]
        public ActionResult UpdateCommand(CommandUpdateDto updatedCmd, int Id) {

            var originalCommand = _repositoryRepo.GetCommandById(Id);

            if (originalCommand == null) {
                throw new ArgumentNullException(nameof(originalCommand));
            }

            // With this command, the update is allready tracked by the context in EF so, only "save changes is required from the Repo
            // to persist the changes to the DB..

            _mapper.Map(updatedCmd, originalCommand);

            // For good practice, this is done manually as well..

            _repositoryRepo.UpdateCommand(originalCommand);

            return NoContent();

        }

        // PATCH:  api/commands/{Id}

        [HttpPatch("{Id}")]
        public ActionResult PartialCommandUpdate(int Id, JsonPatchDocument<CommandUpdateDto> jsonPatchDocument) {

            var originalCommand = _repositoryRepo.GetCommandById(Id);

            //Checking if resource exists

            if (originalCommand == null) {
                throw new ArgumentNullException(nameof(originalCommand));
            }
            // Mapping originalcommand to a Dto for use
            var commandToPatch = _mapper.Map<CommandUpdateDto>(originalCommand);

            //Applying the jsonData to commandToPatch model. Using Modelstate for validation check

            jsonPatchDocument.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch)) {
                return ValidationProblem(ModelState);
            }

            // Once validation passed, continue to pass updated model to context

            _mapper.Map(commandToPatch, originalCommand);

            _repositoryRepo.UpdateCommand(originalCommand);

            return NoContent();
        }

        // DELETE api/controllers/{Id}
        [HttpDelete("{Id}")]
        public ActionResult DeleteCommand(int Id) {

            var currentCommand = _repositoryRepo.GetCommandById(Id);

            if (currentCommand == null)
            {
                throw new ArgumentNullException(nameof(currentCommand));
            }
            _repositoryRepo.DeleteCommand(currentCommand);

            return NoContent();
            
        }

    }
}
