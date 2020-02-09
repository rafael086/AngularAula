using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularAula.DTO
{
    public class EventoDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo obrigatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Local é entre  3 e 100 caracteres")]
        public string Local { get; set; }
        
        [Required(ErrorMessage = "Data do evento é obrigatoria")]
        public string DataEvento { get; set; }
        
        [Required(ErrorMessage = "Tema é obrigatorio")]
        public string Tema { get; set; }

        [Range(2,120000,ErrorMessage = "Quantidade deve estar entre 2 e 120000")]
        public int QtdPessoas { get; set; }
        
        public string Lote { get; set; }
        
        public string ImagemURL { get; set; }
        
        [Phone(ErrorMessage = "Telefone inválido")]
        public string Telefone { get; set; }

        [EmailAddress(ErrorMessage = "Forneça um email válido")]
        public string Email { get; set; }

        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedeSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }

    }
}
