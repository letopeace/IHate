using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
	[SerializeField] private PlayerManager playerManager;
	[SerializeField] private Image image;

	public bool GetCrossHairPositionInTheWorld(out Vector3 position)
	{
		bool result = playerManager.Weapon.GunRay(out RaycastHit hitinfo);
		position = hitinfo.point;
		return result;
	}

	private void Update()
	{
		CrossHairUpdate();
	}

	private void CrossHairUpdate()
	{
		Vector3 worldPos;
		bool res = GetCrossHairPositionInTheWorld(out worldPos);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		screenPos.z = 0;
		transform.position = screenPos;

		if (res)
		{
			image.enabled = true;
		}
		else
		{
			image.enabled = false;
		}
	}
}
