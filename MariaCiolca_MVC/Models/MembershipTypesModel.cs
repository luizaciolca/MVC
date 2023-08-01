using System.ComponentModel.DataAnnotations;

namespace MariaCiolca_MVC.Models
{
    public class MembershipTypesModel
    {
        [Key]
        public Guid idMembershipType { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int SubscriptionLengthInMonths { get; set; }
    }
}
