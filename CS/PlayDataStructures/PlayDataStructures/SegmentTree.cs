using System;
using System.Text;

namespace PlayDataStructures
{
	public delegate T Merge<T>(T a, T b);

	public class SegmentTree<T>
	{
		#region Fields and Properties

		private T[] tree;

		private T[] data;

		private Merge<T> merger;

		public int Size
		{
			get
			{
				return data.Length;
			}
		}

		#endregion Fields and Properties

		#region Constructors

		public SegmentTree(T[] arr, Merge<T> merger)
		{
			this.merger = merger;

			data = new T[arr.Length];
			Array.Copy(arr, data, arr.Length);

			tree = new T[arr.Length * 4];
			BuildSegmentTree(0, 0, arr.Length - 1);
		}

		#endregion Constructors

		#region Methods

		public T Get(int index)
		{
			if (index < 0 || index >= data.Length)
				throw new ArgumentException("Index is illegal.");
			return data[index];
		}

		// 在treeIndex的位置创建表示区间[l...r]的线段树
		private void BuildSegmentTree(int treeIndex, int l, int r)
		{
			if (l == r)
			{
				tree[treeIndex] = data[l];
				return;
			}

			int leftTreeIndex = 2 * treeIndex + 1;
			int rightTreeIndex = 2 * treeIndex + 2;
			int mid = l + (r - l) / 2;

			BuildSegmentTree(leftTreeIndex, l, mid);
			BuildSegmentTree(rightTreeIndex, mid + 1, r);

			tree[treeIndex] = merger(tree[leftTreeIndex], tree[rightTreeIndex]);
		}

		// 返回区间[queryL, queryR]的值
		public T Query(int queryL, int queryR)
		{
			if (queryL < 0 || queryL >= data.Length || queryR < 0
				|| queryR >= data.Length || queryL > queryR)
				throw new ArgumentException("Index is illegal.");

			return Query(0, 0, data.Length - 1, queryL, queryR);
		}

		// 在以treeIndex为根的线段树中[l...r]的范围里，搜索区间[queryL...queryR]的值
		private T Query(int treeIndex, int l, int r, int queryL, int queryR)
		{
			if (l == queryL && r == queryR)
				return tree[treeIndex];

			int mid = l + (r - l) / 2;
			// treeIndex的节点分为[l...mid]和[mid+1...r]两部分

			int leftTreeIndex = 2 * treeIndex + 1;
			int rightTreeIndex = 2 * treeIndex + 2;

			if (queryL > mid)
				return Query(rightTreeIndex, mid + 1, r, queryL, queryR);
			else if (queryR <= mid)
				return Query(leftTreeIndex, l, mid, queryL, queryR);

			T leftRes = Query(leftTreeIndex, l, mid, queryL, mid);
			T rightRes = Query(rightTreeIndex, mid + 1, r, mid + 1, queryR);
			return merger(leftRes, rightRes);
		}

		public void Set(int index, T val)
		{
			if (index < 0 || index >= data.Length)
				throw new ArgumentException("Index is illegal.");

			data[index] = val;
			Set(0, 0, data.Length - 1, index, val);
		}

		// 在以treeIndex为根的线段树中更新index的值为e
		private void Set(int treeIndex, int l, int r, int index, T val)
		{
			if (l == r)
			{
				tree[treeIndex] = val;
				return;
			}

			int mid = l + (r - l) / 2;

			int leftTreeIndex = 2 * treeIndex + 1;
			int rightTreeIndex = 2 * treeIndex + 2;

			if (index > mid)
				Set(rightTreeIndex, mid + 1, r, index, val);
			else
				Set(leftTreeIndex, l, mid, index, val);

			tree[treeIndex] = merger(tree[leftTreeIndex], tree[rightTreeIndex]);
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append("[");
			for (int i = 0; i < tree.Length; i++)
			{
				if (tree[i] != null)
					res.Append(tree[i]);
				else
					res.Append("NULL");

				if (i != tree.Length - 1)
					res.Append(", ");
			}
			res.Append("]");
			return res.ToString();
		}

		#endregion Methods
	}
}
