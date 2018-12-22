using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Data.Repositories
{
    public class SlowoRepo : IDisposable
    {
        private ApplicationDbContext context;

        public SlowoRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public Slowo Get(int Id)
        {
            return context.Slowa.Find(Id);
        }

        public List<Slowo> GetAll(Jezyk jezyk)
        {
            return (List<Slowo>)context.Slowa.ToList().Where(w => w.Jezyk == jezyk);
        }

        public List<Slowo> GetTlumaczenia(Slowo slowo, Jezyk jezyk)
        {
            return (List<Slowo>)slowo.Tlumaczenia.Where(w => w.Jezyk == jezyk);
        }

        public void Add(Slowo slowo)
        {
            context.Slowa.Add(slowo);
        }

        public void Update(Slowo slowo)
        {
            context.Entry(slowo).State = EntityState.Modified;
        }

        public void Remove(Slowo slowo)
        {
            context.Slowa.Remove(slowo);
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