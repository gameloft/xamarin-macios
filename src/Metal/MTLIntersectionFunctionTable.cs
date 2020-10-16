#if !TVOS
using System;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;

namespace Metal {

	// add some extension methods to make the API of the protocol nicer
	public static class MTLIntersectionFunctionTableExtensions {
#if !MONOMAC
        [NoMac, iOS (14,0), NoTV]
		public static void SetBuffers (this IMTLIntersectionFunctionTable table, IMTLBuffer[] buffers, nuint[] offsets, NSRange range)
		{
			if (buffers == null)
				throw new ArgumentNullException (nameof (buffers));
			if (offsets == null)
				throw new ArgumentNullException (nameof (offsets));

			var bufferPtrArray = buffers.Length <= 1024 ? stackalloc IntPtr[buffers.Length] : new IntPtr [buffers.Length];
			// get all intptr from the array to pass to the lower level call
			for (var i = 0; i < buffers.Length; i++) {
				bufferPtrArray [i] = buffers [i].Handle;
			}

			unsafe {
				fixed (void* buffersPtr = bufferPtrArray)
				fixed (void* offsetsPtr = offsets) { // can use fixed
					table.SetBuffers ((IntPtr) buffersPtr, (IntPtr) offsetsPtr, range);
				}
			}
			GC.KeepAlive (buffers);
		}
#endif
    }
}
#endif
