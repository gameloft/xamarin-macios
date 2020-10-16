using System;

namespace Metal {

#if !MONOMAC
    public partial class MTLBlitPassSampleBufferAttachmentDescriptorArray {

		public MTLBlitPassSampleBufferAttachmentDescriptor this[nuint i] {
 			get => GetObject (i);
			set => SetObject (value, i);
		}
	}
#endif

}
