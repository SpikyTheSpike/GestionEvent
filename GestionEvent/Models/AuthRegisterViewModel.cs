using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionEvent.Models
{
   
        public class AuthRegisterViewModel
        {
            [Required(ErrorMessage = "L'e-mail est requis !")]
            [DisplayName("E-mail")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Required(ErrorMessage = "Le pseudo est requis !")]
            [DisplayName("Pseudo")]
            [MinLength(2, ErrorMessage = "le pseudo doit contenir minimum 2 caractères")]
            [MaxLength(50, ErrorMessage = "Le pseudo doit contenir maximum 50 caractères")]
            [RegularExpression("^[A-Za-z][A-Za-z0-9]*$", ErrorMessage = "Pas de symnbole :o")]
            public string Pseudo { get; set; }

            
            [DisplayName("Prénom")]
            public string FirstName { get; set; }

            [DisplayName("Nom")]
            public string LastName { get; set; }

            [DisplayName("Date de naissance")]
             public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis !")]
            [DisplayName("Mot de passe")]
            [DataType(DataType.Password)]
            [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).*$")]
            [MinLength(5, ErrorMessage = "le pseudo doit contenir minimum 5 caractères")]
            public string Password { get; set; }

            [Compare(nameof(Password), ErrorMessage = "La confirmation du mot de passe n'est pas valide !")]
            [Required(ErrorMessage = "La confirmation de mot de passe est requis !")]
            [DisplayName("Confirmation du mot de passe")]
            [DataType(DataType.Password)]
            public string ConfirmPassword { get; set; }
        }

        public class AuthLoginViewModel
        {
            [DisplayName("Pseudo / E-mail")]
            public string Identifiant { get; set; }


            [DisplayName("Mot de passe")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Le mot de passe est requis !")]
            public string Password { get; set; }

        }
    
}
