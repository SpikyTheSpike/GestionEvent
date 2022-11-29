using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GestionEvent.Models
{
    public class InscriptionViewModel
    {

        [Required(ErrorMessage = "Un event est requis !")]
        [DisplayName("EventId")]
        public int Event_Id { get; set; }


        [DisplayName("Nombre de Place")]
        public int NbrPlace { get; set; }

        [DisplayName("Remarque")]
        public string Remarque { get; set; }
    }

    public class SeeInscriptionViewModel
    {

        [Required(ErrorMessage = "Un event est requis !")]
        [DisplayName("EventId")]
        public int Event_Id { get; set; }
    }
}
