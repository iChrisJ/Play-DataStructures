using System;
using System.Text;

namespace PlayDataStructures
{
	public class LinkedListQueue<T> : IQueue<T>
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

		#endregion	Internal Class

		#region Fields and Properties

		private Node head, tail;

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

		public LinkedListQueue()
		{
			head = tail = null;
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		public T Dequeue()
		{
			if (IsEmpty)
				throw new ArgumentException("Cannot dequeue from an empty queue.");

			Node res = head;
			head = head.next;
			res.next = null;
			if (head == null)
				tail = null;
			Size--;
			return res.val;
		}

		public void Enqueue(T val)
		{
			if (tail == null)
			{
				tail = new Node(val);
				head = tail;
			}
			else
			{
				tail.next = new Node(val);
				tail = tail.next;
			}
			Size++;
		}

		public T Front()
		{
			if (IsEmpty)
				throw new ArgumentException("The queue is empty.");

			return head.val;
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append("Queue: Front ");

			for (Node cur = head; cur != null; cur = cur.next)
				res.Append($"{cur} -> ");

			res.Append("NULL Tail");
			return res.ToString();
		}

		#endregion Methods
	}
}
