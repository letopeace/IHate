using UnityEngine;

public class RenderCamera : MonoBehaviour
{
	public bool LimitedFPS = false;

	[SerializeField] private Camera[] allCamera;
	[SerializeField] private AudioTaker audioTaker;
	[SerializeField] private float CameraFPS = 15;

	[SerializeField] private RenderTexture UV_Glitch_Objects, Color_Glitch_Objects, UV_Color_Glitch_Objects;
	//[SerializeField] private RenderTexture UV_Glitch_Depth, Color_Glitch_Depth, UV_Color_Glitch_Depth;
	[SerializeField] private RenderTexture UV_Glitch_Output, Color_Glitch_Output, UV_Color_Glitch_Output;
	[SerializeField] private Material UVGlitchMaterial, ColorGlitchMaterial;

	private float timer = 0;

	private RenderTexture UV_Color_Glitch_Objects_Temp;

	private void Start()
	{
		if (LimitedFPS)
		{
			foreach (var camera in allCamera)
			{
				camera.enabled = false;
				camera.depthTextureMode = DepthTextureMode.Depth;
			}
		}

		UV_Color_Glitch_Objects_Temp = new RenderTexture(UV_Color_Glitch_Objects);
	}

	//[ExecuteAlways]
	private void Update()
	{
		audioTaker.Variety3();

		if (LimitedFPS)
		{
			timer += Time.deltaTime;

			if (timer >= 1f / CameraFPS)
			{
				timer = 0f;

				Render();
			}
		}
		else
		{
			//Render();
		}
	}

	private void Render()
	{
		foreach (var camera in allCamera)
		{
			camera.Render();
		}

		audioTaker.Variety3();

		// UV
		//Graphics.Blit(UV_Glitch_Objects, UV_Glitch_Output, UVGlitchMaterial);

		// Color
		//Graphics.Blit(Color_Glitch_Objects, Color_Glitch_Output, ColorGlitchMaterial);

		// UV Color
		//Graphics.Blit(UV_Color_Glitch_Objects, UV_Color_Glitch_Objects_Temp, UVGlitchMaterial);
		//Graphics.Blit(UV_Color_Glitch_Objects_Temp, UV_Color_Glitch_Output, ColorGlitchMaterial);
	}
}
