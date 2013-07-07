using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Require a character controller to be attached to the same game object
[RequireComponent (typeof(CharacterController))]
[AddComponentMenu ("2D Platformer/Platformer Controller")]
public class CharacterController2D : MonoBehaviour
{
	
	// Does this script currently respond to Input?
	public bool canControl = true;
 
	// The character will spawn at spawnPoint's position when needed.  This could be changed via a script at runtime to implement, e.g. waypoints/savepoints.
	public Transform spawnPoint;
 
	[System.SerializableAttribute]
	public class PlatformerControllerMovement
	{
		// The speed when walking 
		public float walkSpeed = 3.0f;
		// when pressing "Fire1" button (control) we start running
		public float runSpeed = 10.0f;
		public float inAirControlAcceleration = 1.0f;
 
		// The gravity for the character
		public float gravity = 60.0f;
		public float maxFallSpeed = 20.0f;
 
		// How fast does the character change speeds?  Higher is faster.
		public float speedSmoothing = 5.0f;
 
		// This controls how fast the graphics of the character "turn around" when the player turns around using the controls.
		public float rotationSmoothing = 10.0f;
 
		// The current move direction in x-y.  This will always been (1,0,0) or (-1,0,0)
		// The next line, @System.NonSerialized , tells Unity to not serialize the variable or show it in the inspector view.  Very handy for organization!
		[System.NonSerialized]
		public Vector3 direction = Vector3.zero;
 
		// The current vertical speed
		[System.NonSerialized]
		public float verticalSpeed = 0.0f;
 
		// The current movement speed.  This gets smoothed by speedSmoothing.
		[System.NonSerialized]
		public float speed = 0.0f;
 
		// Is the user pressing the left or right movement keys?
		[System.NonSerialized]
		public bool isMoving = false;
 
		// The last collision flags returned from controller.Move
		[System.NonSerialized]
		public CollisionFlags collisionFlags; 
 
		// We will keep track of an approximation of the character's current velocity, so that we return it from GetVelocity () for our camera to use for prediction.
		[System.NonSerialized]
		public Vector3 velocity;
 
		// This keeps track of our current velocity while we're not grounded?
		[System.NonSerialized]
		public Vector3 inAirVelocity = Vector3.zero;
 
		// This will keep track of how long we have we been in the air (not grounded)
		[System.NonSerialized]
		public float hangTime = 0.0f;
	}
 
	public PlatformerControllerMovement movement = new PlatformerControllerMovement();
 
	// We will contain all the jumping related variables in one helper class for clarity.
	[System.SerializableAttribute]
	public class PlatformerControllerJumping
	{
		// Can the character jump?
		public bool enabled = true;
 
		// How high do we jump when pressing jump and letting go immediately
		public float height = 1.0f;
		// We add extraHeight units (meters) on top when holding the button down longer while jumping
		public float extraHeight = 4.1f;
		public float doubleJumpHeight = 2.1f;
 
		// This prevents inordinarily too quick jumping
		// The next line, @System.NonSerialized , tells Unity to not serialize the variable or show it in the inspector view.  Very handy for organization!
		[System.NonSerialized]
		public float repeatTime = 0.05f;
		[System.NonSerialized]
		public float timeout = 0.15f;
 
		// Are we jumping? (Initiated with jump button and not grounded yet)
		[System.NonSerialized]
		public bool jumping = false;
 
		// Are where double jumping? ( Initiated when jumping or falling after pressing the jump button )
		[System.NonSerialized]
		public bool doubleJumping = false;
 
		// Can we make a double jump ( we can't make two double jump or more at the same jump )
		[System.NonSerialized]
		public bool canDoubleJump = false;
		[System.NonSerialized]
		public bool reachedApex = false;
 
		// Last time the jump button was clicked down
		[System.NonSerialized]
		public float lastButtonTime = -10.0f;
 
		// Last time we performed a jump
		[System.NonSerialized]
		public float lastTime = -1.0f;
 
