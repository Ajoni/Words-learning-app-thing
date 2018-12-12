using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Words_learning_app_thing.Logic.Interfaces;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic.Abstract
{
	public abstract class SessionBuilder : ISessionBuilder
	{
		public Session BuiltSession { get; private set; } = new Session();

		public void Reset()
		{
			BuiltSession = new Session();
		}

		public abstract void SetEasy();
		public abstract void SetHard();

		public void SetKnownLanguage(Language language)
		{
			BuiltSession.KnownLanguage = language;
		}

		public void SetLanguageToLearn(Language language)
		{
			BuiltSession.LearningLanguage = language;
		}

		public void SetLearnStrategy()
		{
			BuiltSession.Strategy = new LearnStrategy();
		}

		public abstract void SetMedium();

		public void SetTestStrategy()
		{
			BuiltSession.Strategy = new TestStrategy();
		}

		public abstract void SetVeryHard();
	}
}