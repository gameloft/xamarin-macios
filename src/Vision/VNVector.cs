//
// VNVector.cs
//
// Authors:
//	Alex Soto  <alexsoto@microsoft.com>
//
// Copyright (c) Microsoft Corporation.
//

using System;
using Foundation;
using ObjCRuntime;

namespace Vision {
#if !MONOMAC
    public partial class VNVector {

		public static VNVector Create (double r, double theta)
		{
			var handle = Messaging.IntPtr_objc_msgSend (class_ptr, Selector.GetHandle ("alloc"));
			handle = Messaging.IntPtr_objc_msgSend_Double_Double (handle, Selector.GetHandle ("initWithR:theta:"), r, theta);
			return Runtime.GetNSObject<VNVector> (handle, true);
		}
	}
#endif
}
