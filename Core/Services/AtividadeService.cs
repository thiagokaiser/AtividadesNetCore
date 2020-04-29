﻿using System;
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
    public class AtividadeService
    {
        private readonly IRepositoryAtividade repository;

        public AtividadeService(IRepositoryAtividade repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Atividade> Select()
        {            
            var ativs = repository.Select();
            return ativs;
        }

        public IEnumerable<Atividade> SelectEncerrados()
        {
            
            var ativs = repository.SelectEncerrados();    
            return ativs;
        }

        public Atividade SelectById(int id)
        {
            
            var ativ = repository.SelectById(id);            
            return ativ;
        }

        public ResultViewModel Insert(Atividade atividade)
        {
            
            var result = repository.Insert(atividade);            
            return result;
        }     
        
        public ResultViewModel Update(Atividade atividade)
        {

            var result = repository.Update(atividade);
            return result;
        }

        public ResultViewModel UpdateEncerra(Atividade atividade)
        {
            
            var result = repository.UpdateEncerra(atividade);
            return result;
        }
        public ResultViewModel Reabrir(Atividade atividade)
        {
            
            var result = repository.Reabrir(atividade);
            return result;
        }
        public ResultViewModel Delete(Atividade atividade)
        {
            var result = repository.Delete(atividade);
            return result;
        }
        public ResultViewModel AlteraPrioridade(List<JsonPrioridade> lista)
        {                        
            int priorid = 0;
            
            foreach (var item in lista)
            {
                priorid += 1;
                item.Prioridade = priorid;                
                repository.AlteraPrioridade(item);                
            }

            return new ResultViewModel()
            {
                Success = true,
                Message = "Ordem ajustada",
                Data = null
            };
        }
    }
}
