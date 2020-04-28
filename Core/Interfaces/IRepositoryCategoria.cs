using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IRepositoryCategoria
    {
        IEnumerable<Categoria> Select();
        Categoria SelectById(int id);
        string Insert(Categoria categ);
        string Update(Categoria categ);
        string Delete(Categoria categ);        
    }
}
