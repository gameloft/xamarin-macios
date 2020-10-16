using System;

namespace Metal {

	public partial class MTLRenderPassSampleBufferAttachmentDescriptorArray {
#if !MONOMAC
        public MTLRenderPassSampleBufferAttachmentDescriptor this[nuint i] {
 			get => GetObject (i);
			set => SetObject (value, i);
		}
#endif
    }

}
