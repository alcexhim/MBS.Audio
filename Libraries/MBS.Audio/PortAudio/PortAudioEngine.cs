//
//  PortAudioEngine.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;

namespace MBS.Audio.PortAudio
{
	public class PortAudioEngine : AudioEngine
	{
		private PortAudioDevice mvarDefaultInput = null;
		public override AudioDevice DefaultInputDevice
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
				mvarDefaultOutput = new PortAudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}
		private PortAudioDevice mvarDefaultOutput = null;
		public override AudioDevice DefaultOutputDevice
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
				mvarDefaultOutput = new PortAudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}

		public override Guid ID => new Guid("{361b1dd6-b3d7-4358-bdee-b8741f48c7fe}");
		public override string Title => "PortAudio";

		private static PortAudioDevice[] mvarDevices = null;
		public static PortAudioDevice[] GetDevices()
		{
			if (mvarDevices == null || mvarDevices.Length == 0)
			{
				List<PortAudioDevice> devices = new List<PortAudioDevice>();
				int count = Internal.PortAudio.Methods.Pa_GetDeviceCount();
				for (int i = 0; i < count; i++)
				{
					PortAudioDevice device = new PortAudioDevice(i);
					devices.Add(device);
				}
				mvarDevices = devices.ToArray();
			}
			return mvarDevices;
		}

		public PortAudioDevice OpenAudioDevice(int handle)
		{
			return new PortAudioDevice(handle);
		}

		protected override void InitializeInternal()
		{
			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Initialize();
			Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultInputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
			mvarDefaultInput = new PortAudioDevice(defaultInputDeviceHandle);
			mvarDefaultOutput = new PortAudioDevice(defaultOutputDeviceHandle);
		}

		protected override AudioStream CreateAudioStreamInternal(AudioDevice inputDevice, short inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, short outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
		{
			return new PortAudioStream(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, flags);
		}

		protected override void TerminateInternal()
		{
			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Terminate();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
		}
	}
}
