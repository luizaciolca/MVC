using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models
{
    public class MembershipsModel
    {
        [Key]
        public Guid idMembership { get; set; }

        public Guid idMember { get; set; }

        public Guid idMembershipType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Level { get; set; }
    }
}
