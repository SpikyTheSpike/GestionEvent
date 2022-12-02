using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IInscriptionService
    {
        public IEnumerable<Inscription> getInscriptionList(int Event_Id, int Member_Id);
        public IEnumerable<Inscription> getInscriptionByMember(int Member_Id);
        public Inscription? createInscription(Inscription data, int Member_Id);
        IEnumerable<Inscription> getAllInscription();
        void DeleteAdmin(int ide);
    }
}
