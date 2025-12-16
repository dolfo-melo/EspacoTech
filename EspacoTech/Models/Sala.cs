using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoTech.Models
{
    public class Sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSala { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoSala { get; set; }

        [Required]
        [Display(Name = "Capacidade")]
        public int Capacidade { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Características da Sala")]
        public string Descricao { get; set; }
        
        [Required]
        [StringLength(50)]
        [Display(Name = "Equipamentos da Sala")]
        public string Equipamento { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}
