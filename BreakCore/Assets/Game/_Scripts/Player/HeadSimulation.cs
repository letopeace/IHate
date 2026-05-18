using System;
using UnityEngine;

[Serializable]
public class HeadSimulation
{
	public float Damping;			// Position Lerp
	public float Strength;			// value how mush effect will seen
	public float ShakingSpeed;		// Sin Frequency when running

	public Vector2 NoiseStrength;	// noise amplitude when standing

	private PlayerManager player;	// basic class
	private Rigidbody rb;
	private Transform cameraPivot;	

	private Vector3 velocity = Vector3.zero;	// camera velocity 

	private float tick;				// custom time counter

	public void Init(PlayerManager playerManager)
	{
		player = playerManager;
		rb = player.rigidbody;
		cameraPivot = player.cameraPivot;
	}

	public void Update()
	{
		tick += Time.deltaTime * ShakingSpeed * rb.linearVelocity.magnitude * 0.2f;

		velocity = Vector3.Lerp(velocity, rb.linearVelocity, Time.deltaTime * Damping);

		Vector3 shake = Vector3.up * (Mathf.PerlinNoise(0, Time.timeSinceLevelLoad) * Mathf.Sin(tick) * Mathf.Clamp01(velocity.magnitude) * 1.5f);
		Vector3 lowShake = ((cameraPivot.transform.right * Mathf.PerlinNoise(Time.timeSinceLevelLoad, 0) * NoiseStrength.x) + (Vector3.up * Mathf.PerlinNoise(0, Time.timeSinceLevelLoad) * NoiseStrength.y)) * (1 - Mathf.Clamp01(velocity.magnitude * 0.1f));

		Vector3 dir = velocity + (Vector3.down * velocity.magnitude * 0.3f);

		if (player.Locomotion.onGround)
		{
			dir += shake;
			dir += lowShake;
		}
		cameraPivot.position = (dir * Strength) + cameraPivot.parent.position;
	}
}
