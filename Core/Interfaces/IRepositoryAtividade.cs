using Core.Models;
using Core.ViewModels;
using Core.ViewModels.Atividade;
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
        EditAtividadeViewModel SelectByIdWithCateg(int id);
        EditAtividadeViewModel editAtividadeViewModel();
        ResultViewModel Insert(EditAtividadeViewModel atividade);
        ResultViewModel Update(EditAtividadeViewModel atividade);
        ResultViewModel UpdateEncerra(Atividade atividade);
        ResultViewModel Reabrir(Atividade atividade);
        ResultViewModel Delete(Atividade atividade);
        ResultViewModel AlteraPrioridade(PrioridadeAtividade item);

    }
}
