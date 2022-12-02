using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IInscription : IRepositoryBase<int, Inscription>
    {
        IEnumerable<Inscription> getMyInscription(int Member_Id);
        IEnumerable<Inscription> getInscriptionByEvent(int Event_Id, int Member_Id);
        IEnumerable<Inscription> getAllInscription();
        void DeleteAdmin(int ide);
    }
}
