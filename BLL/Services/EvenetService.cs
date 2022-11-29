using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.Exceptions.EventException;

namespace BLL.Services
{
    public class EvenetService : IEvenetService
    {
        private IEvenet _eventRepository;
        public EvenetService(IEvenet eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public int CancelOneOfMyEvent(int eventId, int memberId)
        {
            return _eventRepository.Cancel(eventId, memberId);
        }

        public Evenement? CreateNewEvent(Evenement data, int MemberId)
        {
            if (string.IsNullOrWhiteSpace(data.Nom))
                throw new NameIsNotValidException();

            if (string.IsNullOrWhiteSpace(data.Photo))
                throw new PhotoIsNotValidException();

            data.Member_Id = MemberId;
            int id = _eventRepository.Add(data);
            return _eventRepository.GetById(id);
        }

        public bool DeleteOneOfMyEvent(int eventId, int memberId)
        {
            return _eventRepository.Delete(eventId,memberId);
        }

        public IEnumerable<Evenement> SeeEveryEvent()
        {
            return _eventRepository.GetAll();
        }

        public IEnumerable<Evenement> SeeFuturEvent()
        {
            return _eventRepository.GetFutur();
        }

        public int UnCancelOneOfMyEvent(int eventId, int memberId)
        {
            return _eventRepository.UnCancel(eventId, memberId);
        }
    }
}
