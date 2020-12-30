using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.CommandLine;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAppCommands()
        {
            var commands = new List<Command>
            {
                new Command {HowTo = "Run DotNet", Id = 0, Line = "dotnet run", Platform = "DotNet"},
                new Command {HowTo = "Autorun DotNet Project", Id = 1, Line = "dotnet watch run", Platform = "DotNet"},
                new Command {HowTo = "Build DotNet Project", Id = 2, Line = "dotnet build", Platform = "DotNet"}
            };

            return commands;
        }

        public Command GetCommandById(int Id)
        {
           return new Command {HowTo = "Create new DotNet Project", Id = 0, Line = "dotnet new", Platform = "DotNet"};
        }
    }
}
