using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models
{
    public class MemberModel
    {
        [Key]
        public Guid IdMember { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        public string Position { get; set; }    
        public string Resume { get; set; }

        [DisplayFormat(DataFormatString = "{0}")]
        public string UserName { get; set; }

        [DisplayFormat(DataFormatString = "{0}")]
        public int UserNameLength => UserName?.Length ?? 0;

        /* [DisplayFormat(DataFormatString = "*****")]
         public string Password { get; set; } */

        [DisplayFormat(DataFormatString = "{0}")]
        public string Password { get; set; }

        [DisplayFormat(DataFormatString = "{0}")]
        public int PasswordLength => Password?.Length ?? 0;

        internal List<MemberModel> ToList()
        {
            throw new NotImplementedException();
        }
    }
}
