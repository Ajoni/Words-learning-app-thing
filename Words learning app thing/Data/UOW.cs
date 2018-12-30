using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Data.Repositories;

namespace Words_learning_app_thing.Data
{
    // UOW as seen here: 
    // https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application#creating-the-student-repository-class
    //
    public class UOW : IDisposable, IUOW
	{
        private ApplicationDbContext _dbContext;

        private JezykRepo _jezykRepo;
        private SlowoRepo _slowoRepo;
        private UserRepo _userRepo;

        public UOW() 
        {
            _dbContext = new ApplicationDbContext();
        }

        public JezykRepo JezykRepo
        {
            get
            {
                if (_jezykRepo == null)
                {
                    _jezykRepo = new JezykRepo(_dbContext);
                }
                return _jezykRepo;
            }
        }

        public SlowoRepo SlowoRepo
        {
            get
            {
                if (_slowoRepo == null)
                {
                    _slowoRepo = new SlowoRepo(_dbContext);
                }
                return _slowoRepo;
            }
        }

        public UserRepo UserRepo
        {
            get
            {
                if (_userRepo == null)
                {
                    _userRepo = new UserRepo(_dbContext);
                }
                return _userRepo;
            }
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}