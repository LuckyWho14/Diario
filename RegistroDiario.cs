using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioPro
{
    internal class RegistroDiario
    {
        public DateTime Fecha { get; set; }
        public string? Nota { get; set; }

        public int lvlActiv { get; set; }
        public int lvlEnerg { get; set; }
    }
}
