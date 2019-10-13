using System;

namespace PlayDataStructures
{
	public class RedBlackTree<K, V> where K : IComparable<K>
	{
		#region Internal Class

		private static readonly bool RED = true;
		private static readonly bool BLACK = false;

		private class Node
		{
			public K key;
			public V value;
			public Node left, right;
			public bool color;

			public Node(K key, V value)
			{
				this.key = key;
				this.value = value;
				left = right = null;
				color = RED;
			}
		}

		#endregion Internal Class

		#region Fields and Properties

		private Node root;

		public int Size { get; private set; }

		public bool IsEmpty
		{
			get { return Size == 0; }
		}

		#endregion Fields and Properties

		#region Constructors

		public RedBlackTree()
		{
			root = null;
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		// 判断节点node的颜色
		private bool IsRed(Node node)
		{
			return node == null ? BLACK : node.color;
		}

		// 返回以node为根节点的红黑树中，key所在的节点
		private Node GetNode(Node node, K key)
		{
			if (node == null)
				return null;

			if (key.Equals(node.key))
				return node;
			else if (key.CompareTo(node.key) < 0)
				return GetNode(node.left, key);
			else // key > node.key
				return GetNode(node.right, key);
		}

		public bool Contains(K key)
		{
			return GetNode(root, key) != null;
		}

		public V this[K key]
		{
			get
			{
				Node node = GetNode(root, key);
				return node == null ? default(V) : node.value;
			}
			set
			{
				Node node = GetNode(root, key);
				if (node == null)
					throw new ArgumentException($"Key: {key} doesn't exist.");
				node.value = value;
			}
		}

		// 向红黑树中添加新的元素(key, value)
		public void Add(K key, V value)
		{
			root = Add(root, key, value);
			root.color = BLACK; // 最终根节点为黑色节点
		}

		// 向以node为根的红黑树中插入元素(key, value)，递归算法
		// 返回插入新节点后红黑树的根
		private Node Add(Node node, K key, V value)
		{
			if (node == null)
			{
				Size++;
				return new Node(key, value); // 默认插入红色节点
			}

			if (key.CompareTo(node.key) < 0)
				node.left = Add(node.left, key, value);
			else if (key.CompareTo(node.key) > 0)
				node.right = Add(node.right, key, value);
			else
				node.value = value;

			if (IsRed(node.right) && !IsRed(node.left))
				node = LeftRotate(node);

			if (IsRed(node.left) && IsRed(node.left.left))
				node = RightRotate(node);

			if (IsRed(node.left) && IsRed(node.right))
				FlipColors(node);

			return node;
		}

		//     node                   x
		//    /   \     右旋转       /  \
		//   x    T2   ------->   y   node
		//  / \                       /  \
		// y  T1                     T1  T2
		private Node RightRotate(Node node)
		{
			Node x = node.left;

			node.left = x.right;
			x.right = node;

			x.color = node.color;
			node.color = RED;

			return x;
		}

		//   node                     x
		//  /   \     左旋转         /  \
		// T1   x   --------->   node   T3
		//     / \              /   \
		//    T2 T3            T1   T2
		private Node LeftRotate(Node node)
		{
			Node x = node.right;

			node.right = x.left;
			x.left = node;

			x.color = node.color;
			node.color = RED;

			return x;
		}

		// 颜色翻转
		private void FlipColors(Node node)
		{
			node.color = RED;
			node.left.color = BLACK;
			node.right.color = BLACK;
		}

		private Node Minimum(Node node)
		{
			return node.left == null ? node : Minimum(node.left);
		}

		private Node Maximum(Node node)
		{
			return node.right == null ? node : Maximum(node.right);
		}

		#endregion Methods
	}
}
