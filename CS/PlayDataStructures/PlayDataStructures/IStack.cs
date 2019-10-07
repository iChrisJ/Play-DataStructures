namespace PlayDataStructures
{
	public interface IStack<T>
	{
		int Size { get; }
		bool IsEmpty { get; }
		void Push(T e);
		T Pop();
		T Peek();
	}
}
