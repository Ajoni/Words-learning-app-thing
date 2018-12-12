namespace Words_learning_app_thing.Models
{
	public interface ISessionIterator
	{
		void First();
		void Next();
		void Prev();
		bool HasPrev();
		bool HasNext();
		Question GetCurrent();
	}
}