using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IEvenetService
    {
        public Evenement? CreateNewEvent(Evenement data , int id);

        public IEnumerable<Evenement> SeeEveryEvent();
        public IEnumerable<Evenement> SeeFuturEvent();

        public bool DeleteOneOfMyEvent(int eventId, int memberId);
        public int CancelOneOfMyEvent(int eventId, int memberId);
        public int UnCancelOneOfMyEvent(int eventId, int memberId);

    }
}
