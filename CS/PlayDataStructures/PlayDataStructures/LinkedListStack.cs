using System.Text;

namespace PlayDataStructures
{
	public class LinkedListStack<T> : IStack<T>
	{
		#region Fields and Properties

		private LinkedList<T> list;

		public int Size
		{
			get
			{
				return list.Size;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return list.IsEmpty;
			}
		}

		#endregion Fields and Properties

		#region Constructors

		public LinkedListStack()
		{
			list = new LinkedList<T>();
		}

		#endregion Constructors

		#region Methods

		public T Peek()
		{
			return list.GetFirst();
		}

		public T Pop()
		{
			return list.RemoveFirst();
		}

		public void Push(T e)
		{
			list.AddFirst(e);
		}

		public override string ToString()
		{
			StringBuilder res = new StringBuilder();
			res.Append("Stack: Top ");
			res.Append(list);

			return res.ToString();
		}

		#endregion Methods
	}
}
