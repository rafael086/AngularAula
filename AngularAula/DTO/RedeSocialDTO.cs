using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAula.DTO
{
    public class RedeSocialDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome da rede social é obrigatoria")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "URL da rede social é obrigatoria")]
        public string URL { get; set; }

    }
}
