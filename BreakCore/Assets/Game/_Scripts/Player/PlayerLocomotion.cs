using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerLocomotion
{
	public float Speed;
	public float SpeedMultiplier = 1.5f;
	public float JumpStrength;
	public float JumpCD = 0.2f;
	public bool onGround = true;

	public LayerMask groundLayer;
	public Transform Head;

	private PlayerManager playerManager;
	private Transform transform;
	private Rigidbody rb;

	private float x;
	private float y;

	private bool inAir = false;
	private bool isSprinting = false;

	public void Init(PlayerManager manager, Rigidbody rigidbody)
	{
		playerManager = manager;
		transform = playerManager.transform;
		rb = rigidbody;
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
		x += Input.GetAxis("Mouse X");
		y += Input.GetAxis("Mouse Y");

		y = Mathf.Clamp(y, -90, 90);

		Vector3 transformRotation = new Vector3 (0, x, 0);
		Vector3 headRotation = new Vector3 (-y, x, 0);

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
