﻿//
//  JackAudioEngine.cs
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
namespace MBS.Audio.Jack
{
	public class JackAudioEngine : AudioEngine
	{
		public override Guid ID => new Guid("{658df958-7d57-482d-ac14-caca38e8d249}");
		public override string Title => "JACK";

		public override AudioDevice DefaultInputDevice => throw new NotImplementedException();

		public override AudioDevice DefaultOutputDevice => throw new NotImplementedException();

		protected override AudioStream CreateAudioStreamInternal(AudioDevice inputDevice, short inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, short outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
		{
			throw new NotImplementedException();
		}

		protected override void InitializeInternal()
		{
		}

		protected override void TerminateInternal()
		{
		}
	}
}
