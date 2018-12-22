using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Data.Repositories
{
    public class JezykRepo : IDisposable
    {
        private ApplicationDbContext context;        

        public JezykRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Jezyk Get(int Id)
        {
            return context.Jezyki.Find(Id);
        }

        public List<Jezyk> GetAll()
        {
            return context.Jezyki.ToList();
        }

        public void Add(Jezyk jezyk)
        {
            context.Jezyki.Add(jezyk);
        }

        public void Update(Jezyk jezyk)
        {
            context.Entry(jezyk).State = EntityState.Modified;
        }
        
        public void Remove(Jezyk jezyk)
        {
            context.Jezyki.Remove(jezyk);
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