		// the height we jumped from (Used to determine for how long to apply extra jump power after jumping.)
		[System.NonSerialized]
		public float lastStartHeight = 0.0f;
	}
 
	public PlatformerControllerJumping jump = new PlatformerControllerJumping();
	public CharacterController controller;
 
	// Moving platform support.
	Transform activePlatform;
	Vector3 activeLocalPlatformPoint;
	Vector3 activeGlobalPlatformPoint;
	Vector3 lastPlatformVelocity;
	bool blissedOut = false;
 
	// This is used to keep track of special effects in UpdateEffects ();
	bool areEmittersOn = false;
	
	Animator anim;
 
	void Awake ()
	{
		Messenger.AddListener(typeof(ColorMessage),HandleColorMessage);
		Messenger.AddListener(typeof(BlissedOutMessage), HandleBliss);
		Messenger.AddListener(typeof(GameLostMessage),HandleGameLostMessage);
		anim = GetComponentInChildren<Animator>();
		movement.direction = transform.TransformDirection (Vector3.forward);
		controller = GetComponent<CharacterController> ();
//		animation.AddClip(animator.jump, "jump");
//		animation.AddClip(animator.idle, "idle");
//		animation.AddClip(animator.run, "run");
//		animation.AddClip(animator.walk, "walk");
		Spawn ();
	}
	
	void HandleBliss(Message msg) {
		BlissedOutMessage bMsg = msg as BlissedOutMessage;
		if(bMsg != null) {
			if(!blissedOut && bMsg.blissingOut) {
				blissedOut = true;
				StartCoroutine(StartBlissing());
			}
			else if (blissedOut && !bMsg.blissingOut) {
				blissedOut = false;
				StartCoroutine(StopBlissing());
			}
		}
	}
	
	IEnumerator StartBlissing() {
		controller.enabled = false;
		movement.walkSpeed = 50;
		movement.velocity = Vector3.zero;
		float startTime = Time.time;
		Vector3 mySize = transform.localScale;
		Vector3 startSize = transform.localScale;
		Vector3 newSize = new Vector3(10f, 10f, 10f);
		while(Mathf.Abs(mySize.x - newSize.x) > 0.01f) {
			float transformDelta = Time.deltaTime * 7f;
			mySize = Vector3.Lerp(startSize, newSize, (Time.time - startTime) / 2f);
			Vector3 myPos = transform.position;
			myPos += new Vector3(transformDelta, transformDelta, 0f);
			transform.localScale = mySize;
			transform.position = myPos;
			yield return new WaitForSeconds(0);
		}
		transform.localScale = newSize;
		controller.enabled = true;
	}
	
	IEnumerator StopBlissing() {
		controller.enabled = false;
		movement.walkSpeed = 10;
		movement.velocity = Vector3.zero;
		movement.speed = 0f;
		movement.inAirVelocity = Vector3.zero;
		float startTime = Time.time;
		Vector3 mySize = transform.localScale;
		Vector3 startSize = transform.localScale;
		Vector3 newSize = new Vector3(1f, 1f, 1f);
		while(Mathf.Abs(mySize.x - newSize.x) > 0.01f) {
			float transformDelta = Time.deltaTime * 7f;
			mySize = Vector3.Lerp(startSize, newSize, (Time.time - startTime) / 2f);
			transform.localScale = mySize;
			yield return new WaitForSeconds(0);
		}
		controller.enabled = true;
	}
	
 	void OnDestroy()
	{
		Messenger.RemoveListener(typeof(ColorMessage),HandleColorMessage);
		Messenger.RemoveListener(typeof(BlissedOutMessage),HandleBliss);
		Messenger.RemoveListener(typeof(GameLostMessage),HandleGameLostMessage);
	}
	void Spawn ()
	{
		// reset the character's speed
		movement.verticalSpeed = 0.0f;
		movement.speed = 0.0f;
 
		// reset the character's position to the spawnPoint
		transform.position = spawnPoint.position;
 
	}
	
	public GameOver gameOver;
 
