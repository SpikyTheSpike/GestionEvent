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
    }
}
