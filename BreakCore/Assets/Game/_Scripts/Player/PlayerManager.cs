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
	[SerializeField] private PlayerWeapon weapon;

	[Header("Links")]
	public Rigidbody rigidbody;
	public Transform cameraPivot;

	private void Start()
	{
		locomotion.Init(this);
		headSimulation.Init(this);
		weapon.Init(this);
	}

	private void Update()
	{
		locomotion.Update();
		headSimulation.Update();
		weapon.Update();
	}

	private void FixedUpdate()
	{
		locomotion.FixedUpdate();
	}
}
