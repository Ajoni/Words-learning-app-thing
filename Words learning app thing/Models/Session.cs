using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Helpers;

namespace Words_learning_app_thing.Models
{
    public class Session
	{
        public int Id { get; set; }
        public int Score { get; set; }
		public SessionType SessionType { get; set; }
        public ISessionStrategy Strategy { get; set; }
		public Lvl Lvl { get; set; }

		public Language KnownLanguage { get; set; }
		public Language LearningLanguage { get; set; }
		public ApplicationUser User { get; set; }
        public List<Question> Questions { get; set; }

		public ISessionIterator GetEnumerator()
		{
			return new SessionIterator(this);
		}

		class SessionIterator : ISessionIterator
		{
			private int _index { get; set; }
			private Session _session{ get; set; }
			private ISessionStrategy _strategy{ get; set; }

			public SessionIterator(Session session)
			{
				_session = session;
			}
			public void First()
			{
				throw new NotImplementedException();
			}

			public Question GetCurrent()
			{
				return _session.Questions[_index];
			}

			public bool HasNext()
			{
				if (!_strategy.CanGoToNext(_session.Questions[_index]))
					return false;
				return _index < _session.Questions.Count;
			}

			public bool HasPrev()
			{
				return _index > 0;
			}

			public void Next()
			{
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