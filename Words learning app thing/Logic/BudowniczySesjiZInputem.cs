using System.Collections.Generic;
using System.Linq;
using Words_learning_app_thing.Data.Repositories;
using Words_learning_app_thing.Logic.Abstract;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic
{
    public class BudowniczySesjiZInputem : BudowniczySesji
	{
        public BudowniczySesjiZInputem(SlowoRepo slowoRepo)
        {
            slowoRepo = _slowoRepo;
        }

		public override void UstawLatwyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(0, 5, this.BudowanaSesja.UczonyJezyk);
		}

        public override void UstawSredniZestaw()
        {
            this.BudowanaSesja.Pytania = getPytania(2, 5, this.BudowanaSesja.UczonyJezyk);
        }

        public override void UstawTrudnyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(4, 5, this.BudowanaSesja.UczonyJezyk);
        }		

		public override void UstawBardzoTrudnyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(6, 5, this.BudowanaSesja.UczonyJezyk);
        }

        private List<Pytanie> getPytania(int minLength, int toTake, Jezyk jezykUczony)
        {
            var slowa = _slowoRepo.GetShuffled(minLength, toTake, jezykUczony);
            return slowa.Select(s => (Pytanie)new PytanieZInputem(s, jezykUczony)).ToList();
        }
    }
}