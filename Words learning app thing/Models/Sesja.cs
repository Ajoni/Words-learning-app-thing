using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Helpers;

namespace Words_learning_app_thing.Models
{
    public class Sesja
	{
        public int Id { get; set; }
        public int Wynik { get; set; }
		public TypSesji TypSesji { get; set; }
        public IStrategiaSesji Strategia { get; set; }
		public Poziom Poziom { get; set; }

		public Jezyk ZnanyJezyk { get; set; }
		public Jezyk UczonyJezyk { get; set; }
		public ApplicationUser Uzytkownik { get; set; }
        public List<Pytanie> Pytania { get; set; }

		public ISessionIterator GetEnumerator()
		{
			return new SessionIterator(this);
		}

		class SessionIterator : ISessionIterator
		{
			private int _index { get; set; }
			private Sesja _sesja{ get; set; }
			private IStrategiaSesji _strategia{ get; set; }

			public SessionIterator(Sesja sesja)
			{
				_sesja = sesja;
			}
			public void First()
			{
                _index = 0;
			}

			public Pytanie GetCurrent()
			{
				return _sesja.Pytania[_index];
			}

			public bool HasNext()
			{
				return _index < _sesja.Pytania.Count;
			}

			public bool HasPrev()
			{
				return _index > 0;
			}

			public void Next()
			{
				if (_strategia.CzyMozeDoKolejnego(_sesja.Pytania[_index]))
					_index++;
				throw new NotImplementedException();
			}

			public void Prev()
			{
				_index--;
				throw new NotImplementedException();
			}
		}
	}
}