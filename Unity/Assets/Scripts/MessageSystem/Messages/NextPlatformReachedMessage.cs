using UnityEngine;
using System.Collections;

public class NextPlatformReachedMessage : Message {
	int mPlatformNumber;
	public int PlatformNumber {
		get {
			return mPlatformNumber;
		}
	}
	public NextPlatformReachedMessage(PlatformGenerator pg) {
		mPlatformNumber = pg.platformNumber;
	}
}
