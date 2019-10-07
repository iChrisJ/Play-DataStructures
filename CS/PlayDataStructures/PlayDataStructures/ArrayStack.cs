using System.Text;

namespace PlayDataStructures
{
	public class ArrayStack<T> : IStack<T>
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

		public ArrayStack()
		{
			array = new Array<T>();
		}

		public ArrayStack(int capacity)
		{
			array = new Array<T>(capacity);
		}

		#endregion Constructors

		#region Methods

		public T Peek()
		{
			return array[array.Size - 1];
		}

		public T Pop()
		{
			return array.RemoveLast();
		}

		public void Push(T e)
		{
			array.AddLast(e);
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append("Stack: [");
			for (int i = 0; i < array.Size; i++)
			{
				res.Append(array[i]);
				if (i != array.Size - 1)
					res.Append(", ");
			}
			res.Append("] Top");
			return res.ToString();
		}

		#endregion Methods
	}
}