	public void HandleGameLostMessage (Message msg)
	{
		GameLostMessage message = msg as GameLostMessage;
		if(message != null) {
			LevelSectionTracker.farthestPoint = transform.position.x;
			//Spawn ();
			foreach(Transform aChild in transform) {
				aChild.gameObject.SetActive(false);
			}
			//renderer.enabled = false;
			enabled= false;
			gameOver.gameObject.SetActive(true);
		}
	}
 
	void UpdateSmoothedMovementDirection ()
	{	
		float h = Input.GetAxisRaw ("Horizontal");
		
		// Forever runner!
		h = 1.0f;
 
		if (!canControl)
			h = 0.0f;
 
		movement.isMoving = Mathf.Abs (h) > 0.1f;
 
		if (movement.isMoving)
			movement.direction = new Vector3 (h, 0f, 0f);
 
		// Grounded controls
		if (controller.isGrounded) {
			// Smooth the speed based on the current target direction
			float curSmooth = movement.speedSmoothing * Time.deltaTime;
 
			// Choose target speed
			float targetSpeed = Mathf.Min (Mathf.Abs (h), 1.0f);
 
			// Pick speed modifier
			/*
			if (Input.GetButton ("Fire2") && canControl)
				targetSpeed *= movement.runSpeed;
			else
			*/
				targetSpeed *= movement.walkSpeed;
 
			movement.speed = Mathf.Lerp (movement.speed, targetSpeed, curSmooth);
 
			movement.hangTime = 0.0f;
		} else {
			// In air controls
			movement.hangTime += Time.deltaTime;
			if (movement.isMoving)
				movement.inAirVelocity += new Vector3 (Mathf.Sign (h), 0f, 0f) * Time.deltaTime * movement.inAirControlAcceleration;
		}
	}
 
	void FixedUpdate ()
	{
		// Make sure we are absolutely always in the 2D plane.
		Vector3 myPos = transform.position;
		myPos.z = 0f;
		transform.position = myPos;
 
	}
 
	void ApplyJumping ()
	{
		// Prevent jumping too fast after each other
		if (jump.lastTime + jump.repeatTime > Time.time)
			return;
 
		if (!IsJumping() || canDoubleJump()) {
			// Jump
			// - Only when pressing the button down
			// - With a timeout so you can press the button slightly before landing		
			if (jump.enabled && Time.time < jump.lastButtonTime + jump.timeout) {
				movement.verticalSpeed = CalculateJumpVerticalSpeed (jump.height);
				movement.inAirVelocity = lastPlatformVelocity;
				SendMessage ("DidJump", SendMessageOptions.DontRequireReceiver);
				anim.SetBool("Jumping", true);
			}
		}
	}
 
	void ApplyGravity ()
	{
		// Apply gravity
		bool jumpButton = Input.GetButton ("Jump") || Input.GetButton("Fire1");
 
 
 
		if (!canControl)
			jumpButton = false;
 
		// When we reach the apex of the jump we send out a message
		if (jump.jumping && !jump.reachedApex && movement.verticalSpeed <= 0.0f) {
			jump.reachedApex = true;
			SendMessage ("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
		}
 
		// if we are jumping and we press jump button, we do a double jump or
		// if we are falling, we can do a double jump to
		if ((jump.jumping && (Input.GetButtonUp ("Jump") || Input.GetButtonUp("Fire1")) && !jump.doubleJumping) || (!controller.isGrounded && !jump.jumping && !jump.doubleJumping && movement.verticalSpeed < -12.0f)) {
			jump.canDoubleJump = true;
		} 
 
		// if we can do a double jump, and we press the jump button, we do a double jump
		if (jump.canDoubleJump && (Input.GetButtonDown ("Jump") || Input.GetButtonDown("Fire1")) && !IsTouchingCeiling ()) {
			jump.doubleJumping = true;
			movement.verticalSpeed = CalculateJumpVerticalSpeed (jump.doubleJumpHeight);
			SendMessage ("DidDoubleJump", SendMessageOptions.DontRequireReceiver);
			jump.canDoubleJump = false;
 
		}
		// * When jumping up we don't apply gravity for some time when the user is holding the jump button
		//   This gives more control over jump height by pressing the button longer
		bool extraPowerJump = jump.jumping && !jump.doubleJumping && movement.verticalSpeed > 0.0f && jumpButton && transform.position.y < jump.lastStartHeight + jump.extraHeight && !IsTouchingCeiling ();
 
		if (extraPowerJump)
			return;
		else if (controller.isGrounded) {
			movement.verticalSpeed = -movement.gravity * Time.deltaTime;
			jump.canDoubleJump = false;
		} else
			movement.verticalSpeed -= movement.gravity * Time.deltaTime;
 
		// Make sure we don't fall any faster than maxFallSpeed.  This gives our character a terminal velocity.
		movement.verticalSpeed = Mathf.Max (movement.verticalSpeed, -movement.maxFallSpeed);
	}
 
	float CalculateJumpVerticalSpeed (float targetJumpHeight)
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt (2f * targetJumpHeight * movement.gravity);
	}
 
