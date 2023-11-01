using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private ItemInfo item;
    [SerializeField] private int countOfItems;
    [SerializeField] private Transform PlaseOfItems;
    private int currentCountOfItems;
    private bool isInteractable;
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    private void Update()
    {
        if (isInteractable)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddItem();
            }
        }
    }
    /// <summary>
    /// ��������, ����� �� ������� �����
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            isInteractable = true;
        }
    }
    /// <summary>
    /// ����� ������ �������� � ���������
    /// </summary>
    public void AddItem()
    {
        if (currentCountOfItems >= countOfItems)
        {
            Debug.Log("������ ������ ������ ��������!");
            return;
        }


        bool isFound = inventory.FindItem(item);
        if (isFound)
        {
            Debug.Log($"��������� {currentCountOfItems}/{countOfItems}");

            inventory.RemoveItem(item);
            ShowItems();

            currentCountOfItems++;

        }
    }

    public void ShowItems()
    {
        if (PlaseOfItems.childCount > 0) //�������� �� ������� �����
        {
            PlaseOfItems.GetChild(currentCountOfItems).gameObject.SetActive(true);
        }
    }
}
