using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Evenement
    {
        public int Event_Id { get; set; }
        public int Member_Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Photo{ get; set; }
        public int LimitPlace { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastModif { get; set; }
    }
}
