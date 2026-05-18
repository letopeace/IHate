using System;
using UnityEngine;

[Serializable]
public class HeadSimulation
{
	public float Damping;
	public float Strength;
	public float ShakingSpeed;

	public Vector2 NoiseStrength;
	public Transform Camera;

	private PlayerManager player;
	private Rigidbody rb;

	private Vector3 velocity = Vector3.zero;

	private float tick;

	public void Init(PlayerManager playerManager, Rigidbody rigidbody)
	{
		player = playerManager;
		rb = rigidbody;
	}

	public void Update()
	{
		tick += Time.deltaTime * ShakingSpeed * rb.linearVelocity.magnitude * 0.2f;

		velocity = Vector3.Lerp(velocity, rb.linearVelocity, Time.deltaTime * Damping);

		Vector3 shake = Vector3.up * (Mathf.PerlinNoise(0, Time.timeSinceLevelLoad) * Mathf.Sin(tick) * Mathf.Clamp01(velocity.magnitude) * 1.5f);
		Vector3 lowShake = ((Camera.transform.right * Mathf.PerlinNoise(Time.timeSinceLevelLoad, 0) * NoiseStrength.x) + (Vector3.up * Mathf.PerlinNoise(0, Time.timeSinceLevelLoad) * NoiseStrength.y)) * (1 - Mathf.Clamp01(velocity.magnitude * 0.1f));

		Vector3 dir = velocity + (Vector3.down * velocity.magnitude * 0.3f);

		if (player.Locomotion.onGround)
		{
			dir += shake;
			dir += lowShake;
		}
		Camera.position = (dir * Strength) + Camera.parent.position;

		Debug.Log($"velocity.magnitude * 0.1f: {velocity.magnitude * 0.1f}");
	}
}
