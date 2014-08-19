using System;
using System.Collections.Generic;

internal static class IMobileSpotInfoManager
{
	private static Dictionary<string, List<string>> spotInfoDictionary = new Dictionary<string, List<string>>();

	private enum SpotInfo{
		PartnerId ,
		MediaId
	}

    internal static void SetSpotInfo(string spotId, string partnerId, string mediaId){
		List<string> spotInfo = new List<string> {partnerId, mediaId};
        if (spotInfoDictionary.ContainsKey (spotId)) {
            spotInfoDictionary [spotId] = spotInfo;
        } else {
            spotInfoDictionary.Add(spotId, spotInfo);
        }
	}

    internal static string GetPartnerId(string spotId){
		return spotInfoDictionary[spotId][(int)SpotInfo.PartnerId];
	}

    internal static string GetMediaId(string spotId){
		return spotInfoDictionary[spotId][(int)SpotInfo.MediaId];
	}
}