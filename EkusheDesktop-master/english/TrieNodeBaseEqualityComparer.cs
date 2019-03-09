using System.Collections.Generic;

namespace english
{
	/// <summary>
	/// EqualityComparer for TrieNodeBase implementations.
	/// </summary>
	internal class TrieNodeBaseEqualityComparer : IEqualityComparer<TrieNodeBase>
	{
		public bool Equals(TrieNodeBase x, TrieNodeBase y)
		{
			return x.Character == y.Character;
		}

		public int GetHashCode(TrieNodeBase trieNode)
		{
			return trieNode.Character.GetHashCode();
		}
	}
}
