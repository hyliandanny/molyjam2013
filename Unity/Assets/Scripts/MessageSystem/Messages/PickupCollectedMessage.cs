public class PickupCollectedMessage : Message {
	PickupClass mPickup;
	public PickupClass Pickup {
		get {
			return mPickup;
		}
	}
	public PickupCollectedMessage(PickupClass pickup) {
		mPickup = pickup;
	}
}
