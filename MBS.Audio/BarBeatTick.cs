using System;
namespace MBS.Audio
{
	public struct BarBeatTick : IComparable<BarBeatTick>
	{
		public int Bars { get; private set; }
		public int Beats { get; private set; }
		public int Ticks { get; private set; }

		public float? BeatsPerBar { get; private set; }
		public double? TicksPerBeat { get; private set; }

		public static BarBeatTick FromBBT(int bars, int beats, int ticks, float? beatsPerBar = null, double? ticksPerBeat = null)
		{
			BarBeatTick bbt = new BarBeatTick();
			bbt.Bars = bars;
			bbt.Beats = beats;
			bbt.Ticks = ticks;
			bbt.BeatsPerBar = beatsPerBar;
			bbt.TicksPerBeat = ticksPerBeat;
			bbt._isNotEmpty = true;
			return bbt;
		}

		private bool _isNotEmpty;
		public bool IsEmpty { get { return !_isNotEmpty; } }

		public static readonly BarBeatTick Empty = new BarBeatTick();

		public override bool Equals(object obj)
		{
			if (obj is BarBeatTick bbt)
			{
				return (Bars == bbt.Bars && Beats == bbt.Beats && Ticks == bbt.Ticks && BeatsPerBar == bbt.BeatsPerBar && TicksPerBeat == bbt.TicksPerBeat && IsEmpty == bbt.IsEmpty);
			}
			return false;
		}

		public int CompareTo(BarBeatTick other)
		{
			if (Bars == other.Bars)
			{
				if (Beats == other.Beats)
				{
					if (Ticks == other.Ticks)
					{
						// completely equal
						return 0;
					}
					else
					{
						return Ticks.CompareTo(other.Ticks);
					}
				}
				else
				{
					return Beats.CompareTo(other.Beats);
				}
			}
			else
			{
				return Bars.CompareTo(other.Bars);
			}
		}

		public static bool operator ==(BarBeatTick left, BarBeatTick right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(BarBeatTick left, BarBeatTick right)
		{
			return !left.Equals(right);
		}

		public static bool operator >=(BarBeatTick left, BarBeatTick right)
		{
			return (left.CompareTo(right) >= 0);
		}
		public static bool operator >(BarBeatTick left, BarBeatTick right)
		{
			return (left.CompareTo(right) > 0);
		}
		public static bool operator <=(BarBeatTick left, BarBeatTick right)
		{
			return (left.CompareTo(right) < 0);
		}
		public static bool operator <(BarBeatTick left, BarBeatTick right)
		{
			return (left.CompareTo(right) <= 0);
		}

		public static BarBeatTick operator +(BarBeatTick left, BarBeatTick right)
		{
			return left.Add(right);
		}

		public BarBeatTick Add(int ticks)
		{
			BarBeatTick thiss = new BarBeatTick();
			thiss.Bars = Bars;
			thiss.Beats = Beats;
			thiss.Ticks = Ticks + ticks;
			thiss.TicksPerBeat = TicksPerBeat;
			thiss.BeatsPerBar = BeatsPerBar;
			thiss._isNotEmpty = true;
			return thiss;
		}
		public BarBeatTick Add(BarBeatTick other)
		{
			int bars = Bars + other.Bars;
			int beats = Beats + other.Beats;
			int ticks = Ticks + other.Ticks;

			float? beatsPerBar = BeatsPerBar;
			if (beatsPerBar == null) beatsPerBar = other.BeatsPerBar;
			double? ticksPerBeat = TicksPerBeat;
			if (ticksPerBeat == null) ticksPerBeat = other.TicksPerBeat;
			if (ticksPerBeat == null) ticksPerBeat = 2000;

			while (ticks > ticksPerBeat)
			{
				beats++;
				ticks -= (int)ticksPerBeat.GetValueOrDefault();
			}
			while (beats > beatsPerBar)
			{
				bars++;
				beats -= (int)beatsPerBar.GetValueOrDefault();
			}

			return BarBeatTick.FromBBT(bars, beats, ticks, beatsPerBar, ticksPerBeat);
		}

		public override string ToString()
		{
			return ToString(" | ");
		}
		public string ToString(string separator)
		{
			return String.Format("{0}{1}{2}{1}{3}", Bars.ToString().PadLeft(3, '0'), separator, Beats.ToString().PadLeft(2, '0'), Ticks.ToString().PadLeft(4, '0'));
		}
	}
}
