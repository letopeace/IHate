using UnityEngine;

public class Gun : MonoBehaviour
{
	public Transform Barrel;
	 
	private void Update()
	{
		
	}

	private void OnDrawGizmos()
	{
		if (Ray(out RaycastHit hitInfo))
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(Barrel.position, hitInfo.point);
		}
		else
		{
			Gizmos.color = Color.gray;
			Gizmos.DrawRay(Barrel.position, Barrel.forward * 1000f);
		}

	}

	private bool Ray(out RaycastHit hit)
	{
		return Physics.Raycast(Barrel.position, Barrel.forward, out hit);
	}
}

