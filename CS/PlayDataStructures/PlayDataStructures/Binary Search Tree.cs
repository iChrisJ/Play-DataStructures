using System;
using System.Collections.Generic;

namespace PlayDataStructures
{
	public class BinarySearchTree<T> where T : IComparable<T>
	{
		#region Internal Class

		private class Node
		{
			public T val;
			public Node left, right;

			public Node(T val)
			{
				this.val = val;
				left = right = null;
			}
		}

		private class Command
		{
			public bool PushOrPrint { get; private set; }
			public Node Node { get; private set; }

			public Command(bool pushOrPrint, Node node)
			{
				PushOrPrint = pushOrPrint;
				Node = node;
			}
		}

		#endregion Internal Class

		#region Fields and Properties

		private Node root;

		public int Size { get; private set; }

		public bool IsEmpty
		{
			get
			{
				return Size == 0;
			}
		}

		#endregion Fields and Properties

		#region Constructors

		public BinarySearchTree()
		{
			root = null;
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		// 向二分搜索树中添加新的元素val
		public void Add(T val)
		{
			root = Add(root, val);
		}

		// 向以node为根的二分搜索树中插入元素val，递归算法
		// 返回插入新节点后二分搜索树的根
		private Node Add(Node node, T val)
		{
			if (node == null)
			{
				Size++;
				return new Node(val);
			}

			if (val.CompareTo(node.val) < 0)
				node.left = Add(node.left, val);
			else if (val.CompareTo(node.val) > 0)
				node.right = Add(node.right, val);
			// if val == node.val,不处理
			return node;
		}

		// 看二分搜索树中是否包含元素val
		private bool Contains(T val)
		{
			return Contains(root, val);
		}

		// 看以node为根的二分搜索树中是否包含元素val, 递归算法
		private bool Contains(Node node, T val)
		{
			if (node == null)
				return false;

			if (val.CompareTo(node.val) < 0)
				return Contains(node.left, val);
			else if (val.CompareTo(node.val) > 0)
				return Contains(node.right, val);
			else // val == node.val;
				return true;
		}

		public void PreOrder()
		{
			PreOrder(root);
		}

		// 前序遍历以node为根的二分搜索树, 递归算法
		private void PreOrder(Node node)
		{
			if (node == null)
				return;

			Console.WriteLine(node.val);
			PreOrder(node.left);
			PreOrder(node.right);
		}

		public void PreOrderTraversal()
		{
			if (root == null)
				return;

			Stack<Node> stack = new Stack<Node>();
			stack.Push(root);

			while (stack.Count != 0)
			{
				Node top = stack.Pop();
				Console.WriteLine(top.val);

				if (top.right != null)
					stack.Push(top.right);
				if (top.left != null)
					stack.Push(top.left);
			}
		}

		public void InOrder()
		{
			InOrder(root);
		}

		// 中序遍历以node为根的二分搜索树, 递归算法
		private void InOrder(Node node)
		{
			if (node == null)
				return;

			InOrder(node.left);
			Console.WriteLine(node.val);
			InOrder(node.right);
		}

		public void InOrderTraversal()
		{
			if (root == null)
				return;

			Stack<Node> stack = new Stack<Node>();
			Node cur = root;

			while (cur != null || stack.Count != 0)
			{
				if (cur != null)
				{
					stack.Push(cur);
					cur = cur.left;
				}
				else
				{
					cur = stack.Pop();
					Console.WriteLine(cur.val);
					cur = cur.right;
				}
			}
		}

		public void PostOrder()
		{
			PostOrder(root);
		}

		// 后序遍历以node为根的二分搜索树, 递归算法
		private void PostOrder(Node node)
		{
			if (node == null)
				return;

			PostOrder(node.left);
			PostOrder(node.right);
			Console.WriteLine(node.val);
		}

		public void PostOrderTraversal()
		{
			if (root == null)
				return;

			Stack<Command> stack = new Stack<Command>();
			stack.Push(new Command(true, root));
			while (stack.Count != 0)
			{
				Command top = stack.Pop();

				if (top.PushOrPrint == true)
					stack.Push(new Command(false, top.Node));
				else
				{
					Console.WriteLine(top.Node.val);
					if (top.Node.right != null)
						stack.Push(new Command(true, top.Node.right));
					if (top.Node.left != null)
						stack.Push(new Command(true, top.Node.left));
				}
			}
		}

		public void LevelOrder()
		{
			if (root == null)
				return;

			Queue<Node> queue = new Queue<Node>();
			queue.Enqueue(root);

			while (queue.Count != 0)
			{
				Node front = queue.Dequeue();
				Console.WriteLine(front.val);

				if (front.left != null)
					queue.Enqueue(front.left);
				if (front.right != null)
					queue.Enqueue(front.right);
			}
		}


		public T Minimum()
		{
			if (Size == 0)
				throw new ArgumentException("The Binary Search Treee is empty.");

			Node minNode = Minimum(root);
			return minNode.val;
		}


		private Node Minimum(Node node)
		{
			if (node.left == null)
				return node;
			return Minimum(node.left);
		}
		public T Maximum()
		{
			if (Size == 0)
				throw new ArgumentException("The Binary Search Treee is empty.");

			Node maxNode = Maximum(root);
			return maxNode.val;
		}

		private Node Maximum(Node node)
		{
			if (node.right == null)
				return node;
			return Maximum(node.right);
		}

		public T RemoveMin()
		{
			T ret = Minimum();
			root = RemoveMin(root);
			return ret;
		}

		private Node RemoveMin(Node node)
		{
			if (node.left == null)
			{
				Node rightNode = node.right;
				node.right = null;
				Size--;
				return rightNode;
			}

			node.left = RemoveMin(node.left);
			return node;
		}

		public T RemoveMax()
		{
			T ret = Maximum();
			root = RemoveMax(root);
			return ret;
		}

		private Node RemoveMax(Node node)
		{
			if (node.right == null)
			{
				Node leftNode = node.left;
				node.left = null;
				Size--;
				return leftNode;
			}

			node.right = RemoveMax(node.right);
			return node;
		}

		public void Remove(T val)
		{
			root = Remove(root, val);
		}

		private Node Remove(Node node, T val)
		{
			if (node == null)
				return null;

			if (val.CompareTo(node.val) < 0)
			{
				node.left = Remove(node.left, val);
				return node;
			}
			else if (val.CompareTo(node.val) < 0)
			{
				node.right = Remove(node.right, val);
				return node;
			}
			else
			{
				if (node.left == null)
				{
					Node rightNode = node.right;
					node.right = null;
					Size--;
					return rightNode;
				}

				if (node.right == null)
				{
					Node leftNode = node.left;
					node.left = null;
					Size--;
					return leftNode;
				}

				// 待删除节点左右子树均不为空的情况

				// 找到比待删除节点大的最小节点, 即待删除节点右子树的最小节点
				// 用这个节点顶替待删除节点的位置
				Node successor = Minimum(node.right);
				successor.right = RemoveMin(node.right);
				successor.left = node.left;

				node.left = node.right = null;
				return successor;
			}
		}

		#endregion Methods
	}
}
