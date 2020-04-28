using Core.Models;
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
        string Insert(Atividade atividade);
        string Update(Atividade atividade);
        string UpdateEncerra(Atividade atividade);
        string Reabrir(Atividade atividade);
        string Delete(Atividade atividade);
        string AlteraPrioridade(JsonPrioridade item);

    }
}
