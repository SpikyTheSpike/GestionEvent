using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class EventException : Exception
    {

        public EventException(string? message) : base(message)
        { }

        public class NameIsNotValidException : EventException
        {
            public NameIsNotValidException() : base("Nom non valide")
            {
            }

        }


        public class PhotoIsNotValidException : EventException
        {
            public PhotoIsNotValidException() : base("Photo ou chemin d'acces non valide")
           {
            }
        }

    }
}
