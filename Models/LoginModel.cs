﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DeD_InfoServices.Models
{
    [Table("Login")]
    public class LoginModel
    {
        [Key]
        [Column("ide_login")]
        public int Ide_Login { get; set; }

        [Column("ide_usuario")]
        public UsuarioModel Ide_usuario { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("senha")]
        public string Senha { get; set; }

    }
}
