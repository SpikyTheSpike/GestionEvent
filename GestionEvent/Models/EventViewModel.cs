using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GestionEvent.Models
{
    public class EventViewModel
    {

        [Required(ErrorMessage = "Le nom est requis !")]
        [DisplayName("Nom")]
        public string Nom { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Date de début")]
        public DateTime DateDebut { get; set; }

        [Required]
        [DisplayName("Date de fin")]
        public DateTime DateFin { get; set; }

        [Required]
        [DisplayName("Photo")]
        public string Photo { get; set; }

        [DisplayName("Limite de place")]
        public int LimitPlace { get; set; }
    }

    public class EventUpdateViewModel
    {
        [Required(ErrorMessage = "L'event id est requis !")]
        [DisplayName("EventId")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Le nom est requis !")]
        [DisplayName("Nom")]
        public string Nom { get; set; }


        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Date de début")]
        public DateTime DateDebut { get; set; }

        [Required]
        [DisplayName("Date de fin")]
        public DateTime DateFin { get; set; }

        [Required]
        [DisplayName("Photo")]
        public string Photo { get; set; }

        [DisplayName("Limite de place")]
        public int LimitPlace { get; set; }
    }

    public class EventDeleteOrCancelViewModel
    {
        [Required(ErrorMessage = "L'event id est requis !")]
        [DisplayName("EventId")]
        public int EventId { get; set; }

    }
}
