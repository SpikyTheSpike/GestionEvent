using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Inscription
    {
        public int Inscription_Id { get; set; }
        public int Member_Id { get; set; }
        public int Event_Id { get; set; }
        public int NombrePlace { get; set; }
        public string Remarque { get; set; }
    }
}
