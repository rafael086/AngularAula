using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAula.DTO
{
    public class LoteDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Nome do lote é obrigatorio")]
        public string Nome { get; set; }
        
        [Range(10, 5000, ErrorMessage = "Preco deve estar entre 10 e 5000")]
        public decimal Preco { get; set; }
        
        public string DataInicio { get; set; }
        
        public string DataFim { get; set; }

        [Range(2,120000)]
        public int Quantidade { get; set; }
        


    }
}