	void DidJump ()
	{
		jump.jumping = true;
		jump.reachedApex = false;
		jump.lastTime = Time.time;
		jump.lastStartHeight = transform.position.y;
		jump.lastButtonTime = -10f;
		//animation.CrossFade("jump");
		anim.SetBool("Jumping", true);
	}
 
	void UpdateEffects ()
	{
		bool wereEmittersOn = areEmittersOn;
		areEmittersOn = jump.jumping && movement.verticalSpeed > 0.0f;
 
		// By comparing the previous value of areEmittersOn to the new one, we will only update the particle emitters when needed
		if (wereEmittersOn != areEmittersOn) {
			foreach (ParticleEmitter emitter in GetComponentsInChildren<ParticleEmitter>()) {
				emitter.emit = areEmittersOn;
			}
		}
	}
 
	void Update ()
	{
		if ((Input.GetButtonDown ("Jump") || Input.GetButtonDown("Fire1")) && canControl) {
			jump.lastButtonTime = Time.time;
			anim.SetBool("Jumping", true);
		}
		
		anim.transform.localPosition = Vector3.zero;
 
		UpdateSmoothedMovementDirection ();
 
		// Apply gravity
		// - extra power jump modifies gravity
		ApplyGravity ();
 
		// Apply jumping logic
		ApplyJumping ();
 
		// Moving platform support
		if (activePlatform != null) {
			Vector3 newGlobalPlatformPoint = activePlatform.TransformPoint (activeLocalPlatformPoint);
			Vector3 moveDistance = (newGlobalPlatformPoint - activeGlobalPlatformPoint);
			transform.position = transform.position + moveDistance;
			lastPlatformVelocity = (newGlobalPlatformPoint - activeGlobalPlatformPoint) / Time.deltaTime;
		} else {
			lastPlatformVelocity = Vector3.zero;	
		}
 
		activePlatform = null;
 
		// Save lastPosition for velocity calculation.
		Vector3 lastPosition = transform.position;
 
		// Calculate actual motion
		Vector3 currentMovementOffset = movement.direction * movement.speed + new Vector3 (0f, movement.verticalSpeed, 0f) + movement.inAirVelocity;
 
		// We always want the movement to be framerate independent.  Multiplying by Time.deltaTime does this.
		currentMovementOffset *= Time.deltaTime;
 
		// Move our character!
		movement.collisionFlags = controller.Move (currentMovementOffset);
 
		// Calculate the velocity based on the current and previous position.  
		// This means our velocity will only be the amount the character actually moved as a result of collisions.
		movement.velocity = (transform.position - lastPosition) / Time.deltaTime;
 
		// Moving platforms support
		if (activePlatform != null) {
			activeGlobalPlatformPoint = transform.position;
			activeLocalPlatformPoint = activePlatform.InverseTransformPoint (transform.position);
		}
 
		// Set rotation to the move direction	
		if (movement.direction.sqrMagnitude > 0.01f)
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (movement.direction), Time.deltaTime * movement.rotationSmoothing);
 
