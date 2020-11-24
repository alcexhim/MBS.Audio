using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Audio.Internal.PortAudio.Linux
{
    internal static class Methods
    {
        private const string LIBRARY_FILENAME = "libportaudio.so.2";

        #region Initialization/Termination
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_Initialize();
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_Terminate();
        #endregion
        #region Device Enumeration
        [DllImport(LIBRARY_FILENAME)]
        public static extern int Pa_GetDeviceCount();
        [DllImport(LIBRARY_FILENAME)]
        public static extern int Pa_GetDefaultInputDevice();
        [DllImport(LIBRARY_FILENAME)]
        public static extern int Pa_GetDefaultOutputDevice();
        [DllImport(LIBRARY_FILENAME)]
        public static extern IntPtr Pa_GetDeviceInfo(int device);
        #endregion
        #region Stream Initialization
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_OpenStream(out IntPtr stream, ref Structures.PaStreamParameters inputParameters, ref Structures.PaStreamParameters outputParameters, double sampleRate, uint framesPerBuffer, AudioStreamFlags streamFlags, Delegates.PaStreamCallbackDelegate streamCallback, IntPtr userData);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_OpenDefaultStream(out IntPtr stream, int numInputChannels, int numOutputChannels, uint sampleFormat, double sampleRate, uint framesPerBuffer, Delegates.PaStreamCallbackDelegate streamCallback, IntPtr userData);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_CloseStream(IntPtr stream);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_SetStreamFinishedCallback(ref IntPtr stream, [MarshalAs(UnmanagedType.FunctionPtr)] Delegates.PaStreamFinishedCallbackDelegate streamFinishedCallback);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_StartStream(IntPtr stream);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_StopStream(IntPtr stream);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_AbortStream(IntPtr stream);
        #endregion
        #region Stream Reading
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] float[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] byte[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] sbyte[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] ushort[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] short[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] uint[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_ReadStream(IntPtr stream, [Out] int[] buffer, uint frames);
        #endregion
        #region Stream Writing
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] float[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] byte[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] sbyte[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] ushort[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] short[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] uint[] buffer, uint frames);
        [DllImport(LIBRARY_FILENAME)]
        public static extern Constants.PaError Pa_WriteStream(IntPtr stream, [In] int[] buffer, uint frames);
        #endregion
    }
}
