using System;

namespace PlayDataStructures
{
	public class UnionFind : IUnionFind
	{
		#region Fields and Properties

		// rank[i]表示以i为根的集合所表示的树的层数
		// 在后续的代码中, 我们并不会维护rank的语意, 也就是rank的值在路径压缩的过程中, 有可能不在是树的层数值
		// 这也是我们的rank不叫height或者depth的原因, 他只是作为比较的一个标准
		private int[] rank;

		private int[] parent; // parent[i]表示第i个元素所指向的父节点

		public int Size
		{
			get
			{
				return parent.Length;
			}

		}

		#endregion Fields and Properties

		#region Constructors

		public UnionFind(int size)
		{
			rank = new int[size];
			parent = new int[size];

			for (int i = 0; i < size; i++)
			{
				parent[i] = i;
				rank[i] = 1;
			}
		}

		#endregion Constructors

		#region Methods

		// 查找过程, 查找元素p所对应的集合编号
		// O(h)复杂度, h为树的高度
		private int Find(int p)
		{
			if (p < 0 || p >= Size)
				throw new ArgumentException("The p is out of bound.");
			while (p != parent[p])
			{
				parent[p] = parent[parent[p]];
				p = parent[p];
			}

			// Another way for path compress.
			// if (p != parent[p])
			//	 parent[p] = Find(parent[p]);
			return p;
		}

		// 查看元素p和元素q是否所属一个集合
		// O(h)复杂度, h为树的高度
		public bool IsConnected(int p, int q)
		{
			return Find(p) == Find(q);
		}

		// 合并元素p和元素q所属的集合
		// O(h)复杂度, h为树的高度
		public void UnionElements(int p, int q)
		{
			int pRoot = Find(p);
			int qRoot = Find(q);

			if (pRoot == qRoot)
				return;

			// 根据两个元素所在树的rank不同判断合并方向
			// 将rank低的集合合并到rank高的集合上
			if (rank[pRoot] < rank[qRoot])
				parent[pRoot] = qRoot;
			else if (rank[pRoot] > rank[qRoot])
				parent[qRoot] = pRoot;
			else // rank[pRoot] == rank[qRoot]
			{
				parent[pRoot] = qRoot;
				rank[qRoot] += 1;
			}
		}

		#endregion Methods
	}
}
