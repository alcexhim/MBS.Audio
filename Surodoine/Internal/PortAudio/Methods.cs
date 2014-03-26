using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Surodoine.Internal.PortAudio
{
    public static class Methods
    {
        #region Initialization/Termination
        public static Constants.PaError Pa_Initialize()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_Initialize();
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_Initialize();
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_Terminate()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_Terminate();
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_Terminate();
                }
            }
            throw new PlatformNotSupportedException();
        }
        #endregion
        #region Device Enumeration
        public static int Pa_GetDeviceCount()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_GetDeviceCount();
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_GetDeviceCount();
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static int Pa_GetDefaultInputDevice()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_GetDefaultInputDevice();
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_GetDefaultInputDevice();
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static int Pa_GetDefaultOutputDevice()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_GetDefaultOutputDevice();
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_GetDefaultOutputDevice();
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Structures.PaDeviceInfo Pa_GetDeviceInfo(int index)
        {
            IntPtr ptr = IntPtr.Zero;
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    ptr = Internal.PortAudio.Linux.Methods.Pa_GetDeviceInfo(index);
                    break;
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    ptr = Internal.PortAudio.Windows.Methods.Pa_GetDeviceInfo(index);
                    break;
                }
            }
            
            Structures.PaDeviceInfo devinfo = new Structures.PaDeviceInfo();
            if (ptr != IntPtr.Zero)
            {
                devinfo = (Structures.PaDeviceInfo)Marshal.PtrToStructure(ptr, typeof(Structures.PaDeviceInfo));
            }
            return devinfo;
        }
        #endregion
        #region Stream Initialization
        public static Constants.PaError Pa_OpenStream(out IntPtr stream, ref Structures.PaStreamParameters inputParameters, ref Structures.PaStreamParameters outputParameters, double sampleRate, uint framesPerBuffer, AudioStreamFlags streamFlags, Delegates.PaStreamCallbackDelegate streamCallback, IntPtr userData)
        {
            stream = IntPtr.Zero;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_OpenStream(out stream, ref inputParameters, ref outputParameters, sampleRate, framesPerBuffer, streamFlags, streamCallback, userData);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_OpenStream(out stream, ref inputParameters, ref outputParameters, sampleRate, framesPerBuffer, streamFlags, streamCallback, userData);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_OpenDefaultStream(out IntPtr stream, int numInputChannels, int numOutputChannels, uint sampleFormat, double sampleRate, uint framesPerBuffer, Delegates.PaStreamCallbackDelegate streamCallback, IntPtr userData)
        {
            stream = IntPtr.Zero;

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_OpenDefaultStream(out stream, numInputChannels, numOutputChannels, sampleFormat, sampleRate, framesPerBuffer, streamCallback, userData);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_OpenDefaultStream(out stream, numInputChannels, numOutputChannels, sampleFormat, sampleRate, framesPerBuffer, streamCallback, userData);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_CloseStream(IntPtr stream)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_CloseStream(stream);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_CloseStream(stream);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_SetStreamFinishedCallback(ref IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)] Delegates.PaStreamFinishedCallbackDelegate streamFinishedCallback)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_SetStreamFinishedCallback(ref stream, streamFinishedCallback);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_SetStreamFinishedCallback(ref stream, streamFinishedCallback);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_StartStream(IntPtr stream)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_StartStream(stream);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_StartStream(stream);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_StopStream(IntPtr stream)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_StopStream(stream);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_StopStream(stream);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_AbortStream(IntPtr stream)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                    {
                        return Internal.PortAudio.Linux.Methods.Pa_AbortStream(stream);
                    }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                    {
                        return Internal.PortAudio.Windows.Methods.Pa_AbortStream(stream);
                    }
            }
            throw new PlatformNotSupportedException();
        }
        #endregion
        #region Stream Reading
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] float[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] byte[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] sbyte[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] ushort[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] short[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] uint[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_ReadStream(IntPtr stream, [Out] int[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_ReadStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_ReadStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        #endregion
        #region Stream Writing
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] float[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] byte[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] sbyte[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] ushort[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] short[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] uint[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        public static Constants.PaError Pa_WriteStream(IntPtr stream, [In] int[] buffer, uint frames)
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
                    return Internal.PortAudio.Linux.Methods.Pa_WriteStream(stream, buffer, frames);
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                case PlatformID.Xbox:
                {
                    return Internal.PortAudio.Windows.Methods.Pa_WriteStream(stream, buffer, frames);
                }
            }
            throw new PlatformNotSupportedException();
        }
        #endregion

        public static void Pa_ResultToException(Constants.PaError result)
        {
            switch (result)
            {
                case Internal.PortAudio.Constants.PaError.paBadBufferPtr:
                    throw new ArgumentException("Bad buffer pointer.");
                case Internal.PortAudio.Constants.PaError.paBadIODeviceCombination:
                    throw new ArgumentException("Bad input/output device combination.");
                case Internal.PortAudio.Constants.PaError.paBadStreamPtr:
                    throw new ArgumentException("Bad stream pointer.");
                case Internal.PortAudio.Constants.PaError.paBufferTooBig:
                    throw new ArgumentException("Buffer too big.");
                case Internal.PortAudio.Constants.PaError.paBufferTooSmall:
                    throw new ArgumentException("Buffer too small.");
                case Internal.PortAudio.Constants.PaError.paCanNotReadFromACallbackStream:
                    throw new System.IO.IOException("Cannot read from a callback stream.");
                case Internal.PortAudio.Constants.PaError.paCanNotReadFromAnOutputOnlyStream:
                    throw new System.IO.IOException("Cannot read from an output-only stream.");
                case Internal.PortAudio.Constants.PaError.paCanNotWriteToACallbackStream:
                    throw new System.IO.IOException("Cannot write to a callback stream.");
                case Internal.PortAudio.Constants.PaError.paCanNotWriteToAnInputOnlyStream:
                    throw new System.IO.IOException("Cannot write to an input-only stream.");
                case Internal.PortAudio.Constants.PaError.paDeviceUnavailable:
                    throw new System.IO.IOException("The device is unavailable.");
                case Internal.PortAudio.Constants.PaError.paHostApiNotFound:
                    throw new InvalidOperationException("Host API not found.");
                case Internal.PortAudio.Constants.PaError.paIncompatibleHostApiSpecificStreamInfo:
                    throw new InvalidOperationException("Incompatible host API-specific stream information.");
                case Internal.PortAudio.Constants.PaError.paIncompatibleStreamHostApi:
                    throw new InvalidOperationException("Incompatible stream host API.");
                case Internal.PortAudio.Constants.PaError.paInputOverflowed:
                    throw new OverflowException("Input overflowed.");
                case Internal.PortAudio.Constants.PaError.paInsufficientMemory:
                    throw new OutOfMemoryException("Insufficient memory.");
                case Internal.PortAudio.Constants.PaError.paInternalError:
                    throw new Exception("Internal error.");
                case Internal.PortAudio.Constants.PaError.paInvalidChannelCount:
                    throw new ArgumentException("Invalid channel count.");
                case Internal.PortAudio.Constants.PaError.paInvalidDevice:
                    throw new ArgumentException("Invalid device.");
                case Internal.PortAudio.Constants.PaError.paInvalidFlag:
                    throw new ArgumentException("Invalid flag.");
                case Internal.PortAudio.Constants.PaError.paInvalidHostApi:
                    throw new ArgumentException("Invalid host API.");
                case Internal.PortAudio.Constants.PaError.paInvalidSampleRate:
                    throw new ArgumentException("Invalid sample rate.");
                case Internal.PortAudio.Constants.PaError.paNotInitialized:
                    throw new InvalidOperationException("PortAudio has not been initialized.");
                case Internal.PortAudio.Constants.PaError.paNullCallback:
                    throw new InvalidOperationException("Null callback.");
                case Internal.PortAudio.Constants.PaError.paOutputUnderflowed:
                    // throw new OverflowException("Output underflowed.");
                    break;
                case Internal.PortAudio.Constants.PaError.paSampleFormatNotSupported:
                    throw new NotSupportedException("Sample format not supported.");
                case Internal.PortAudio.Constants.PaError.paStreamIsNotStopped:
                    throw new InvalidOperationException("Stream is not stopped.");
                case Internal.PortAudio.Constants.PaError.paStreamIsStopped:
                    throw new InvalidOperationException("Stream is stopped.");
                case Internal.PortAudio.Constants.PaError.paTimedOut:
                    throw new TimeoutException();
                case Internal.PortAudio.Constants.PaError.paUnanticipatedHostError:
                    throw new Exception("Unanticipated host error.");
            }
        }
    }
}
