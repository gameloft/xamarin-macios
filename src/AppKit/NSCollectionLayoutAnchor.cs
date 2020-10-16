using System;
using System.Runtime.InteropServices;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace AppKit {

	[NoMac]
	public enum NSCollectionLayoutAnchorOffsetType {
		Absolute,
		Fractional,
	}

#if !MONOMAC
    [NoMac]
	public partial class NSCollectionLayoutAnchor {
		public static NSCollectionLayoutAnchor Create (NSDirectionalRectEdge edges, NSCollectionLayoutAnchorOffsetType offsetType, CGPoint offset) =>
		    offsetType switch
		    {
			    NSCollectionLayoutAnchorOffsetType.Absolute   => _LayoutAnchorWithEdgesAbsoluteOffset (edges, offset),
			    NSCollectionLayoutAnchorOffsetType.Fractional => _LayoutAnchorWithEdgesFractionalOffset (edges, offset),
			    _                                             => throw new ArgumentException (message: "Invalid enum value", paramName: nameof (offsetType)),
		    };
	}
#endif
}
