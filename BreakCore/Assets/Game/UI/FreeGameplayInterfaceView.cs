using UnityEngine;

public class FreeGameplayInterfaceView : MonoBehaviour
{
	public FreeGameplayInterfaceViewModel viewModel;
	public Transform CrossHair;

	private void Update()
	{
		CrossHairUpdate();
	}

	private void CrossHairUpdate()
	{
		Vector3 worldPos;
		bool res = viewModel.GetCrossHairPositionInTheWorld(out worldPos);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
		screenPos.z = 0;
		CrossHair.position = screenPos;
	}
}
