using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Packaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestProject1.Helpers
{
    public class DbContextHelper
    {
        private static readonly AnnoucementModel model;

        public static ClubLibraContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ClubLibraContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  //configurare si utilizarea unei baze de date in memorie
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var databaseContext = new ClubLibraContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        public static AnnoucementModel AddAnnouncement(ClubLibraContext dbContext, AnnoucementModel model)
        {
            dbContext.Add(model);
            dbContext.SaveChanges();
            return model;
        }

        internal static AnnoucementModel AddAnnouncement(ClubLibraContext contextInMemory)
        {
            throw new NotImplementedException();
        }
    }
}

