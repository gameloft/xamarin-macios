//
// CoreMotion/CMDeviceMotion.cs
//
// Copyright (C) 2011-2014 Xamarin Inc

using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace CoreMotion {

	// CMDeviceMotion.h
	[NoMac]
	[StructLayout (LayoutKind.Sequential)]
	public struct CMCalibratedMagneticField {
		public CMMagneticField Field;
		public CMMagneticFieldCalibrationAccuracy Accuracy;

		public override string ToString ()
		{
			return String.Format ("({0},{1})", Field, Accuracy);
		}
	}
}
