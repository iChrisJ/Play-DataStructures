using System.Collections.Generic;

namespace PlayDataStructures
{
	public class Trie
	{
		#region Internal Class

		private class Node
		{
			public bool IsWord { get; set; }
			public IDictionary<char, Node> Next { get; private set; }

			public Node(bool isWord)
			{
				IsWord = isWord;
				Next = new Dictionary<char, Node>();
			}

			public Node() : this(false) { }
		}

		#endregion Internal Class

		#region Fields and Properties

		private Node root;

		public int Size { get; private set; }

		#endregion Fields and Properties

		#region Constructors

		public Trie()
		{
			root = new Node();
			Size = 0;
		}

		#endregion Constructors

		#region Methods

		public void Add(string word)
		{
			Node cur = root;

			for (int i = 0; i < word.Length; i++)
			{
				if (!cur.Next.ContainsKey(word[i]))
					cur.Next.Add(word[i], new Node());
				cur = cur.Next[word[i]];
			}

			if (!cur.IsWord)
			{
				cur.IsWord = true;
				Size++;
			}
		}

		public bool Contains(string word)
		{
			Node cur = root;
			for (int i = 0; i < word.Length; i++)
			{
				if (!cur.Next.ContainsKey(word[i]))
					return false;
				cur = cur.Next[word[i]];
			}

			return cur.IsWord;
		}

		// 查询是否在Trie中有单词以prefix为前缀
		public bool HasPrefix(string prefix)
		{
			Node cur = root;
			for (int i = 0; i < prefix.Length; i++)
			{
				if (!cur.Next.ContainsKey(prefix[i]))
					return false;
				cur = cur.Next[prefix[i]];
			}
			return true;
		}

		#endregion Methods
	}
}
