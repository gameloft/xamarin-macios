// Copyright 2019 Microsoft Corporation

using System;

using CoreFoundation;
using Foundation;
using ObjCRuntime;

namespace ExecutionPolicy {

	[NoMac]
	[Native]
	public enum EPDeveloperToolStatus : long {
		NotDetermined = 0,
		Restricted,
		Denied,
		Authorized,
	}

	[NoMac]
	[Native]
	[ErrorDomain ("EPErrorDomain")]
	public enum EPError : long {
		Generic = 1,
		NotADeveloperTool,
	}

	[NoMac]
	[BaseType (typeof (NSObject))]
	interface EPDeveloperTool {

		[Export ("authorizationStatus")]
		EPDeveloperToolStatus AuthorizationStatus { get; }

		[Export ("requestDeveloperToolAccessWithCompletionHandler:")]
		void RequestDeveloperToolAccess (Action<bool> handler);
	}

	[NoMac]
	[BaseType (typeof (NSObject))]
	interface EPExecutionPolicy {

		[Export ("addPolicyExceptionForURL:error:")]
		bool AddPolicyException (NSUrl url, [NullAllowed] out NSError error);
	}
}
