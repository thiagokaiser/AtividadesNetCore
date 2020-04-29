using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IRepositoryAtividade
    {
        IEnumerable<Atividade> Select();
        IEnumerable<Atividade> SelectEncerrados();
        Atividade SelectById(int id);
        ResultViewModel Insert(Atividade atividade);
        ResultViewModel Update(Atividade atividade);
        ResultViewModel UpdateEncerra(Atividade atividade);
        ResultViewModel Reabrir(Atividade atividade);
        ResultViewModel Delete(Atividade atividade);
        ResultViewModel AlteraPrioridade(JsonPrioridade item);

    }
}
