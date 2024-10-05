using Attack.Raycast;
using UnityEngine;
using Zenject;

public class Zoom: MonoBehaviour
{
    public float zoomValue = 60;
	private Weapon weapon;
	private Camera mainCamera;
	private bool isZoom = false;
	[Inject] private UIManager manager;

	private void Start()
	{
		mainCamera = Camera.main;
		isZoom = false;
	}

	private void OnEnable()
	{
		weapon = GetComponent<Weapon>();
		weapon.OnZoomMouseClick += ZoomWeapon;
	}

	private void OnDisable()
	{
		weapon.OnZoomMouseClick -= ZoomWeapon;
	}
	public void ZoomWeapon()
	{
		isZoom = !isZoom;
		if (isZoom)
		{
			mainCamera.fieldOfView = 60;
			manager.ZoomIcon.enabled = false;
			manager.AimIcon.enabled = true;
		}
		else
		{
			mainCamera.fieldOfView = zoomValue;
			manager.ZoomIcon.enabled = true;
			manager.AimIcon.enabled = false;
		}

	}
}
