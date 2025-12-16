using EspacoTech.Areas.Identity.Data;
using FluentValidation;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspacoTech.Models
{
    
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data da Reserva")]
        public DateTime Data { get; set; }

        public string Horario { get; set; }

        // FK para Sala
        [Required]
        [ForeignKey("Sala")]
        public int IdSala { get; set; }
        public Sala Sala { get; set; }

        // FK para Usuário 
        [Required]
        [ForeignKey("UsuarioId")]
        public string UsuarioId { get; set; }
        
        public Usuario UsuarioPerfil { get; set; }
    }

    public class ReservaValidation : AbstractValidator<Reserva>
    {
        public ReservaValidation()
        {
            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("A Data é Obrigatória!!")
                .Must(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Saturday)
                .WithMessage("Funcionamos de segunda à sexta-feira!!")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Não é possível agendar para Datas Anteriores!!");

            RuleFor(x => x.Horario)
                .NotEmpty().WithMessage("O horário é Obrigatório!!")
                .NotEqual("-- Selecione o Horário --")
                .WithMessage("Selecione uma opção de horário");


            RuleFor(x => x.IdSala)
            .GreaterThan(0).WithMessage("Selecione uma Sala Válida.");
        }
    }
}
