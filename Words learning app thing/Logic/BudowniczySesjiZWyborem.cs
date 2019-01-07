using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Data.Repositories;
using Words_learning_app_thing.Logic.Abstract;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic
{
	public class BudowniczySesjiZWyborem : BudowniczySesji
	{
        public BudowniczySesjiZWyborem(SlowoRepo slowoRepo)
        {
            _slowoRepo = slowoRepo;
        }

        public override void UstawLatwyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(5, 1, this.BudowanaSesja.UczonyJezyk);
        }

        public override void UstawSredniZestaw()
        {
            this.BudowanaSesja.Pytania = getPytania(5, 2, this.BudowanaSesja.UczonyJezyk);
        }

        public override void UstawTrudnyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(5, 3, this.BudowanaSesja.UczonyJezyk);
        }		

		public override void UstawBardzoTrudnyZestaw()
		{
            this.BudowanaSesja.Pytania = getPytania(5, 4, this.BudowanaSesja.UczonyJezyk);
        }

        private List<Pytanie> getPytania(int toTake, int wrongToTake, Jezyk jezykUczony)
        {
            var slowa = _slowoRepo.GetShuffled(0, toTake, jezykUczony);
            return slowa
                .Select(s => (Pytanie)
                new PytanieZWyborem(s, jezykUczony, _slowoRepo.GetWrong(s.Id, wrongToTake, jezykUczony)))
                .ToList();
        }
    }
}