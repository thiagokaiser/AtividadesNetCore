using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IRepositoryCategoria
    {
        IEnumerable<Categoria> Select();
        Categoria SelectById(int id);
        ResultViewModel Insert(Categoria categ);
        ResultViewModel Update(Categoria categ);
        ResultViewModel Delete(Categoria categ);        
    }
}
