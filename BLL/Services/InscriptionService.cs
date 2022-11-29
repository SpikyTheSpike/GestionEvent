using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InscriptionService : IInscriptionService
    {
        private IInscription _inscriptionRepository;
        public InscriptionService(IInscription inscriptionRepository)
        {
            _inscriptionRepository = inscriptionRepository;
        }

        public Inscription? createInscription(Inscription data, int Member_Id)
        {
            data.Member_Id = Member_Id;
            int id = _inscriptionRepository.Add(data);
            return _inscriptionRepository.GetById(id);
        }

        public IEnumerable<Inscription> getInscriptionByMember(int Member_Id)
        {
            return _inscriptionRepository.getMyInscription(Member_Id) ;
        }

        public IEnumerable<Inscription> getInscriptionList(int Event_Id, int Member_Id)
        {
           return  _inscriptionRepository.getInscriptionByEvent(Event_Id, Member_Id) ;
        }
    }
}
