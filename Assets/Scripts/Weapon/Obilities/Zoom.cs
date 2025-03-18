using Attack.Raycast;
using System;
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
		private ItemPickup item;
		private Camera mainCamera;
		private MouseLook playerLook;
		private bool isZoom = false;
		[Inject] private UIManager manager;
		[Inject] private PlayerMoovement player;

		private void Start()
		{
			mainCamera = Camera.main;

			playerLook = player.gameObject.GetComponentInChildren<MouseLook>();

			oldSensitivityVer = playerLook.sensitivityVer;
			oldSensitivityHor = playerLook.sensitivityHor;

			isZoom = false;
		}

		private void OnEnable()
		{
			weapon = GetComponent<Weapon>();
			weapon.OnZoomMouseClick += ZoomWeapon;
			item  = GetComponent<ItemPickup>();
			item.OnChangeIsPicked += CheckForRemoveZoom;
		}


		/// <summary>
		/// Поменять зум у оружия
		/// </summary>
		public void ZoomWeapon()
		{
			if (isZoom)
			{
				RemoveZoom();
			}
			else
			{
				mainCamera.fieldOfView = zoomValue;
				manager.ZoomIcon.enabled = true;
				manager.AimIcon.enabled = false;

				playerLook.sensitivityVer = sensitivityVer;
				playerLook.sensitivityHor = sensitivityHor;
			}

			isZoom = !isZoom;
		}

		/// <summary>
		/// Убрать зум при сброшенном оружии
		/// </summary>
		private void CheckForRemoveZoom()
		{
			if (item.IsPicked == true)
				return;

			RemoveZoom();
		}

		/// <summary>
		/// Убрать зум
		/// </summary>
		private void RemoveZoom()
		{
			mainCamera.fieldOfView = 60;
			manager.ZoomIcon.enabled = false;
			manager.AimIcon.enabled = true;
			playerLook.sensitivityVer = oldSensitivityVer;
			playerLook.sensitivityHor = oldSensitivityHor;
		}

		private void OnDisable()
		{
			weapon.OnZoomMouseClick -= ZoomWeapon;
			item.OnChangeIsPicked -= CheckForRemoveZoom;
		}
	}
}
