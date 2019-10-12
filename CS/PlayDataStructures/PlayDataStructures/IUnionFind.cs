namespace PlayDataStructures
{
	public interface IUnionFind
	{
		int Size { get; }
		bool IsConnected(int p, int q);
		void UnionElements(int p, int q);
	}
}
