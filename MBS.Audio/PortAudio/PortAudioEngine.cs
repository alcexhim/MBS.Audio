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
namespace MBS.Audio.PortAudio
{
	public class PortAudioEngine : AudioEngine
	{
		private AudioDevice mvarDefaultInput = null;
		public AudioDevice DefaultInput
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
				mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}
		private AudioDevice mvarDefaultOutput = null;
		public AudioDevice DefaultOutput
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
				mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}

		public override Guid ID => new Guid("{361b1dd6-b3d7-4358-bdee-b8741f48c7fe}");
		public override string Title => "PortAudio";

		protected override void InitializeInternal()
		{
			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Initialize();
			Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultInputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
			mvarDefaultInput = new AudioDevice(defaultInputDeviceHandle);
			mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
		}

		protected override void TerminateInternal()
		{
			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Terminate();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
		}
	}
}
