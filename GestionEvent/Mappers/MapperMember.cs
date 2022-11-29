using Domain.Entities;
using GestionEvent.Models;

namespace GestionEvent.Mappers
{
    public static class MapperMember
    {
        public static Member ToBLL(this AuthRegisterViewModel f)
        {
            return new Member
            {
                Email = f.Email,
                Psw = f.Password,
                Pseudo = f.Pseudo,
                FirstName = f.FirstName,
                LastName = f.LastName,
                BirthDate = f.Birthdate
            };
        }


        public static ConnectedUserDTO ToDTO(this Member member)
        {
            return new ConnectedUserDTO
            {
                Email = member.Email,
                Pseudo = member.Pseudo,
                MemberId = member.MemberId,
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate
            };
        }
    }
}
