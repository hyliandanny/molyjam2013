public class PlayerDistanceMessage : Message {
	float mDistance;
	public float Distance {
		get {
			return mDistance;
		}
	}
	public PlayerDistanceMessage(float distance) {
		mDistance = distance;
	}
}
