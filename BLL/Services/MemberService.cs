﻿using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Domain.Entities;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BLL.Exceptions.IdentifiantException;

namespace BLL.Services
{
    public class MemberService : IMemberService
    {
        private IMember _memberRepository;

        public MemberService(IMember memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public void DeleteAdmin(int ide)
        {
            _memberRepository.DeleteAdmin(ide);
        }

        public IEnumerable<Member> getListCompte()
        {
            return _memberRepository.getListeCompte();
        }

        public Member? Login(string identifiant, string mdp)
        {
            if (string.IsNullOrWhiteSpace(identifiant) || string.IsNullOrWhiteSpace(mdp))
            {
                throw new ArgumentNullException();
            }

            string? hashPwd = _memberRepository.GetHashPwd(identifiant);
            if (hashPwd is null)
            {
                throw new IdentifiantNotExistException();
            }

            if (!Argon2.Verify(hashPwd, mdp))
            {
                throw new MotDePasseMauvaisException();
            }
            return _memberRepository.getByIdentifiant(identifiant);
        }

        public Member? LoginAdmin(string identifiant, string mdp)
        {
            if (string.IsNullOrWhiteSpace(identifiant) || string.IsNullOrWhiteSpace(mdp))
            {
                throw new ArgumentNullException();
            }

            string? hashPwd = _memberRepository.GetHashPwd(identifiant);
            if (hashPwd is null)
            {
                throw new IdentifiantNotExistException();
            }

            if (!Argon2.Verify(hashPwd, mdp))
            {
                throw new MotDePasseMauvaisException();
            }
            return _memberRepository.getByIdentifiantAdmin(identifiant);
        }

        public Member? Register(Member memberData)
        {
            if (string.IsNullOrWhiteSpace(memberData.Email) || string.IsNullOrWhiteSpace(memberData.Psw) || string.IsNullOrWhiteSpace(memberData.Pseudo))
            {
                throw new ArgumentNullException();
            }


            if (_memberRepository.getByIdentifiant(memberData.Pseudo, memberData.Email) != null)
            {
                throw new IdentifiantAlreadyExistException();
            }


           
            memberData.hashPsw = Argon2.Hash(memberData.Psw);
            memberData.Psw = null;
           

            int id = _memberRepository.Add(memberData);
            return _memberRepository.GetById(id);
        }

        public void UpdateProfile(Member data, int id)
        {
            data.hashPsw = Argon2.Hash(data.Psw);
            data.Psw = null;
            _memberRepository.UpdateProfile(data,id);
        }
    }
}
