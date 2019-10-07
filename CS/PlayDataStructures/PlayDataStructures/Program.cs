using System;
using System.Diagnostics;

namespace PlayDataStructures
{
	class Program
	{
		public static void Main()
		{
			//Array<int> arr = new Array<int>();
			//for (int i = 0; i < 10; i++)
			//	arr.AddLast(i);
			//Console.WriteLine(arr);

			//arr.Add(1, 100);
			//Console.WriteLine(arr);

			//arr.AddFirst(-1);
			//Console.WriteLine(arr);

			//arr.Remove(2);
			//Console.WriteLine(arr);

			//arr.RemoveElement(4);
			//Console.WriteLine(arr);

			//arr.RemoveFirst();
			//Console.WriteLine(arr);

			/********************************************/

			//ArrayStack<int> stack = new ArrayStack<int>();

			//for (int i = 0; i < 5; i++)
			//{
			//	stack.Push(i);
			//	Console.WriteLine(stack);
			//}

			//stack.Pop();
			//Console.WriteLine(stack);

			/********************************************/

			//ArrayQueue<int> queue = new ArrayQueue<int>();
			//LoopQueue<int> queue = new LoopQueue<int>();
			//for (int i = 0; i < 10; i++)
			//{
			//	queue.Enqueue(i);
			//	Console.WriteLine(queue);

			//	if (i % 3 == 2)
			//	{
			//		queue.Dequeue();
			//		Console.WriteLine(queue);
			//	}
			//}

			/********************************************/

			int opCount = 100000;

			ArrayQueue<int> arrayQueue = new ArrayQueue<int>();
			double time1 = TestQueue(arrayQueue, opCount);
			Console.WriteLine("ArrayQueue, time: " + time1 + " s");

			LoopQueue<int> loopQueue = new LoopQueue<int>();
			double time2 = TestQueue(loopQueue, opCount);
			Console.WriteLine("LoopQueue, time: " + time2 + " s");
		}

		private static double TestQueue(IQueue<int> q, int opCount)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Random random = new Random();
			for (int i = 0; i < opCount; i++)
				q.Enqueue(random.Next(int.MaxValue));

			for (int i = 0; i < opCount; i++)
				q.Dequeue();
			sw.Stop();

			return sw.Elapsed.TotalSeconds;
		}
	}
}
