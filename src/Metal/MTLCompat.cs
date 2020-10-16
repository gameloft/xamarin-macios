#if !XAMCORE_4_0
using System;

using Foundation;
using ObjCRuntime;

namespace Metal {

	public partial class MTLSharedTextureHandle {

		[Obsolete ("Type is not meant to be created by user code.")]
		public MTLSharedTextureHandle () {}
	}


}
#endif // !XAMCORE_4_0
