using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Words_learning_app_thing.Models
{
	public class TestStrategy : SessionStrategy
	{
		public bool CanGoToNext(Question current)
		{
			return true;
		}
	}
}