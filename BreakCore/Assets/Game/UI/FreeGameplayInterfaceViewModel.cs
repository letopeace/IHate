using UnityEngine;

public class FreeGameplayInterfaceViewModel
{
	[SerializeField] private PlayerManager playerManager;

	public bool GetCrossHairPositionInTheWorld(out Vector3 position)
	{
		bool result = playerManager.Weapon.GunRay(out RaycastHit hitinfo);
		position = hitinfo.point;
		return result;
	}
}
