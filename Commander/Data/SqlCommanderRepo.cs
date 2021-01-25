using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Commander.Data;
using Commander.Dtos;

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

            return _mapper.Map<IEnumerable<Command>>(result);

        }

        public void NewCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _context.Commands.Add(cmd);
            _context.SaveChanges();
                    
         }

        public Command GetCommandById(int id)
        {
            var result = _context.Commands.SingleOrDefault(p => p.Id == id);
            return _mapper.Map<Command>(result);
        }

        public void UpdateCommand(Command cmd)
        {
            _context.Commands.Update(cmd);
            _context.SaveChanges();
        }

        public void DeleteCommand(Command cmd)
        {
            _context.Commands.Remove(cmd);
            _context.SaveChanges();
        }
    }
}
