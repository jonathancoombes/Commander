using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.CommandLine;
using Commander.Dtos;
using Commander.Data;
using AutoMapper;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo

    {
        private readonly IMapper _imapper;

        public MockCommanderRepo(IMapper imapper)
    {
            _imapper = imapper;  }   
        
        public IEnumerable<CommandReadDto> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command {HowTo = "Run DotNet", Id = 0, Line = "dotnet run", Platform = "DotNet"},
                new Command {HowTo = "Autorun DotNet Project", Id = 1, Line = "dotnet watch run", Platform = "DotNet"},
                new Command {HowTo = "Build DotNet Project", Id = 2, Line = "dotnet build", Platform = "DotNet"}
            };

            return _imapper.Map<IEnumerable<CommandReadDto>>(commands);
                
                }

        public CommandReadDto GetCommandById(int Id)
        {
            return _imapper.Map<CommandReadDto>(new Command { HowTo = "Create new DotNet Project", Id = 0, Line = "dotnet new" });
        }

        public void NewCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
