using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
    public struct AudioTimestamp
    {
        public AudioTimestamp(long totalSamples, int samplesPerSecond)
        {
            mvarTotalSamples = totalSamples;
            mvarSamplesPerSecond = samplesPerSecond;
        }
        public AudioTimestamp(int hours, int minutes, int seconds, int milliseconds, int samplesPerSecond)
        {
            mvarTotalSamples = (int)(((double)milliseconds / 100) + (seconds) + (minutes * 60) + (hours * 3600)) * samplesPerSecond;
            mvarSamplesPerSecond = samplesPerSecond;
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
    }
}
