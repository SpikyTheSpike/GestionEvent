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

        public IEnumerable<Evenement> SeeEveryEvent()
        {
            return _eventRepository.GetAll();
        }
    }
}
