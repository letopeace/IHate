using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public PlayerLocomotion Locomotion
	{
		get { return locomotion; }
		private set { locomotion = value; }
	}


	[SerializeField] private PlayerLocomotion locomotion;
	[SerializeField] private HeadSimulation headSimulation;

	[Header("Links")]
	public Rigidbody rigidbody;

	private void Start()
	{
		locomotion.Init(this, rigidbody);
		headSimulation.Init(this, rigidbody);
	}

	private void Update()
	{
		locomotion.Update();
		headSimulation.Update();
	}

	private void FixedUpdate()
	{
		locomotion.FixedUpdate();
	}
}
