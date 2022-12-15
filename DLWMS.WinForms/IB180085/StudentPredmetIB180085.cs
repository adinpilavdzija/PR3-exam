﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLWMS.WinForms.IB180085
{
    [Table("StudentiPredmeti")]
    public class StudentPredmetIB180085
    {
        public int Id { get; set; }
        public virtual Student Student { get; set; }
        public virtual PredmetIB180085 Predmet { get; set; }
        public int Ocjena { get; set; }
        public DateTime DatumPolaganja { get; set; }
    }
}
