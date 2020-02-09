using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAula.DTO
{
    public class PalestranteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do palestrante é obrigatorio")]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL { get; set; }
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
        
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}
