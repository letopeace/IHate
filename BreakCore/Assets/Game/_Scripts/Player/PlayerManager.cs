using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public PlayerLocomotion Locomotion			// for easy access to bool onGround
	{
		get { return locomotion; }
		private set { locomotion = value; }
	}
	public PlayerWeapon Weapon					// for easy access to crosshiar position
	{
		get { return weapon; }
		private set { weapon = value; }
	}

	[SerializeField] private PlayerLocomotion locomotion;
	[SerializeField] private HeadSimulation headSimulation;
	[SerializeField] private PlayerWeapon weapon;

	[Header("Links")]				// links that need for other classes 
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
