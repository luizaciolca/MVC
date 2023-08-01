using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace MariaCiolca_MVC.Data
{
    public class ClubLibraContext : DbContext
    {
        public ClubLibraContext(DbContextOptions<ClubLibraContext> options) : base(options) { }

        public DbSet<AnnoucementModel> Announcements { get; set; }

        public DbSet<CodeSnippetModel> CodeSnippets { get; set; }

        public DbSet<MemberModel> Members { get; set; }

        public DbSet<MembershipTypesModel> MembershipTypes { get; set; }

        public DbSet<MembershipsModel> Memberships { get; set; }


    }

   

    

 


}
