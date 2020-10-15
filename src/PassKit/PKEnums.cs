// Copyright 2012-2014 Xamarin Inc. All rights reserved.

using System;
using Foundation;
using ObjCRuntime;

namespace PassKit {

	// untyped enum -> PKError.h
	// This never seemed to be deprecatd, yet in iOS8 it's obsoleted
	[Obsoleted (PlatformName.iOS, 8, 0)]
	[NoMac]
	public enum PKErrorCode {
		None = 0,
		Unknown = 1,
		NotEntitled,
		PermissionDenied, // new in iOS7
	}

	// NSInteger -> PKPass.h
	[NoMac]
	[ErrorDomain ("PKPassKitErrorDomain")]
	[Native]
	public enum PKPassKitErrorCode : long {
		Unknown = -1,
		None = 0,
		InvalidData = 1,
		UnsupportedVersion,
		InvalidSignature,
		[iOS (8,0)]
		NotEntitled
	}

	// NSInteger -> PKPassLibrary.h
	[iOS (7,0)]
	[NoMac]
	[Native]
	public enum PKPassLibraryAddPassesStatus : long {
		DidAddPasses,
		ShouldReviewPasses,
		DidCancelAddPasses
	}

	[NoMac]
	[Native]
	public enum PKPassType : ulong {
		Barcode,
		SecureElement,
		[NoMac]
		[Deprecated (PlatformName.iOS, 13, 4, message: "Use 'SecureElement' instead.")]
		[Deprecated (PlatformName.WatchOS, 6, 2, message: "Use 'SecureElement' instead.")]
		Payment = SecureElement,
		Any = ulong.MaxValue,
	}

	[NoMac]
	[Watch (3,0)]
	[Native]
	public enum PKPaymentAuthorizationStatus : long {
		Success,
		Failure,

		[NoMac]
		[Deprecated (PlatformName.WatchOS, 4,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentBillingAddressInvalidError'.")]
		[Deprecated (PlatformName.iOS, 11,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentBillingAddressInvalidError'.")]
		InvalidBillingPostalAddress,

		[NoMac]
		[Deprecated (PlatformName.WatchOS, 4,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentShippingAddressInvalidError'.")]
		[Deprecated (PlatformName.iOS, 11,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentShippingAddressInvalidError'.")]
		InvalidShippingPostalAddress,

		[NoMac]
		[Deprecated (PlatformName.WatchOS, 4,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentContactInvalidError'.")]
		[Deprecated (PlatformName.iOS, 11,0, message: "Use 'Failure' and 'PKPaymentRequest.CreatePaymentContactInvalidError'.")]
		InvalidShippingContact,

		[iOS (9,2)]
		PinRequired,
		[iOS (9,2)]
		PinIncorrect,
		[iOS (9,2)]
		PinLockout
	}

	[NoMac]
	[Deprecated (PlatformName.iOS, 13, 4, message: "Use 'PKSecureElementPassActivationState' instead.")]
	[Deprecated (PlatformName.WatchOS, 6, 2, message: "Use 'PKSecureElementPassActivationState' instead.")]
	[Native]
	public enum PKPaymentPassActivationState : ulong {
		Activated, RequiresActivation, Activating, Suspended, Deactivated
	}

	[NoMac]
	[Watch (6,2), iOS (13,4)]
	[Native]
	public enum PKSecureElementPassActivationState : long {
		Activated,
		RequiresActivation,
		Activating,
		Suspended,
		Deactivated,
	}

	[NoMac]
	[Watch (3,0)]
	[Native]
	public enum PKMerchantCapability : ulong {
		ThreeDS = 1 << 0,
		EMV = 1 << 1,
		Credit = 1 << 2,
		Debit = 1 << 3
	}

	[NoMac]
	[Watch (3,0)]
	[Deprecated (PlatformName.iOS, 11,0, message: "Use 'PKContactField' instead.")]
	[Deprecated (PlatformName.WatchOS, 4,0, message: "Use 'PKContactField' instead.")]
	[Native]
	[Flags]
	public enum PKAddressField : ulong {
		None = 0,
		PostalAddress = 1 << 0,
		Phone = 1 << 1,
		Email = 1 << 2,
		[iOS (8,3)]
		Name = 1 << 3,
		All = PostalAddress|Phone|Email|Name
	}

	[NoMac]
	[NoWatch]
	[iOS (8,3)]
	[Native]
	public enum PKPaymentButtonStyle : long {
		White,
		WhiteOutline,
		Black,
	}

	[NoMac]
	[NoWatch]
	[iOS (8,3)]
	[Native]
	public enum PKPaymentButtonType : long {
		Plain,
		Buy,
		[iOS (9,0)]
		SetUp,
		[iOS (10,0)]
		InStore,
		[iOS (10,2)]
		Donate,
	}

	[NoMac]
	[Watch (3,0)]
	[iOS (8,3)]
	[Native]
	public enum PKShippingType : ulong {
		Shipping,
		Delivery,
		StorePickup,
		ServicePickup,
	}

	[Watch (6,0)]
	[iOS (9,0)]
	[NoMac]
	[Native]
	public enum PKAddPaymentPassError : long
	{
		Unsupported,
		UserCancelled,
		SystemCancelled
	}

	[NoMac]
	[NoWatch]
	[iOS (9,0)]
	[Native]
	public enum PKAutomaticPassPresentationSuppressionResult : ulong
	{
		NotSupported = 0,
		AlreadyPresenting,
		Denied,
		Cancelled,
		Success
	}

	[NoMac]
	[Watch (3,0)]
	[iOS (9,0)]
	[Native]
	public enum PKPaymentMethodType : ulong
	{
		Unknown = 0,
		Debit,
		Credit,
		Prepaid,
		Store
	}

	[NoMac]
	[Watch (3,0)]
	[iOS (9,0)]
	[Native]
	public enum PKPaymentSummaryItemType : ulong
	{
		Final,
		Pending
	}

	[NoWatch]
	[NoMac] // under `#if TARGET_OS_IOS`
	[iOS (9,0)]
	[Native]
	public enum PKAddPassButtonStyle : long {
		Black = 0,
		Outline
	}

	[NoMac]
	[Watch (4,0)][iOS (11,0)]
	[ErrorDomain ("PKPaymentErrorDomain")]
	[Native]
	public enum PKPaymentErrorCode : long {
		Unknown = -1,
		ShippingContactInvalid = 1,
		BillingContactInvalid,
		ShippingAddressUnserviceable,
	}

	[iOS (12,0)]
	[NoMac]
	[NoWatch] // https://feedbackassistant.apple.com/feedback/6301809 https://github.com/xamarin/maccore/issues/1819
	[Native]
	public enum PKAddPaymentPassStyle : ulong {
		Payment,
		Access,
	}

	[Watch (6,2), iOS (13,4)]
	[NoMac]
	[ErrorDomain ("PKAddSecureElementPassErrorDomain")]
	[Native]
	public enum PKAddSecureElementPassErrorCode : long {
		UnknownError,
		UserCanceledError,
		UnavailableError,
		InvalidConfigurationError,
		DeviceNotSupportedError,
		DeviceNotReadyError
	}
}
