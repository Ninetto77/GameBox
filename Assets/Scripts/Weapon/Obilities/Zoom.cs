using Attack.Raycast;
using UnityEngine;
using Zenject;

namespace WeaponObilities
{
	public class Zoom : MonoBehaviour
	{
		public float zoomValue = 60;
		[Header("Zoom Sensitivity")]
		public float sensitivityHor = 0.5f;
		public float sensitivityVer = 0.5f;

		private float oldSensitivityHor;
		private float oldSensitivityVer;

		private Weapon weapon;
		private Camera mainCamera;
		private MouseLook look;
		private MouseLook playerLook;
		private bool isZoom = false;
		[Inject] private UIManager manager;
		[Inject] private PlayerMoovement player;

		private void Start()
		{
			mainCamera = Camera.main;

			look = mainCamera.gameObject.GetComponent<MouseLook>();
			playerLook = player.gameObject.GetComponent<MouseLook>();

			oldSensitivityVer = look.sensitivityVer;
			oldSensitivityHor = playerLook.sensitivityHor;

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
			if (isZoom)
			{
				mainCamera.fieldOfView = 60;
				manager.ZoomIcon.enabled = false;
				manager.AimIcon.enabled = true;
				look.sensitivityVer = oldSensitivityVer;
				playerLook.sensitivityHor = oldSensitivityHor;
			}
			else
			{
				mainCamera.fieldOfView = zoomValue;
				manager.ZoomIcon.enabled = true;
				manager.AimIcon.enabled = false;

				look.sensitivityVer = sensitivityVer;
				playerLook.sensitivityHor = sensitivityHor;
			}

			isZoom = !isZoom;
		}
	}
}
