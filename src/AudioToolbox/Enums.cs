using System;
using Foundation;
using ObjCRuntime;

namespace AudioToolbox {
	[Flags]
	[Introduced (PlatformName.UIKitForMac, 13,0)]
	[NoWatch, TV (10, 0), Mac (10, 12), iOS (10, 0)]
	public enum AudioSettingsFlags : uint
	{
		ExpertParameter = (1u << 0),
		InvisibleParameter = (1u << 1),
		MetaParameter = (1u << 2),
		UserInterfaceParameter = (1u << 3),
	}

	[NoWatch, TV (14, 0), NoMac, iOS (14, 0)]
	public enum AUSpatialMixerOutputType : uint
	{
		Headphones = 1,
		BuiltInSpeakers = 2,
		ExternalSpeakers = 3,
	}

	[NoWatch, TV (14, 0), NoMac, iOS (14, 0)]
	public enum AUSpatialMixerPointSourceInHeadMode : uint
	{
		Mono = 0,
		Bypass = 1,
	}

	[NoWatch, TV (14, 0), NoMac, iOS (14, 0)]
	public enum AUSpatialMixerSourceMode : uint
	{
		SpatializeIfMono = 0,
		Bypass = 1,
		PointSource = 2,
		AmbienceBed = 3,
	}

}
