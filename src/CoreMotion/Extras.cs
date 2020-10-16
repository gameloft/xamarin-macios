//
// CoreMotion's extra methods
//
using Foundation;
using System;

namespace CoreMotion {

	public partial class CMAccelerometerData {
#if !MONOMAC
        public override string ToString ()
		{
			return String.Format ("t={0} {1}", Acceleration.ToString (), Timestamp);
		}
#endif
    }
}
