using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Core.Models;
using System.Reflection;
using Core.Interfaces;

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

        public string Insert(Categoria categ)
        {            
            var mensagem = repository.Insert(categ);
            return mensagem;
        }     
        
        public string Update(Categoria categ)
        {            
            var mensagem = repository.Update(categ);                    
            return mensagem;
        }

        public string Delete(Categoria categ)
        {            
            var mensagem = repository.Delete(categ);                    
            return mensagem;
        }        
    }
}
