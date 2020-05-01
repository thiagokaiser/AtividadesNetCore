using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.ViewModels.Atividade
{
    public class EditAtividadeViewModel
    {        
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Responsavel { get; set; }
        public string Setor { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        public List<Categoria> Categorias { get; set; }
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }        
        public string Solicitante { get; set; }
        [DataType(DataType.MultilineText)]
        public string Narrativa { get; set; }
    }
}
