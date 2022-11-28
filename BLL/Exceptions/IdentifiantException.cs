using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Exceptions
{
    public class IdentifiantException : Exception
    {
        public IdentifiantException(string? message) : base(message)
        { }

        public class IdentifiantExceptionIsNotValidException : IdentifiantException
        {
            public IdentifiantExceptionIsNotValidException() : base("Identifiant non valide")
            {
            }
        }

        public class MotDePasseMauvaisException : IdentifiantException
        {
            public MotDePasseMauvaisException() : base("mot de passe non valide")
            {
            }
        }


        public class IdentifiantAlreadyExistException : IdentifiantException
        {
            public IdentifiantAlreadyExistException() : base("Identifiant déjà existant!")
            {
            }
        }

        public class IdentifiantNotExistException : IdentifiantException
        {
            public IdentifiantNotExistException() : base("Identifiant non existant!")
            {
            }
        }
    }
}
