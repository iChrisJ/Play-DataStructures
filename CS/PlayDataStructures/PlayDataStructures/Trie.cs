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
			// Add(root, word, 0);
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

		private void Add(Node node, string word, int index)
		{
			if (index == word.Length)
			{
				if (!node.IsWord)
				{
					node.IsWord = true;
					Size++;
				}
				return;
			}

			if (!node.Next.ContainsKey(word[index]))
				node.Next.Add(word[index], new Node());

			Add(node.Next[word[index]], word, index + 1);
		}

		public bool Contains(string word)
		{
			// return Contains(root, word, 0);
			Node cur = root;
			for (int i = 0; i < word.Length; i++)
			{
				if (!cur.Next.ContainsKey(word[i]))
					return false;
				cur = cur.Next[word[i]];
			}

			return cur.IsWord;
		}

		private bool Contains(Node node, string word, int index)
		{
			if (index == word.Length)
				return node.IsWord;

			if (!node.Next.ContainsKey(word[index]))
				return false;

			return Contains(node.Next[word[index]], word, index + 1);
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

		private bool HasPrefix(Node node, string prefix, int index)
		{
			if (index == prefix.Length)
				return true;

			if (!node.Next.ContainsKey(prefix[index]))
				return false;

			return HasPrefix(node.Next[prefix[index]], prefix, index + 1);
		}

		#endregion Methods
	}
}
