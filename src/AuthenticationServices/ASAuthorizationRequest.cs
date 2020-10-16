//
// ASAuthorization.cs
//
// Authors:
//	Alex Soto <alexsoto@microsoft.com>
//
// Copyright 2019 Microsoft Corporation
//

using Foundation;
using ObjCRuntime;

namespace AuthenticationServices {
#if !MONOMAC
    public partial class ASAuthorizationRequest {
			public T GetProvider<T> () where T : NSObject, IASAuthorizationProvider => Runtime.GetINativeObject<T> (_Provider, false);
	}
#endif
}
