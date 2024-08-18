using UnityEngine;
using Zenject;

public class InventoryInstaller : MonoInstaller
{
    [SerializeField] private Inventory inventory;
    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(inventory).AsSingle().NonLazy();
    }
}