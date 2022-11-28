using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Member
    {

        public int MemberId { get; set; }
        public string Email { get; set; }
        public string Pseudo { get; set; }
        public string Psw { get; set; }
        public string hashPsw { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
