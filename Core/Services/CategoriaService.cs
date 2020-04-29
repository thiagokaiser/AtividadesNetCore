using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Core.Models;
using System.Reflection;
using Core.Interfaces;
using Core.ViewModels;

namespace Core.Services
{
    public class CategoriaService
    {
        private readonly IRepositoryCategoria repository;

        public CategoriaService(IRepositoryCategoria repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Categoria> Select()
        {            
            var categ = repository.Select();
            return categ;
        }

        public Categoria SelectById(int id)
        {            
            var categ = repository.SelectById(id);
            return categ;
        }

        public ResultViewModel Insert(Categoria categ)
        {            
            var result = repository.Insert(categ);
            return result;
        }     
        
        public ResultViewModel Update(Categoria categ)
        {            
            var result = repository.Update(categ);                    
            return result;
        }

        public ResultViewModel Delete(Categoria categ)
        {            
            var result = repository.Delete(categ);                    
            return result;
        }        
    }
}
