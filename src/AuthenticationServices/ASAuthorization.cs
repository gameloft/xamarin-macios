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
    public partial class ASAuthorization {
		public T GetProvider<T> () where T : NSObject, IASAuthorizationProvider => Runtime.GetINativeObject<T> (_Provider, false);

		public T GetCredential<T> () where T : NSObject, IASAuthorizationCredential => Runtime.GetINativeObject<T> (_Credential, false);
	}
#endif
}
