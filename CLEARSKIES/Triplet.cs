using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BRM
{
	[ComVisible(true)]
	[Serializable]
	public class Triplet<T1, T2, T3>
	{
		public T1 First;

		public T2 Second;

		public T3 Third;

		public static implicit operator KeyValuePair<T1, KeyValuePair<T2, T3>>(Triplet<T1, T2, T3> ykno)
		{
			return new KeyValuePair<T1, KeyValuePair<T2, T3>>(ykno.First, new KeyValuePair<T2, T3>(ykno.Second, ykno.Third));
		}

		public static Triplet<T1, T2, T3>[] operator +(Triplet<T1, T2, T3> triplet, Triplet<T1, T2, T3> triplet2)
		{
			return new Triplet<T1, T2, T3>[]
			{
				triplet,
				triplet2
			};
		}

		public static Triplet<T1, T2, T3>[] operator +(Triplet<T1, T2, T3> triplet, Triplet<T1, T2, T3>[] triplets)
		{
			Array.Resize<Triplet<T1, T2, T3>>(ref triplets, triplets.Length + 1);
			triplets[triplets.Length - 1] = triplet;
			return triplets;
		}

		public static Triplet<T1, T2, T3>[] operator +(Triplet<T1, T2, T3>[] triplets, Triplet<T1, T2, T3> triplet)
		{
			Array.Resize<Triplet<T1, T2, T3>>(ref triplets, triplets.Length + 1);
			triplets[triplets.Length - 1] = triplet;
			return triplets;
		}

		public static Triplet<T1, T2, T3>[] operator -(Triplet<T1, T2, T3>[] triplets, Triplet<T1, T2, T3> triplet)
		{
			List<Triplet<T1, T2, T3>> list = triplets.ToList<Triplet<T1, T2, T3>>();
			if (list.Remove(triplet))
			{
				return list.ToArray();
			}
			return triplets;
		}

		public KeyValuePair<T2, T3> this[int optional]
		{
			get
			{
				return new KeyValuePair<T2, T3>(this.Second, this.Third);
			}
			set
			{
				this.Second = value.Key;
				this.Third = value.Value;
			}
		}

		public KeyValuePair<T1, T2> OneTwoPair
		{
			get
			{
				return new KeyValuePair<T1, T2>(this.First, this.Second);
			}
			set
			{
				this.First = value.Key;
				this.Second = value.Value;
			}
		}

		public KeyValuePair<T2, T3> TwoThreePair
		{
			get
			{
				return new KeyValuePair<T2, T3>(this.Second, this.Third);
			}
			set
			{
				this.Second = value.Key;
				this.Third = value.Value;
			}
		}

		public KeyValuePair<T1, T3> OneThreePair
		{
			get
			{
				return new KeyValuePair<T1, T3>(this.First, this.Third);
			}
			set
			{
				this.First = value.Key;
				this.Third = value.Value;
			}
		}

		public Triplet(Triplet<T1, T2, T3> triple)
		{
			this.First = triple.First;
			this.Second = triple.Second;
			this.Third = triple.Third;
		}

		public Triplet(T1 first, T2 second, T3 third)
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
		}

		public Triplet(T1 first, KeyValuePair<T2, T3> pair)
		{
			this.First = first;
			this.Second = pair.Key;
			this.Third = pair.Value;
		}

		public Triplet(KeyValuePair<T1, KeyValuePair<T2, T3>> pair)
		{
			this.First = pair.Key;
			this.Second = pair.Value.Key;
			this.Third = pair.Value.Value;
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}, {2}]", (this.First != null) ? this.First.ToString() : string.Empty, (this.Second != null) ? this.Second.ToString() : string.Empty, (this.Third != null) ? this.Third.ToString() : string.Empty);
		}

		public string ToString(Func<T1, string> clause1)
		{
			return string.Format("[{0}, {1}, {2}]", (this.First != null) ? clause1(this.First).ToString() : string.Empty, (this.Second != null) ? this.Second.ToString() : string.Empty, (this.Third != null) ? this.Third.ToString() : string.Empty);
		}

		public string ToString(Func<T1, string> clause1, Func<T2, string> clause2)
		{
			return string.Format("[{0}, {1}, {2}]", (this.First != null) ? clause1(this.First).ToString() : string.Empty, (this.Second != null) ? clause2(this.Second).ToString() : string.Empty, (this.Third != null) ? this.Third.ToString() : string.Empty);
		}

		public string ToString(Func<T1, string> clause1, Func<T2, string> clause2, Func<T3, string> clause3)
		{
			return string.Format("[{0}, {1}, {2}]", (this.First != null) ? clause1(this.First).ToString() : string.Empty, (this.Second != null) ? clause2(this.Second).ToString() : string.Empty, (this.Third != null) ? clause3(this.Third).ToString() : string.Empty);
		}

		public Triplet<T1, T2, T3> Clone()
		{
			return new Triplet<T1, T2, T3>(this);
		}

		public override bool Equals(object obj)
		{
			if (obj is Triplet<T1, T2, T3>)
			{
				Triplet<T1, T2, T3> triplet = (Triplet<T1, T2, T3>)obj;
				return triplet != null && (this.First.Equals(triplet.First) && this.Second.Equals(triplet.Second)) && this.Third.Equals(triplet.Third);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
