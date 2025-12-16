using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EspacoTech.Models;
using Microsoft.AspNetCore.Identity;

namespace EspacoTech.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Usuario class
public class Usuario : IdentityUser
{
    public string NomeUsuario { get; set; }

    // Propriedade de Navegação: Lista de reservas feitas por este usuário
    public ICollection<Reserva> Reservas { get; set; }

}

