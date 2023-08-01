using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models
{
    public class CodeSnippetModel
    {
        [Key]
        public Guid idCodeSnippet { get; set; }

        public string Title { get; set; }

        public string ContentCode { get; set; }

        public Guid idMember { get; set; }

        [Range(1, 100, ErrorMessage = "Revision poate fi intre 1 si 100")]
        public int Revision { get; set; }

        public DateTime DateTimeAdded { get; set; }

        //[Range(typeof(bool), "true", "false",  ErrorMessage = "Trebuie sa selectati cel putin o optiune")]
        [Display(Name = "Is Published")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool IsPublished { get; set; }
    }
}
