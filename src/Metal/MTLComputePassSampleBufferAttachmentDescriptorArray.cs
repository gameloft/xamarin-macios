using System;

namespace Metal {

	public partial class MTLComputePassSampleBufferAttachmentDescriptorArray {
#if !MONOMAC
        public MTLComputePassSampleBufferAttachmentDescriptor this[nuint i] {
 			get => GetObject (i);
			set => SetObject (value, i);
		}
#endif
    }

}
