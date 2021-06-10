using UnityEngine;

/// <summary>
/// Handles inputs for the player. It serves two main purposes: 1) wrap up
/// inputs so swapping between mobile and standalone is simpler and 2) keeping inputs
/// from Update() in sync with FixedUpdate()
/// </summary>
[DefaultExecutionOrder(-100)] //ensure this script runs before all other player scripts to prevent laggy input
public class PlayerInput : MonoBehaviour
{

	[HideInInspector] public float horizontal;      // stores horizontal input value (later clamped between -1 and 1)
	[HideInInspector] public float vertical;        // stores vertical input value (later clamped between -1 and 1)
	[HideInInspector] public bool jumpHeld;
	[HideInInspector] public bool jumpPressed;
	[HideInInspector] public bool crouchHeld;
	[HideInInspector] public bool crouchPressed;
	//bool dPadCrouchPrev;							// Previous values of touch Thumbstick
	bool readyToClear;                              // Used to keep input in sync


	public static PlayerInput Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Update()
	{
		// Clear out existing input values
		ClearInput();

		// Process keyboard, mouse, gamepad (etc) inputs
		ProcessInputs();

		// Clamp the horizontal input to be between -1 and 1
		horizontal = Mathf.Clamp(horizontal, -1f, 1f);
		vertical = Mathf.Clamp(vertical, -1f, 1f);

	}

	void FixedUpdate()
	{
		// In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
		// next Update(). This ensures that all code gets to use the current inputs
		readyToClear = true;
	}

	void ClearInput()
	{
		// If we're not ready to clear input, exit
		if (!readyToClear)
			return;

		// Reset all inputs
		horizontal		= 0f;
		jumpPressed		= false;
		jumpHeld		= false;
		crouchPressed	= false;
		crouchHeld		= false;

		readyToClear	= false;
	}

	void ProcessInputs()
	{

		horizontal		= Input.GetAxis("Horizontal");
		vertical		= Input.GetAxis("Vertical");
		jumpPressed		= jumpPressed || Input.GetButtonDown("Jump");
		jumpHeld		= jumpHeld || Input.GetButton("Jump");
		//crouchPressed	= crouchPressed || Input.GetButtonDown("Crouch");
		crouchHeld		= crouchHeld || Input.GetButton("Crouch") || Input.GetAxis("Crouch") > 0f;

		// Modify input settings in Project Setting --> Input Manager
	}

}
