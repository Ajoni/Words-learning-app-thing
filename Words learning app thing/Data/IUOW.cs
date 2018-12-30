using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Words_learning_app_thing.Data.Repositories;

namespace Words_learning_app_thing.Data
{
	public interface IUOW
	{
		JezykRepo JezykRepo { get; }
		SlowoRepo SlowoRepo { get; }
		UserRepo UserRepo { get; }
		void SaveChanges();
	}
}
