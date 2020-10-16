using System;
using System.Runtime.InteropServices;
using CoreGraphics;
using Foundation;
using Metal;
using ObjCRuntime;

namespace MetalPerformanceShaders {
#if !MONOMAC
    public partial class MPSNNGraph {
		[Introduced (PlatformName.MacCatalyst, 13, 0)]
		[TV (13,0), NoMac, iOS (13,0)]
		public unsafe static MPSNNGraph Create (IMTLDevice device, MPSNNImageNode[] resultImages, bool[] resultsAreNeeded)
		{
			fixed (void *resultsAreNeededHandle = resultsAreNeeded)
				return Create (device, resultImages, (IntPtr) resultsAreNeededHandle);
		}
	}
#endif
}
