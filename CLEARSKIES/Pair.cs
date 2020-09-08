using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CLEARSKIES
{
    [System.Runtime.InteropServices.ComVisible(true)]
	[System.Serializable]
	public class Pair<TKey, TValue>
	{
		public Pair(System.Collections.Generic.KeyValuePair<TKey, TValue> pair)
		{
			this.First = pair.Key;
			this.Second = pair.Value;
		}

		public Pair(System.Collections.DictionaryEntry entry)
		{
			this.First = (TKey)((object)entry.Key);
			this.Second = (TValue)((object)entry.Value);
		}

		public Pair(TKey first, TValue second)
		{
			this.First = first;
			this.Second = second;
		}

		public bool AnyEquals(object obj)
		{
			if (obj == null)
			{
				return this == null || this.First == null || this.Second == null;
			}
			return this == obj || (obj is TKey && this.First.Equals((TKey)((object)obj))) || (obj is TValue && this.Second.Equals((TValue)((object)obj))) || this == obj || this.First.Equals((TKey)((object)obj)) || this.Second.Equals((TValue)((object)obj));
		}

		public Pair<TKey, TValue> Clone()
		{
			return new Pair<TKey, TValue>(this.First, this.Second);
		}

		public override bool Equals(object obj)
		{
			if (obj is Pair<TKey, TValue>)
			{
				Pair<TKey, TValue> pair = (Pair<TKey, TValue>)obj;
				return pair != null && this.First.Equals(pair.First) && this.Second.Equals(pair.Second);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator System.Collections.Generic.KeyValuePair<TKey, TValue>(Pair<TKey, TValue> pair)
		{
			return new System.Collections.Generic.KeyValuePair<TKey, TValue>(pair.First, pair.Second);
		}

		public static implicit operator Pair<TKey, TValue>(System.Collections.Generic.KeyValuePair<TKey, TValue> pair)
		{
			return new Pair<TKey, TValue>(pair);
		}

		public static implicit operator System.Collections.DictionaryEntry(Pair<TKey, TValue> pair)
		{
			return new System.Collections.DictionaryEntry(pair.First, pair.Second);
		}

		public static implicit operator Pair<TKey, TValue>(System.Collections.DictionaryEntry entry)
		{
			return new Pair<TKey, TValue>(entry);
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", new object[]
			{
				(this.First != null) ? this.First.ToString() : string.Empty,
				(this.Second != null) ? this.Second.ToString() : string.Empty
			});
		}

		public string ToString(System.Func<TKey, string> clause1)
		{
			return string.Format("[{0}, {1}]", new object[]
			{
				(this.First != null) ? clause1(this.First).ToString() : string.Empty,
				(this.Second != null) ? this.Second.ToString() : string.Empty
			});
		}

		public string ToString(System.Func<TKey, string> clause1, System.Func<TValue, string> clause2)
		{
			return string.Format("[{0}, {1}]", new object[]
			{
				(this.First != null) ? clause1(this.First).ToString() : string.Empty,
				(this.Second != null) ? clause2(this.Second).ToString() : string.Empty
			});
		}

		public TKey First;

		public TValue Second;
	}
}

