//
//  JackInputPort.cs
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
	public class JackInputPort : JackPort
	{
		public JackInputPort(JackClient client, IntPtr handle, string portName, string portType, long bufferSize, bool isMonitor, bool isPhysical, bool isTerminal) :  base(client, handle, portName, portType, bufferSize, true, false, isMonitor, isPhysical, isTerminal)
		{
		}

		public float[] Read(long frameCount)
		{
			uint fc = (uint)frameCount;
			IntPtr hBuffer = Internal.Methods.jack_port_get_buffer(Handle, fc);

			float[] buffer = new float[fc];
			System.Runtime.InteropServices.Marshal.Copy(hBuffer, buffer, 0, (int)fc);
			return buffer;
		}
	}
}
