using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surodoine.Internal.PortAudio
{
    public static class Constants
    {
        public enum PaStreamCallbackFlags : uint
        {
            paInputUnderflow = 1u,
            paInputOverflow,
            paOutputUnderflow = 4u,
            paOutputOverflow = 8u,
            paPrimingOutput = 16u
        }
        public enum PaStreamCallbackResult : uint
        {
            paContinue,
            paComplete,
            paAbort
        }
        public enum PaError
        {
	        paNoError,
	        paNotInitialized = -10000,
	        paUnanticipatedHostError,
	        paInvalidChannelCount,
	        paInvalidSampleRate,
	        paInvalidDevice,
	        paInvalidFlag,
	        paSampleFormatNotSupported,
	        paBadIODeviceCombination,
	        paInsufficientMemory,
	        paBufferTooBig,
	        paBufferTooSmall,
	        paNullCallback,
	        paBadStreamPtr,
	        paTimedOut,
	        paInternalError,
	        paDeviceUnavailable,
	        paIncompatibleHostApiSpecificStreamInfo,
	        paStreamIsStopped,
	        paStreamIsNotStopped,
	        paInputOverflowed,
	        paOutputUnderflowed,
	        paHostApiNotFound,
	        paInvalidHostApi,
	        paCanNotReadFromACallbackStream,
	        paCanNotWriteToACallbackStream,
	        paCanNotReadFromAnOutputOnlyStream,
	        paCanNotWriteToAnInputOnlyStream,
	        paIncompatibleStreamHostApi,
	        paBadBufferPtr
        }
    }
}
