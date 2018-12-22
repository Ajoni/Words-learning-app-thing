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

        public Word Get(int Id)
        {
            return context.Slowa.Find(Id);
        }

        public List<Word> GetAll(Language jezyk)
        {
            return (List<Word>)context.Slowa.ToList().Where(w => w.Language == jezyk);
        }

        public List<Word> GetTlumaczenia(Word slowo, Language jezyk)
        {
            return (List<Word>)slowo.Translations.Where(w => w.Language == jezyk);
        }

        public void Add(Word slowo)
        {
            context.Slowa.Add(slowo);
        }

        public void Update(Word slowo)
        {
            context.Entry(slowo).State = EntityState.Modified;
        }

        public void Remove(Word slowo)
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