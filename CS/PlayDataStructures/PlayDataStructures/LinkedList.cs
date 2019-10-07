using System;
using System.Text;

namespace PlayDataStructures
{
	public class LinkedList<T>
	{
		#region Internal Class

		private class Node
		{
			public T val;
			public Node next;

			public Node(T val, Node next)
			{
				this.val = val;
				this.next = next;
			}

			public Node(T val) : this(val, null) { }

			public Node() : this(default(T), null) { }

			public override string ToString()
			{
				return val.ToString();
			}
		}

		#endregion Internal Class

		#region Fields and Properties

		private Node dummyHead;

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

		public LinkedList()
		{
			dummyHead = new Node();
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		public void Add(int index, T value)
		{
			if (index < 0 || index > Size)
				throw new ArgumentException("Add failed, illegal index.");

			Node prev = dummyHead;
			for (int i = 0; i < index; i++)
				prev = prev.next;

			prev.next = new Node(value, prev.next);
			Size++;
		}

		public void AddFirst(T value)
		{
			Add(0, value);
		}

		public void AddLast(T value)
		{
			Add(Size, value);
		}

		public T Get(int index)
		{
			if (index < 0 || index >= Size)
				throw new ArgumentException("Get failed, illegal index.");

			Node cur = dummyHead.next;
			for (int i = 0; i < index; i++)
				cur = cur.next;
			return cur.val;
		}

		public T GetFirst()
		{
			return Get(0);
		}

		public T GetLast()
		{
			return Get(Size - 1);
		}

		public void Set(int index, T value)
		{
			if (index < 0 || index >= Size)
				throw new ArgumentException("Set failed, illegal index.");

			Node cur = dummyHead.next;
			for (int i = 0; i < index; i++)
				cur = cur.next;
			cur.val = value;
		}

		public bool Contains(T value)
		{
			Node cur = dummyHead.next;
			while (cur != null)
			{
				if (cur.val.Equals(value))
					return true;
				cur = cur.next;
			}

			return false;
		}

		public T Remove(int index)
		{
			if (index < 0 || index >= Size)
				throw new ArgumentException("Remove failed, illegal index.");

			Node prev = dummyHead;

			for (int i = 0; i < index; i++)
				prev = prev.next;

			Node delNode = prev.next;
			prev.next = delNode.next;
			delNode.next = null;

			Size--;
			return delNode.val;
		}

		public T RemoveFirst()
		{
			return Remove(0);
		}

		public T RemoveLast()
		{
			return Remove(Size - 1);
		}

		public void RemoveElement(T value)
		{
			Node prev = dummyHead;

			while (prev.next != null)
			{
				if (prev.next.val.Equals(value))
					break;
				prev = prev.next;
			}

			if (prev.next != null)
			{
				Node delNode = prev.next;
				prev.next = delNode.next;
				delNode.next = null;
				Size--;
			}
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();

			for (Node cur = dummyHead.next; cur != null; cur = cur.next)
				res.Append($"{cur} -> ");
			res.Append("NULL");

			return res.ToString();
		}

		#endregion Methods
	}
}