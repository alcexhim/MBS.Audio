using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
	public struct AudioTimestamp : IComparable<AudioTimestamp>
	{
		public static AudioTimestamp FromSamples(long totalSamples, int samplesPerSecond)
		{
			AudioTimestamp timestamp = new AudioTimestamp();
			timestamp.TotalSamples = totalSamples;
			timestamp.mvarSamplesPerSecond = samplesPerSecond;
			timestamp._IsNotEmpty = true;
			return timestamp;
		}
		public static AudioTimestamp FromSamples(long totalSamples, int samplesPerSecond, int bars, int beats, int ticks, float beatsPerBar, double ticksPerBeat)
		{
			AudioTimestamp timestamp = new AudioTimestamp();
			timestamp.TotalSamples = totalSamples;
			timestamp.mvarSamplesPerSecond = samplesPerSecond;
			timestamp._IsNotEmpty = true;
			timestamp.Bars = bars;
			timestamp.Beats = beats;
			timestamp.Ticks = ticks;
			timestamp.BeatsPerBar = beatsPerBar;
			timestamp.TicksPerBeat = ticksPerBeat;
			return timestamp;
		}

		public static AudioTimestamp FromHMS(int hours, int minutes, int seconds, int milliseconds, int samplesPerSecond)
		{
			return FromHMS(0, hours, minutes, seconds, milliseconds, samplesPerSecond);
		}
		public static AudioTimestamp FromHMS(int days, int hours, int minutes, int seconds, int milliseconds, int samplesPerSecond)
		{
			AudioTimestamp timestamp = new AudioTimestamp();
			timestamp.TotalSamples = (int)(((double)milliseconds / 100) + (seconds) + (minutes * 60) + (hours * 3600) + (days * 24 * 3600)) * samplesPerSecond;
			timestamp.mvarSamplesPerSecond = samplesPerSecond;
			timestamp._IsNotEmpty = true;
			return timestamp;
		}

		private bool _IsNotEmpty;
		public bool IsEmpty { get { return !_IsNotEmpty; } }

		public int Bars { get; private set; }
		public int Beats { get; private set; }
		public int Ticks { get; private set; }
		public float BeatsPerBar { get; private set; }
		public double TicksPerBeat { get; private set; }

		public string ToBBTString(string separator = ".")
		{
			return String.Format("{0}{3}{1}{3}{2}", Bars.ToString().PadLeft(3, '0'), Beats.ToString().PadLeft(2, '0'), Ticks.ToString().PadLeft(4, '0'), separator);
		}

		public BarBeatTick ToBBTTimeSpan()
		{
			BarBeatTick bbt = BarBeatTick.FromBBT(Bars, Beats, Ticks, BeatsPerBar, TicksPerBeat);
			return bbt;
		}

		public static readonly AudioTimestamp Empty = new AudioTimestamp();

		private int mvarSamplesPerSecond;

		private long mvarTotalSamples;
		public long TotalSamples { get { return mvarTotalSamples; } set { mvarTotalSamples = value; } }

		public int Days
		{
			get { return (int)(TotalDays % 365); }
		}
		public int Hours
		{
			get { return (int)(TotalHours % 24); }
		}
		public int Minutes
		{
			get { return (int)(TotalMinutes % 60); }
		}
		public int Seconds
		{
			get { return (int)(TotalSeconds % 60); }
		}
		public int Milliseconds
		{
			get { return (int)(TotalMilliseconds % 1000); }
		}
		public long Samples
		{
			get
			{
				if (mvarSamplesPerSecond == 0) return 0;
				return mvarTotalSamples % mvarSamplesPerSecond;
			}
		}

		public double TotalDays
		{
			get
			{
				return ((double)TotalHours / 24);
			}
		}
		public double TotalHours
		{
			get
			{
				return ((double)TotalMinutes / 60);
			}
		}
		public double TotalMinutes
		{
			get
			{
				return ((double)TotalSeconds / 60);
			}
		}
		public double TotalSeconds
		{
			get
			{
				if (mvarSamplesPerSecond == 0) return 0;
				return ((double)mvarTotalSamples / mvarSamplesPerSecond);
			}
		}
		public double TotalMilliseconds
		{
			get
			{
				if (mvarSamplesPerSecond == 0) return 0;
				return (((double)mvarTotalSamples / mvarSamplesPerSecond * 1000));
			}
		}

		public override string ToString()
		{
			// return Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0') + ":" + Seconds.ToString().PadLeft(2, '0') + "." + Milliseconds.ToString() + "/" + Samples.ToString();
			return Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0') + ":" + Seconds.ToString().PadLeft(2, '0') + "." + Milliseconds.ToString().PadLeft(3, '0');
		}

		public TimeSpan ToTimeSpan()
		{
			double tsecs = 0;
			double tms = 0;
			if (mvarTotalSamples != 0)
			{
				tsecs = ((double)mvarTotalSamples / mvarSamplesPerSecond);
				tms = (((double)mvarTotalSamples / mvarSamplesPerSecond) * 1000) % 1000;
			}

			int days = (int)(((((double)tsecs / 60) / 60) / 24) % 365);
			int hours = (int)((((double)tsecs / 60) / 60) % 24);
			int mins = (int)(((double)tsecs / 60) % 24);
			int secs = (int)((double)tsecs % 60);
			int ms = (int)(tms);

			TimeSpan ts = new TimeSpan(days, hours, mins, secs, ms);
			return ts;
		}

		public int CompareTo(AudioTimestamp other)
		{
			return this.ToTimeSpan().CompareTo(other.ToTimeSpan());
		}

		public static bool operator <(AudioTimestamp left, AudioTimestamp right)
		{
			return left.CompareTo(right) < 0;
		}
		public static bool operator >(AudioTimestamp left, AudioTimestamp right)
		{
			return left.CompareTo(right) > 0;
		}
		public static bool operator <=(AudioTimestamp left, AudioTimestamp right)
		{
			return left.CompareTo(right) <= 0;
		}
		public static bool operator >=(AudioTimestamp left, AudioTimestamp right)
		{
			return left.CompareTo(right) >= 0;
		}
	}
}
