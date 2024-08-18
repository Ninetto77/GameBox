using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerMoovement player;

	public override void InstallBindings()
	{

		Container.
			Bind<PlayerMoovement>().
			FromInstance(player).
			AsSingle().
			NonLazy(); //регистрируем игрока
	}

	///// <summary>
	///// Если захочу заспавнить игрока
	///// </summary>
	//public override void InstallBindings()
 //   {
 //       var playerInstance =
 //           Container.InstantiatePrefabForComponent<PlayerMoovement>(
 //               player, playerSpawPoint.position, Quaternion.identity, null);

 //       Container.
 //           Bind<PlayerMoovement>().
 //           FromInstance(playerInstance).
 //           AsSingle().
 //           NonLazy(); //регистрируем игрока
 //   }
}