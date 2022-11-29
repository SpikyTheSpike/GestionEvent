using Domain.Entities;
using GestionEvent.Models;

namespace GestionEvent.Mappers
{
    public static class MapperInscription
    {
        public static Inscription ToBLL(this InscriptionViewModel f)
        {
            return new Inscription
            {
                Event_Id = f.Event_Id,
                Remarque=f.Remarque,
                NombrePlace=f.NbrPlace
            };
        }
    }
}
