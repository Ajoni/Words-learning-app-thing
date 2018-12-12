using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words_learning_app_thing.Models;

namespace Words_learning_app_thing.Logic.Interfaces
{
	interface ISessionBuilder
	{
		void Reset();
		void SetLanguageToLearn(Language language);
		void SetKnownLanguage(Language language);
		void SetTestStrategy();
		void SetLearnStrategy();
		void SetEasy();
		void SetMedium();
		void SetHard();
		void SetVeryHard();
		Session BuiltSession { get; }
	}
}
