//
// NWAdvertiseDescriptor.cs: Bindings the Netowrk nw_advertise_descriptor_t API
//
// Authors:
//   Miguel de Icaza (miguel@microsoft.com)
//
// Copyrigh 2018 Microsoft Inc
//
#nullable enable

using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using Foundation;
using CoreFoundation;

using nw_advertise_descriptor_t=System.IntPtr;
using OS_nw_advertise_descriptor=System.IntPtr;
using OS_nw_txt_record=System.IntPtr;

namespace Network {
	[TV (12,0), Mac (10,14), iOS (12,0)]
	[Watch (6,0)]
	public class NWAdvertiseDescriptor : NativeObject {
		public NWAdvertiseDescriptor (IntPtr handle, bool owns) : base (handle, owns)
		{ }

		[DllImport (Constants.NetworkLibrary)]
		static extern IntPtr nw_advertise_descriptor_create_bonjour_service (string name, string type, string? domain);

		public static NWAdvertiseDescriptor? CreateBonjourService (string name, string type, string? domain = null)
		{
			if (name == null)
				throw new ArgumentNullException (nameof (name));

			if (type == null)
				throw new ArgumentNullException (nameof (type));

			var x = nw_advertise_descriptor_create_bonjour_service (name, type, domain);
			if (x == IntPtr.Zero)
				return null;
			return new NWAdvertiseDescriptor (x, owns: true);
		}

		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_advertise_descriptor_set_txt_record (IntPtr handle, string txtRecord, nuint txtLen);

		public void SetTxtRecord (string txt)
		{
			if (txt == null)
				throw new ArgumentNullException (nameof (txt));
			var n = System.Text.Encoding.UTF8.GetByteCount (txt);
			nw_advertise_descriptor_set_txt_record (GetCheckedHandle (), txt, (nuint) n);
		}

		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_advertise_descriptor_set_no_auto_rename (IntPtr handle, [MarshalAs (UnmanagedType.I1)] bool no_auto_rename);

		[DllImport (Constants.NetworkLibrary)]
		[return: MarshalAs (UnmanagedType.I1)]
		static extern bool nw_advertise_descriptor_get_no_auto_rename (IntPtr handle);

		public bool NoAutoRename {
			set => nw_advertise_descriptor_set_no_auto_rename (GetCheckedHandle (), value);
			get => nw_advertise_descriptor_get_no_auto_rename (GetCheckedHandle ());
		}

		[TV (13,0), NoMac, iOS (13,0), Watch (6,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern OS_nw_txt_record nw_advertise_descriptor_copy_txt_record_object (OS_nw_advertise_descriptor advertise_descriptor);

		[TV (13,0), NoMac, iOS (13,0), Watch (6,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_advertise_descriptor_set_txt_record_object (OS_nw_advertise_descriptor advertise_descriptor, OS_nw_txt_record txt_record);

		[TV (13,0), NoMac, iOS (13,0), Watch (6,0)]
		public NWTxtRecord TxtRecord {
			get => new NWTxtRecord (nw_advertise_descriptor_copy_txt_record_object (GetCheckedHandle ()), owns: true); 
			set => nw_advertise_descriptor_set_txt_record_object (GetCheckedHandle (), value.GetHandle ()); 
		}
	}
}
