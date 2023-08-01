using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MariaCiolca_MVC.Models;

namespace MariaCiolca_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MariaCiolca_MVC.Models.AnnoucementModel>? AnnoucementModel { get; set; }
        public DbSet<MariaCiolca_MVC.Models.CodeSnippetModel>? CodeSnippetModel { get; set; }

        public DbSet<MariaCiolca_MVC.Models.MemberModel> Members { get; set; }

        public DbSet<MariaCiolca_MVC.Models.MembershipTypesModel> MembershipTypeModel { get; set; }

        public DbSet<MariaCiolca_MVC.Models.MembershipsModel> MembershipsModel { get; set; }

       


    }
}
