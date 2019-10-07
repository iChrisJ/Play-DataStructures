using System.Text;

namespace PlayDataStructures
{
	public class ArrayQueue<T> : IQueue<T>
	{
		#region Fields and Properties

		private Array<T> array;

		public int Size
		{
			get
			{
				return array.Size;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return array.IsEmpty;
			}
		}

		public int Capacity
		{
			get
			{
				return array.Capacity;
			}
		}

		#endregion Fields and Properties

		#region Constructors

		public ArrayQueue()
		{
			array = new Array<T>();
		}

		public ArrayQueue(int capacity)
		{
			array = new Array<T>(capacity);
		}

		#endregion Constructors

		#region Methods

		public T Dequeue()
		{
			return array.RemoveFirst();
		}

		public void Enqueue(T e)
		{
			array.AddLast(e);
		}

		public T Front()
		{
			return array[0];
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append("Queue: Front [");
			for (int i = 0; i < array.Size; i++)
			{
				res.Append(array[i]);
				if (i != array.Size - 1)
					res.Append(", ");
			}
			res.Append("] Tail");
			return res.ToString();
		}
		#endregion Methods
	}
}
