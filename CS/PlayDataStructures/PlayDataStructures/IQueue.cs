namespace PlayDataStructures
{
	public interface IQueue<T>
	{
		int Size { get; }
		bool IsEmpty { get; }
		void Enqueue(T e);
		T Dequeue();
		T Front();
	}
}
