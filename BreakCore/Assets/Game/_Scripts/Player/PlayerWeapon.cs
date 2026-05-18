using System;
using UnityEngine;

[Serializable]
public class PlayerWeapon
{
	public float GunDamping;
	public Vector3 GunOffset;
	public Transform GunPivot;
	public LayerMask GunReactMask;

	private Transform camera;


	public void Init(PlayerManager manager)
	{
		camera = manager.cameraPivot;
	}

	public void Update()
	{
		Follow();
		LookAt();
	}

	private void Follow()
	{
		Vector3 offset = (camera.forward * GunOffset.z) + (camera.right * GunOffset.x) + (camera.up * GunOffset.y);
		GunPivot.position = Vector3.Lerp(GunPivot.position, camera.position + offset, GunDamping * Time.deltaTime);
	}

	private void LookAt()
	{
		if (Ray(out RaycastHit hitInfo))
		{
			var rot = Quaternion.LookRotation(hitInfo.point - GunPivot.position, Vector3.up);
			GunPivot.rotation = Quaternion.Lerp(GunPivot.rotation, rot, GunDamping * Time.deltaTime);
		}
		else
		{
			GunPivot.rotation = Quaternion.Lerp(GunPivot.rotation, camera.rotation, GunDamping * Time.deltaTime);
		}
	}

	private bool Ray(out RaycastHit hit)
	{
		return Physics.Raycast(camera.position, camera.forward, out hit, 500, GunReactMask);
	}
}
