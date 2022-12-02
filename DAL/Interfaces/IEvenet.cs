using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IEvenet : IRepositoryBase<int, Evenement>
    {
        Evenement? getById(int id);
        IEnumerable<Evenement> GetFutur();

        bool Delete(int id, int otherId);
        int Cancel(int eventId, int memberId);
        int UnCancel(int eventId, int memberId);
        void UpdateEvent(Evenement data, int id);
        IEnumerable<Evenement> GetAllAdmin(bool res);
        void DeleteAdmin(int ide);
    }
}
