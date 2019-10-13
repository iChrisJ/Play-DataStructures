using System;

namespace PlayDataStructures
{
	public class AVLTree<K, V> where K : IComparable<K>
	{
		#region Internal Class

		private class Node
		{
			public K key;
			public V value;
			public Node left, right;
			public int height;

			public Node(K key, V value)
			{
				this.key = key;
				this.value = value;
				left = right = null;
				height = 1;
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

		public AVLTree()
		{
			root = null;
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		// 获得节点node的高度
		private int GetHeight(Node node)
		{
			return node == null ? 0 : node.height;
		}

		// 获得节点node的平衡因子
		private int GetBalanceFactor(Node node)
		{
			return node == null ? 0 : GetHeight(node.left) - GetHeight(node.right);
		}

		// 返回以node为根节点的二分搜索树中，key所在的节点
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

		// 向二分搜索树中添加新的元素(key, value)
		public void Add(K key, V value)
		{
			root = Add(root, key, value);
		}

		// 向以node为根的二分搜索树中插入元素(key, value)，递归算法
		// 返回插入新节点后二分搜索树的根
		private Node Add(Node node, K key, V value)
		{
			if (node == null)
			{
				Size++;
				return new Node(key, value);
			}

			if (key.CompareTo(node.key) < 0)
				node.left = Add(node.left, key, value);
			else if (key.CompareTo(node.key) > 0)
				node.right = Add(node.right, key, value);
			else
				node.value = value;

			// 更新height
			node.height = 1 + Math.Max(GetHeight(node.left), GetHeight(node.right));

			// 计算平衡因子
			int balanceFactor = GetBalanceFactor(node);

			if (balanceFactor > 1 && GetBalanceFactor(node.left) >= 0)
				return RightRotate(node);

			if (balanceFactor < -1 && GetBalanceFactor(node.right) <= 0)
				return LeftRotate(node);

			if (balanceFactor > 1 && GetBalanceFactor(node.left) < 0)
			{
				node.left = LeftRotate(node.left);
				return RightRotate(node);
			}

			if (balanceFactor < -1 && GetBalanceFactor(node.right) > 0)
			{
				node.right = RightRotate(node.right);
				return LeftRotate(node);
			}

			return node;
		}

		// 对节点y进行向右旋转操作，返回旋转后新的根节点x
		//        y                            x
		//       / \                         /   \
		//      x   T4    向右旋转 (y)       z     y
		//     / \       ------------->    / \   / \
		//    z   T3                     T1  T2 T3 T4
		//   / \
		// T1   T2
		private Node RightRotate(Node y)
		{
			Node x = y.left;
			Node T3 = x.right;

			// 向右旋转过程
			x.right = y;
			y.left = T3;

			y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));
			x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(y.right));

			return x;
		}

		// 对节点y进行向左旋转操作，返回旋转后新的根节点x
		//    y                             x
		//  /  \                          /   \
		// T1   x      向左旋转 (y)       y     z
		//     / \   --------------->   / \   / \
		//   T2  z                     T1 T2 T3 T4
		//      / \
		//     T3 T4
		private Node LeftRotate(Node y)
		{
			Node x = y.right;
			Node T2 = x.left;

			x.left = y;
			y.right = T2;

			y.height = 1 + Math.Max(GetHeight(y.left), GetHeight(y.right));
			x.height = 1 + Math.Max(GetHeight(x.left), GetHeight(x.right));

			return x;
		}

		// 从二分搜索树中删除键为key的节点
		public V Remove(K key)
		{
			Node node = GetNode(root, key);
			if (node != null)
			{
				root = Remove(root, key);
				return node.value;
			}
			return default(V);
		}

		private Node Remove(Node node, K key)
		{
			if (node == null)
				return null;

			Node retNode;

			if (key.CompareTo(node.key) < 0)
			{
				node.left = Remove(node.left, key);
				retNode = node;
			}
			else if (key.CompareTo(node.key) > 0)
			{
				node.right = Remove(node.right, key);
				retNode = node;
			}
			else
			{
				if (node.left == null)
				{
					Node rightNode = node.right;
					node.right = null;
					Size--;
					retNode = rightNode;
				}
				else if (node.right == null)
				{
					Node leftNode = node.left;
					node.left = null;
					Size--;
					retNode = leftNode;
				}
				else
				{
					Node successor = Minimum(node.right);
					successor.right = Remove(node.right, successor.key);
					successor.left = node.left;

					node.left = node.right = null;
					retNode = successor;
				}
			}

			if (retNode == null)
				return null;

			// 更新height
			retNode.height = 1 + Math.Max(GetHeight(retNode.left), GetHeight(retNode.right));

			// 计算平衡因子
			int balanceFactor = GetBalanceFactor(retNode);

			if (balanceFactor > 1 && GetBalanceFactor(retNode.left) >= 0)
				return RightRotate(retNode);

			if (balanceFactor < -1 && GetBalanceFactor(retNode.right) <= 0)
				return LeftRotate(retNode);

			if (balanceFactor > 1 && GetBalanceFactor(retNode.left) < 0)
			{
				node.left = LeftRotate(retNode.left);
				return RightRotate(retNode);
			}

			if (balanceFactor < -1 && GetBalanceFactor(retNode.right) > 0)
			{
				node.right = RightRotate(retNode.right);
				return LeftRotate(retNode);
			}

			return retNode;
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
