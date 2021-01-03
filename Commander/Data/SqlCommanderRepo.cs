using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommanderRepo
    {
        private readonly IMapper _mapper;
        private readonly CommanderContext _context;

        public SqlCommanderRepo(IMapper mapper, CommanderContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public IEnumerable<Command> GetAppCommands()
        {

            var result = _context.Commands.ToList();

            return result;

        }

        Command ICommanderRepo.GetCommandById(int id)
        {
            var result = _context.Commands.SingleOrDefault(p => p.Id == id);
          
                return result;
            
        }
    }
}
