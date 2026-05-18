using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerLocomotion
{
	public float Speed;						// movement basic speed
	public float SpeedMultiplier = 1.5f;	// Speed * multiplier | when sprinting
	public float JumpStrength;				// Jump Height
	public float JumpCD = 0.2f;				// Wait after jumped
	public bool onGround = true;

	public LayerMask groundLayer;
	public Transform Head;					// Head pivot

	private PlayerManager playerManager;	// Base PlayerClass
	private Transform transform;			// Player Transform
	private Rigidbody rb;

	private float mouseX;
	private float mouseY;

	private bool inAir = false;				// true when ready to check ground
	private bool isSprinting = false;		// true when sprinting

	public void Init(PlayerManager manager)
	{
		playerManager = manager;
		transform = playerManager.transform;
		rb = playerManager.rigidbody;
	}


	public void Update()
	{
		
		CameraRotation();
		Sprint();
		Jump();
		GroundCheck();
	}

	public void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");
		float y = rb.linearVelocity.y;
		Vector3 dir = (transform.forward * v + transform.right * h);
		dir.Normalize();
		dir *= Speed * Time.fixedDeltaTime;
		dir = isSprinting ? dir * SpeedMultiplier : dir;
		dir.y = y;
		rb.linearVelocity = dir;
	}

	private void Sprint()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			isSprinting = true;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			isSprinting = false;
		}
	}

	private void Jump()
	{
		if (onGround && Input.GetKeyDown(KeyCode.Space))
		{
			Vector3 dir = rb.linearVelocity;
			dir.y = JumpStrength;
			rb.linearVelocity = dir;
			onGround = false;

			WaitAndExecute(JumpCD, () => inAir = true);
		}
	}

	private void CameraRotation()
	{
		mouseX += Input.GetAxis("Mouse X");
		mouseY += Input.GetAxis("Mouse Y");

		mouseY = Mathf.Clamp(mouseY, -90, 90);

		Vector3 transformRotation = new Vector3 (0, mouseX, 0);
		Vector3 headRotation = new Vector3 (-mouseY, mouseX, 0);

		transform.eulerAngles = transformRotation;
		Head.eulerAngles = headRotation;
	}

	private void GroundCheck()
	{
		if (inAir)
		{
			if (IsOnGround())
			{
				inAir = false;
				onGround = true;
			}
		}

		if (rb.linearVelocity.y < -0.1f)
		{
			inAir = true;
		}
	}

	private bool IsOnGround()
	{
		return Physics.Raycast(transform.position + (Vector3.up * 0.02f), Vector3.down, out RaycastHit hit, 0.05f, groundLayer);
	}
	private bool IsOnGround(out RaycastHit hit)
	{
		return Physics.Raycast(transform.position, Vector3.down, out hit, groundLayer);
	}

	private IEnumerator WaitAndExecute(float duration, Action action)
	{
		yield return new WaitForSeconds(duration);
		action.Invoke();
	}

}
