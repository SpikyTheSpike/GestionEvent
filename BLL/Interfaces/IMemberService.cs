using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMemberService
    {
        public Member? Register(Member data);

        public Member? Login(string identifiant, string mdp);

    }
}
