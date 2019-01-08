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

        // Eagerly loading multiple levels here
        public Slowo Get(int Id)
        {
            return context.Slowa
                .Include(s => s.Jezyk)
                .Include(s => s.Tlumaczenia.Select(t => t.Jezyk))
                .Where(s => s.Id == Id)
                .FirstOrDefault();
        }

        public List<Slowo> GetAll()
        {
            return context.Slowa.Include(s => s.Jezyk).ToList();
        }

        public List<Slowo> GetAll(int jezykId)
        {
            return (List<Slowo>)context.Slowa.Include(s => s.Jezyk).Include(s => s.Tlumaczenia).Where(w => w.Jezyk.Id == jezykId).ToList();
        }

        public List<Slowo> GetAll(Jezyk jezyk)
        {
            return (List<Slowo>)context.Slowa.Include(s => s.Jezyk).Include(s => s.Tlumaczenia).Where(w => w.Jezyk.Id == jezyk.Id).ToList();
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

        public List<Slowo> GetShuffled(int minLength, int toTake, Jezyk jezyk)
        {
            return context.Slowa
                .Where(s => s.Zawartosc.Length >= minLength)
                .Where(s => s.Tlumaczenia.Any(tl => tl.Jezyk.Id == jezyk.Id))
                .Include(s => s.Tlumaczenia)
                .OrderBy(s => Guid.NewGuid())
                .Take(toTake)
                .ToList();
        }

        public List<Slowo> GetWrong(int excludedId, int toTake, Jezyk jezyk)
        {
            return context.Slowa
                .Where(s => s.Id != excludedId)
                .Where(s => s.Jezyk.Id == jezyk.Id)
                .OrderBy(s => Guid.NewGuid())
                .Take(toTake)
                .ToList();
        }

        public void Remove(Slowo slowo)
        {
            // Usunąć to słowo z tłumaczeń
            foreach(var s in slowo.Tlumaczenia)
            {
                Slowo tlumaczenie = Get(s.Id);
                tlumaczenie.Tlumaczenia.Remove(slowo);
            }
            // Usunąć tłumaczenia tego słowa
            slowo.Tlumaczenia.Clear();
            
            // Usunięcie samego słowa
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