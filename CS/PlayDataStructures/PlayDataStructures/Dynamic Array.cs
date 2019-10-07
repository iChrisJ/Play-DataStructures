using System;
using System.Text;

namespace PlayDataStructures
{
	public class Array<T>
	{
		#region Fields and Properties

		private T[] data;

		public int Size { get; private set; }

		public int Capacity
		{
			get
			{
				return data.Length;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return Size == 0;
			}

		}

		#endregion Fields and Properties

		#region Constructors

		public Array(int capacity)
		{
			data = new T[capacity];
			Size = 0;
		}

		public Array() : this(10)
		{
		}

		#endregion Constructors

		#region Methods

		public void Add(int index, T e)
		{
			if (index < 0 || index > Size)
				throw new ArgumentException("Add failed. Require index >= 0 and index <= Size.");

			if (Size == data.Length)
				Resize(2 * Size); // 扩容

			for (int i = Size - 1; i >= index; i--)
				data[i + 1] = data[i];

			data[index] = e;
			Size++;
		}
		public void AddFirst(T e)
		{
			Add(0, e);
		}

		public void AddLast(T e)
		{
			Add(Size, e);
		}

		public T this[int index]
		{
			get
			{
				if (index < 0 || index >= Size)
					throw new ArgumentException("Get failed. Index is illegal.");
				return data[index];
			}
			set
			{
				if (index < 0 || index >= Size)
					throw new ArgumentException("Set failed. Index is illegal.");
				data[index] = value;
			}
		}

		public bool Contains(T e)
		{
			for (int i = 0; i < Size; i++)
				if (data[i].Equals(e))
					return true;

			return false;
		}

		public int Find(T e)
		{
			for (int i = 0; i < Size; i++)
				if (data[i].Equals(e))
					return i;

			return -1;
		}

		public T Remove(int index)
		{
			if (index < 0 || index >= Size)
				throw new ArgumentException("Remove failed. Index is illegal.");

			T ret = data[index];

			for (int i = index + 1; i < Size; i++)
				data[i - 1] = data[i];
			Size--;
			data[Size] = default(T);

			if (Size == data.Length / 4)
				Resize(data.Length / 2); // 缩容, 防止复杂度震荡

			return ret;
		}

		public T RemoveFirst()
		{
			return Remove(0);
		}

		public T RemoveLast()
		{
			return Remove(Size - 1);
		}

		public void RemoveElement(T e)
		{
			int index = Find(e);
			if (index != -1)
				Remove(index);
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append($"Array: Size = {Size}, Capacity = {data.Length} \n");
			res.Append("[");
			for (int i = 0; i < Size; i++)
			{
				res.Append(data[i]);
				if (i != Size - 1)
					res.Append(", ");
			}
			res.Append("]");
			return res.ToString();
		}

		private void Resize(int newCapacity)
		{
			T[] newData = new T[newCapacity];
			for (int i = 0; i < Size; i++)
				newData[i] = data[i];

			data = newData;
		}

		#endregion Methods
	}
}
