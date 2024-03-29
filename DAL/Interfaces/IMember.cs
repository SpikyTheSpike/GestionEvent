﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMember : IRepositoryBase<int, Member>
    {
        Member? getByIdentifiant(string identifiant);
        Member? getByIdentifiantAdmin(string identifiant);
        Member? getByIdentifiant(string pseudo, string email);
        Member? GetByEmail(string Email);

        string? GetHashPwd(string identifiant);
        void UpdateProfile(Member data, int id);
        IEnumerable<Member> getListeCompte();
        void DeleteAdmin(int ide);
    }
}
