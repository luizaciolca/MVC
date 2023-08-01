using MariaCiolca_MVC.Data;
using MariaCiolca_MVC.Models;
using MariaCiolca_MVC.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.RepositoryTests
{
    public class AnnouncementRepositoryTests
    {
        private readonly AnnouncementRepository _repository;
        private readonly ClubLibraContext _contextInMemory;

        public AnnouncementRepositoryTests()
        {
            _contextInMemory = Helpers.DbContextHelper.GetDatabaseContext();
            _repository = new AnnouncementRepository(_contextInMemory);
        }

        [Fact]
        public void DeleteAnnouncement_AnnouncementNotExist()
        {
            //Arange -> voi crea un anunt fals
             Guid id = Guid.NewGuid();

            /*AnnoucementModel myAnnouncement = new AnnoucementModel
            {
                IdAnnouncement = id,
                ValidFrom = DateTime.UtcNow,
                ValidTo = DateTime.UtcNow,
                EventDate = DateTime.UtcNow,
                Title = "Anunt pentru a fi sters",
                Tags = "tags1", 
                Text = "Anunt de test"
                }; 

            AnnoucementModel dbAnnouncement = Helpers.DbContextHelper.AddAnnouncement(_contextInMemory);  */

                //Act 

                var resultBeforeDelete = _repository.GetAnnoucementById(id);
                _repository.Delete(id);
                var resultAfterDelete = _repository.GetAnnoucementById(id);
            

        //Assert
        Assert.NotNull(resultBeforeDelete);
            Assert.Null(resultAfterDelete); 

            }


        }
    }
