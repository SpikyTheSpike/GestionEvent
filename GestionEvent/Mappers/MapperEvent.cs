using Domain.Entities;
using GestionEvent.Models;

namespace GestionEvent.Mappers
{
    public static class MapperEvent
    {

        public static Evenement ToBLL(this EventViewModel f)
        {
            return new Evenement
            {
                Nom = f.Nom,
                Description = f.Description,
                DateDebut = f.DateDebut,
                DateFin = f.DateFin,
                Photo= f.Photo,
                LimitPlace = f.LimitPlace
            };
        }
    }
}
