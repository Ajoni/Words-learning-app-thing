using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Data.Repositories
{
    public class UserRepo : IDisposable
    {
        private ApplicationDbContext context;

        public UserRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        
        public ApplicationUser Get(string Id)
        {            
            return context.Uzytkownicy.Find(Id);
        }

        public void Add(ApplicationUser uzytkownik)
        {
            context.Uzytkownicy.Add(uzytkownik);
        }

        public void Update(ApplicationUser uzytkownik)
        {
            context.Entry(uzytkownik).State = EntityState.Modified;
        }

        public void Remove(ApplicationUser uzytkownik)
        {
            context.Uzytkownicy.Remove(uzytkownik);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
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