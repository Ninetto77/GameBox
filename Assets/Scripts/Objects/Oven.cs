using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oven : MonoBehaviour
{
    [SerializeField]private ItemInfo item;
    [SerializeField]private int countOfLogs;
    private int currentCountOfLogs;
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
                AddLogs();
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
    ///  ����� ������ �������� � ���������
    /// </summary>
    public void AddLogs()
    {
        if (currentCountOfLogs >= countOfLogs)
        {
            Debug.Log("����� ���������!");
            return;
        }


        bool isFound = inventory.FindItem(item);
        if (isFound)
        {
            Debug.Log($"����� ��������� {currentCountOfLogs}/{countOfLogs}");

            inventory.RemoveItem(item);
            currentCountOfLogs++;
        }
    }
}
