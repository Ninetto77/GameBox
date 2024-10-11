using InventorySystem;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class EquipmentManager : MonoBehaviour
{
    private Transform playerHand => player.hand;

	[Inject] private DiContainer diContainer;
    [Inject] private PlayerMoovement player;

	private Equipable[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipable oldItem, Equipable newItem);
    public OnEquipmentChanged OnEquipmentChangedCallback;

	private InventoryController inventory;

	private void Start()
    {
        //найти количество экипированных частей тела
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentsSlot)).Length;
        currentEquipment = new Equipable[numberOfSlots];
		inventory = InventoryController.instance;
	}

	private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) 
        {
            UnequiptAll();
        }
    }

    /// <summary>
    /// Экипировать предмет
    /// </summary>
    /// <param name="newItem"></param>
    public void Equip(Equipable newItem)
    {
        int slotIndex = (int)newItem.EquipmentSlot;
		Equipable oldItem = null;

        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        if (newItem.EquipmentSlot == 0 || newItem.EquipmentSlot == EquipmentsSlot.hand)
		{
            EquipHand(newItem);
        }
    }
    /// <summary>
    /// Снять старый предмет
    /// </summary>
    /// <param name="slotIndex"></param>
    public void Unequipt(int slotIndex)
    {
        UnequipHand();
    }
	/// <summary>
	/// Снять всю экипировку
	/// </summary>
	public void UnequiptAll()
    {
        for (int i=0; i < currentEquipment.Length; i++)
        {
            Unequipt(i);
        }
    }

    /// <summary>
    /// Метод экипировки руки
    /// </summary>
    /// <param name="newItem"></param>
    public void EquipHand(Equipable newItem)
    {
        UnequipHand();

		GameObject item =  diContainer.InstantiatePrefab(newItem.Prefab, playerHand);

        item.transform.position = playerHand.position;
        item.GetComponent<ItemPickup>().IsPicked = true;
		item.GetComponent<Rigidbody>().isKinematic = true;

		Debug.Log($"Get {item.name}");
    }

    /// <summary>
    /// Метод снятия экипировки с руки
    /// </summary>
    public void UnequipHand()
    {
        if (playerHand.childCount > 0)
        {
            Destroy(GetPlayerHand());
            //playerHand.GetChild(0).gameObject.transform.parent = null;
        }
    }

    /// <summary>
    /// Бросить в мир предмет
    /// </summary>
	public void DropHand()
	{
		if (playerHand.childCount > 0)
		{
			GetPlayerHand().transform.parent = null;
		}
	}

    public GameObject GetPlayerHand()
    {
        if (playerHand.childCount > 0)
        {
            return playerHand.GetChild(0).gameObject;
        }
        return null;
    }
}
