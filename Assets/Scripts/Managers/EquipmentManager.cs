using InventorySystem;
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


        //if (currentEquipment[slotIndex] != null)
        //{
        //    //найти старый предмет
        //    oldItem = currentEquipment[slotIndex];
        //    //добавить старый предмет в инвентарь
        //    inventory.TryAddItem(inventoryname, oldItem.Name);
        //}

        //экипировать новый предмет
        //currentEquipment[slotIndex] = newItem;
        //удалить новый предмет из инвентаря
        //inventory.RemoveItem(inventoryname, newItem.Name, 1);

        //����� �������
        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        if (newItem.EquipmentSlot == 0 || newItem.EquipmentSlot == EquipmentsSlot.handweapon)
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

        //если слот экипировки не пустой
        //if (currentEquipment[slotIndex] != null)
        //{
        //    Tool oldItem = currentEquipment[slotIndex];

        //    //bool IsAdded = inventory.TryAddItem(inventoryname, oldItem.Name);
        //    //if (IsAdded)
        //    //{
        //    //    currentEquipment[slotIndex] = null;
        //    //}

        //    UnequipHand();

        //    ///вызов события
        //    if (OnEquipmentChangedCallback != null)
        //    {
        //        OnEquipmentChangedCallback.Invoke(null, oldItem);
        //    }
        //}


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

		//GameObject item = Instantiate(newItem.Prefab, playerHand) as GameObject;
        item.transform.position = playerHand.position;
        item.GetComponent<ItemPickup>().isPicked = true;
        Debug.Log($"Get {item.name}");
    }

    /// <summary>
    /// Метод снятия экипировки с руки
    /// </summary>
    public void UnequipHand()
    {
        if (playerHand.childCount > 0)
        {
            Destroy(playerHand.GetChild(0).gameObject);
        }
    }


}
