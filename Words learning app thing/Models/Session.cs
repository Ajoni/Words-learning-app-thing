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
        public SessionStrategy Strategy { get; set; }
		public Lvl Lvl { get; set; }

		public Language KnownLanguage { get; set; }
		public Language LearningLanguage { get; set; }
		public ApplicationUser User { get; set; }
        public List<Question> Questions { get; set; }

		public ISessionIterator GetEnumerator()
		{
			throw new NotImplementedException();
		}

		class SessionIterator : ISessionIterator
		{
			private int _index { get; set; }
			private Session _session{ get; set; }
			private SessionStrategy _strategy{ get; set; }

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
				throw new NotImplementedException();
			}

			public bool HasNext()
			{
				throw new NotImplementedException();
			}

			public bool HasPrev()
			{
				throw new NotImplementedException();
			}

			public void Next()
			{
				throw new NotImplementedException();
			}

			public void Prev()
			{
				throw new NotImplementedException();
			}
		}
	}
}