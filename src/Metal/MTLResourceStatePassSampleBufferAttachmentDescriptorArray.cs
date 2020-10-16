using System;

namespace Metal {

#if !TVOS

	public partial class MTLResourceStatePassSampleBufferAttachmentDescriptorArray {
#if !MONOMAC
        public MTLResourceStatePassSampleBufferAttachmentDescriptor this[nuint i] {
 			get => GetObject (i);
			set => SetObject (value, i);
		}
#endif
    }

#endif
}
