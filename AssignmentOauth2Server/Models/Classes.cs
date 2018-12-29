﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentOauth2Server.Models
{
    public class Classes
    {
        [Key]
        [ForeignKey("Account")]
        public long OwnerId { get; set; }

        [Key]
        [ForeignKey("Class")]
        public string ClassId { get; set; }
    }
}