		// We are in jump mode but just became grounded
		if (controller.isGrounded) {
			movement.inAirVelocity = Vector3.zero;
			if (jump.jumping) {
				jump.jumping = false;
				anim.SetBool("Jumping", false);
				jump.doubleJumping = false;
				jump.canDoubleJump = false;
				SendMessage ("DidLand", SendMessageOptions.DontRequireReceiver);
 
				Vector3 jumpMoveDirection = movement.direction * movement.speed + movement.inAirVelocity;
				if (jumpMoveDirection.sqrMagnitude > 0.01f)
					movement.direction = jumpMoveDirection.normalized;
			}
		}	
 
		// Update special effects like rocket pack particle effects
		UpdateEffects ();
		
		// Update Animations
		UpdateAnimations();
		
	}
	
	void UpdateAnimations() 
	{
		anim.SetFloat("Speed", movement.velocity.x);
		//anim.SetBool("Jumping", IsJumping());
		if(IsMoving() && !IsJumping() && Mathf.Abs(movement.velocity.x) > 5f) {
		//	animation.CrossFade("run");
		}
		else if(!IsJumping() && Mathf.Abs(movement.velocity.x) > 0.5f) {
		//	animation.CrossFade("walk");
		}
		else if(!IsMoving() && Mathf.Abs(movement.velocity.x) < 0.5f && !IsJumping()) {
		//	animation.CrossFade("idle");
		}
		else if(IsJumping()) {
			
		//	if(!animation.IsPlaying("jump"))
		//		animation.Play("jump");
		}
	}
 
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if (hit.moveDirection.y > 0.01f) 
			return;
 
		// Make sure we are really standing on a straight platform
		// Not on the underside of one and not falling down from it either!
		if (hit.moveDirection.y < -0.9f && hit.normal.y > 0.9f) {
			activePlatform = hit.collider.transform;	
		}
	}
 
// Various helper functions below:
	public float GetSpeed ()
	{
		return movement.speed;
	}
 
	public Vector3 GetVelocity ()
	{
		return movement.velocity;
	}
 
	public bool IsMoving ()
	{
		return movement.isMoving;
	}
 
	public bool IsJumping ()
	{
		return jump.jumping;
	}
 
	public bool IsDoubleJumping ()
	{
		return jump.doubleJumping;
	}
 
	public bool canDoubleJump ()
	{
		return jump.canDoubleJump;
	}
 
	public bool IsTouchingCeiling ()
	{
		return (movement.collisionFlags & CollisionFlags.CollidedAbove) != 0;
	}
 
	public Vector3 GetDirection ()
	{
		return movement.direction;
	}
 
	public float GetHangTime ()
	{
		return movement.hangTime;
	}
 
	public void Reset ()
	{
		gameObject.tag = "Player";
	}
 
	public void SetControllable (bool controllable)
	{
		canControl = controllable;
	}
	float MAX_GRAVITY = 60;
	float MIN_GRAVITY = 20;
	float MIN_WALK_SPEED = 10;
	float MAX_WALK_SPEED = 15;
	
	public float MIN_JUMP_HEIGHT = 1;
	public float MAX_JUMP_HEIGHT = 10;
	void HandleColorMessage(Message msg) {
		ColorMessage message = msg as ColorMessage;
		if(message != null) {
			movement.gravity = MAX_GRAVITY-message.Percent*MIN_GRAVITY;
			float j = MIN_JUMP_HEIGHT+message.Percent*(MAX_JUMP_HEIGHT-MIN_JUMP_HEIGHT);
			jump.height = j;
			jump.extraHeight = 2*j;
			jump.doubleJumpHeight = 2*j;
			//movement.walkSpeed = message.Percent*(MAX_WALK_SPEED-MIN_WALK_SPEED)+MIN_WALK_SPEED;
		}
	}
}