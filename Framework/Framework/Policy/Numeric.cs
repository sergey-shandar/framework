namespace Framework.Policy
{
	struct Numeric:
		INumeric<System.Byte>,
		INumeric<System.SByte>,
		INumeric<System.UInt16>,
		INumeric<System.Int16>,
		INumeric<System.UInt32>,
		INumeric<System.Int32>,
		INumeric<System.UInt64>,
		INumeric<System.Int64>
	{
		System.Byte INumeric<System.Byte>._0 { get { return 0; } }
		System.SByte INumeric<System.SByte>._0 { get { return 0; } }
		System.UInt16 INumeric<System.UInt16>._0 { get { return 0; } }
		System.Int16 INumeric<System.Int16>._0 { get { return 0; } }
		System.UInt32 INumeric<System.UInt32>._0 { get { return 0; } }
		System.Int32 INumeric<System.Int32>._0 { get { return 0; } }
		System.UInt64 INumeric<System.UInt64>._0 { get { return 0; } }
		System.Int64 INumeric<System.Int64>._0 { get { return 0; } }
	}
}