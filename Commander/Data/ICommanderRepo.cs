using Commander.Dtos;
using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public interface ICommanderRepo
    {
        IEnumerable<CommandReadDto> GetAppCommands();
        CommandReadDto GetCommandById(int id);


    }
}
