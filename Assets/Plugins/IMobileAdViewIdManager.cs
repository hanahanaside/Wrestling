using System;
using System.Collections.Generic;

internal static class IMobileAdViewIdManager
{
	private static int adViewIdCounter = 100000;	

    internal static int createId()
	{
		return adViewIdCounter ++;
	}
}

