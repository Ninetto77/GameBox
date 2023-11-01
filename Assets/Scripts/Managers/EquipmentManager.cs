using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singlton
    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }
    #endregion

    [SerializeField]private Transform playerHand;

    private Inventory inventory;
    private Insrument[] currentEquipment;

    public delegate void OnEquipmentChanged(Insrument oldItem, Insrument newItem);
    public OnEquipmentChanged OnEquipmentChangedCallback;


    private void Start()
    {
        inventory = Inventory.instance;

        //найти количество экипированных частей тела
        int numberOfSlots = System.Enum.GetNames(typeof(EquipmentsSlot)).Length;
        currentEquipment = new Insrument[numberOfSlots];
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
    public void Equip(Insrument newItem)
    {
        int slotIndex = (int)newItem.EquipmentSlot;
        Insrument oldItem = null;


        if (currentEquipment[slotIndex] != null)
        {
            //найти старый предмет
            oldItem = currentEquipment[slotIndex];
            //добавить старый предмет в инвентарь
            inventory.TryAddItem(oldItem);
        }

        //экипировать новый предмет
        currentEquipment[slotIndex] = newItem;
        //удалить новый предмет из инвентаря
        inventory.RemoveItem(newItem);

        //����� �������
        if (OnEquipmentChangedCallback != null)
        {
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
        }

        if (newItem.EquipmentSlot == 0)
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
        //если слот экипировки не пустой
        if (currentEquipment[slotIndex] != null)
        {
            Insrument oldItem = currentEquipment[slotIndex];
            bool IsAdded = inventory.TryAddItem(oldItem);

            if (IsAdded)
            {
                currentEquipment[slotIndex] = null;
            }

            UnequipHand();

            ///вызов события
            if (OnEquipmentChangedCallback != null)
            {
                OnEquipmentChangedCallback.Invoke(null, oldItem);
            }
        }


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
    public void EquipHand(Insrument newItem)
    {
        UnequipHand();

        GameObject item = Instantiate(newItem.Prefab, playerHand) as GameObject;
        item.transform.position = playerHand.position;
        item.GetComponent<ItemPickup>().isPicked = true;
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
