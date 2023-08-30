//
//  JackOpenOptions.cs
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
	public enum JackOpenOptions
	{
		None = 0x00,
		/// <summary>
		/// Do not automatically start the JACK server when it is not
		/// already running.  This option is always selected if
		/// $JACK_NO_START_SERVER is defined in the calling process
		/// environment.
		/// </summary>
		NoStartServer = 0x01,
		/// <summary>
		/// Use the exact client name requested.  Otherwise, JACK
		/// automatically generates a unique one, if needed.
		/// </summary>
		UseExactName = 0x02,
		/// <summary>
		/// Open with optional <c>server_name</c> parameter.
		/// </summary>
		ServerName = 0x04,
		/// <summary>
		/// Pass a SessionID Token this allows the sessionmanager to identify the client again.
		/// </summary>
		SessionID = 0x20
	}
}
