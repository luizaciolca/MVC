using MariaCiolca_MVC.Models.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models
{
    public class AnnoucementModel
    {
        [Key]
        public Guid IdAnnouncement { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")] 
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        [DateMustBeValid("ValidFrom", ErrorMessage = "Data de sfarsit trebuie sa fie dupa data de inceput")]
        public DateTime ValidTo {get; set;}

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        [StringLength(250, MinimumLength =5, ErrorMessage = "Campul Title poate sa contina minim 5 caractere si maxim 250 caractere!")]
        public string Title { get; set;}

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Campul Text poate sa contina minim 5 caractere si maxim 250 caractere!")]
        public string Text { get; set;}

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        public DateTime EventDate { get; set;}

        [Required(ErrorMessage = "Acest camp este obligatoriu!")]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Campul Tags poate sa contina minim 5 caractere si maxim 250 caractere!")]
        public string Tags { get; set;} 
    }
}
