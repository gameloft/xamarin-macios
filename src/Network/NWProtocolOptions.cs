//
// NWProtocolOptions.cs: Bindings the Netowrk nw_protocol_options API.
//
// Authors:
//   Miguel de Icaza (miguel@microsoft.com)
//
// Copyrigh 2018 Microsoft Inc
//
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ObjCRuntime;
using Foundation;
using CoreFoundation;

using OS_nw_protocol_definition=System.IntPtr;
using OS_nw_protocol_options=System.IntPtr;

namespace Network {
	
	public class NWProtocolOptions : NativeObject {
		public NWProtocolOptions (IntPtr handle, bool owns) : base (handle, owns) {}

		[TV (12,0), Mac (10,14), iOS (12,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern OS_nw_protocol_definition nw_protocol_options_copy_definition (OS_nw_protocol_options options);

		public NWProtocolDefinition ProtocolDefinition => new NWProtocolDefinition (nw_protocol_options_copy_definition (GetHandle()), owns: true);

		[TV (12,0), Mac (10,14), iOS (12,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_ip_options_set_version (OS_nw_protocol_options options, NWIPVersion version);

		[TV (12,0), Mac (10,14), iOS (12,0)]
		public void SetVersion (NWIPVersion version)
		{
			nw_ip_options_set_version (GetHandle(), version);
		}

		[TV (12,0), Mac (10,14), iOS (12,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_ip_options_set_hop_limit (OS_nw_protocol_options options, byte hop_limit);
	
		public void SetHopLimit (byte hopLimit)
		{
			nw_ip_options_set_hop_limit (GetHandle(), hopLimit);
		}

		[TV (12,0), Mac (10,14), iOS (12,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_ip_options_set_use_minimum_mtu (OS_nw_protocol_options options, [MarshalAs(UnmanagedType.I1)]bool use_minimum_mtu);
	
		[TV (12,0), Mac (10,14), iOS (12,0)]
		public void SetUseMinimumMtu (bool useMinimumMtu)
		{
			nw_ip_options_set_use_minimum_mtu (GetHandle(), useMinimumMtu);
		}

		[TV (12,0), Mac (10,14), iOS (12,0)]
		[DllImport (Constants.NetworkLibrary)]
		static extern void nw_ip_options_set_disable_fragmentation (OS_nw_protocol_options options, [MarshalAs(UnmanagedType.I1)]bool disable_fragmentation);

		[TV (12,0), Mac (10,14), iOS (12,0)]
		public void SetDisableFragmentation (bool disableFragmentation)
		{
			nw_ip_options_set_disable_fragmentation (GetHandle(), disableFragmentation);
		}

		
	}
}
