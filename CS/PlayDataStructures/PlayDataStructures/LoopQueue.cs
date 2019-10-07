using System;
using System.Text;

namespace PlayDataStructures
{
	public class LoopQueue<T> : IQueue<T>
	{
		#region Fields and Properties

		private T[] data;

		private int front, tail;

		private int size;

		public int Size { get { return size; } }

		public bool IsEmpty
		{
			get
			{
				return front == tail;
			}
		}

		public int Capacity
		{
			get
			{
				return data.Length - 1;
			}
		}

		#endregion Fields and Properties

		#region Constructors

		public LoopQueue(int capacity)
		{
			data = new T[capacity + 1];
			front = tail = 0;
		}

		public LoopQueue() : this(10)
		{
		}

		#endregion Constructors

		#region Methods

		public T Dequeue()
		{
			if (IsEmpty)
				throw new ArgumentException("Cannot dequeue from an empty queue.");

			T ret = data[front];
			data[front] = default(T);
			front = (front + 1) % data.Length;
			size--;

			if (size == Capacity / 4 && Capacity / 2 != 0)
				Resize(Capacity / 2);

			return ret;
		}

		public void Enqueue(T e)
		{
			if ((tail + 1) % data.Length == front)
				Resize(Capacity * 2);

			data[tail] = e;
			tail = (tail + 1) % data.Length;
			size++;
		}

		public T Front()
		{
			if (IsEmpty)
				throw new ArgumentException("Queue is empty.");
			return data[front];
		}

		private void Resize(int newCapacity)
		{
			T[] newData = new T[newCapacity + 1];
			for (int i = 0; i < size; i++)
				newData[i] = data[(i + front) % data.Length];

			data = newData;
			front = 0;
			tail = size;
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append($"Queue: Size = {Size}, Capacity = {Capacity}\n");
			res.Append("Front [");
			for (int i = front; i != tail; i = (i + 1) % data.Length)
			{
				res.Append(data[i]);
				if ((i + 1) % data.Length != tail)
					res.Append(", ");
			}

			res.Append("] Tail");
			return res.ToString();
		}

		#endregion Methods
	}
}
