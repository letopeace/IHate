using System;
using UnityEngine;

[Serializable]
public class PlayerWeapon
{
	public float GunDamping;  // affect to position lerp and rotation lerp
	public Vector3 GunOffset;  // local position of gun in the head
	public Transform GunPivot;  // The gun holder transform
	public LayerMask GunReactMask;  // just not player to avoid self collising

	private Transform cameraPivot; 


	public void Init(PlayerManager manager)
	{
		cameraPivot = manager.cameraPivot;
	}

	public void Update()
	{
		Follow();
		LookAt();
	}

	private void Follow()
	{
		Vector3 offset = (cameraPivot.forward * GunOffset.z) + (cameraPivot.right * GunOffset.x) + (cameraPivot.up * GunOffset.y);
		GunPivot.position = Vector3.Lerp(GunPivot.position, cameraPivot.position + offset, GunDamping * Time.deltaTime);
	}

	private void LookAt()
	{
		if (CameraRay(out RaycastHit hitInfo))
		{
			var rot = Quaternion.LookRotation(hitInfo.point - GunPivot.position, Vector3.up);
			GunPivot.rotation = Quaternion.Lerp(GunPivot.rotation, rot, GunDamping * Time.deltaTime);
		}
		else
		{
			GunPivot.rotation = Quaternion.Lerp(GunPivot.rotation, cameraPivot.rotation, GunDamping * Time.deltaTime);
		}
	}

	private bool CameraRay(out RaycastHit hit)
	{
		return Physics.Raycast(cameraPivot.position, cameraPivot.forward, out hit, 1000, GunReactMask);
	}

	public bool GunRay(out RaycastHit hit)
	{
		return Physics.Raycast(GunPivot.position, GunPivot.forward, out hit, 1000, GunReactMask);
	}
}
