using Attack.Raycast;
using UnityEngine;
using Zenject;

public class Aim : MonoBehaviour
{
    [Inject] private UIManager manager;
	private Weapon weapon;

	private void Start()
	{
		manager.AimIcon.sprite = weapon.weapon.AimIcon;
	}
}
