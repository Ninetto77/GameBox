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
    /// Проверка, задел ли триггер игрок
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
    ///  Метод поиска предмета в инвентаре
    /// </summary>
    public void AddLogs()
    {
        if (currentCountOfLogs >= countOfLogs)
        {
            Debug.Log("Печка затоплена!");
            return;
        }


        bool isFound = inventory.FindItem(item);
        if (isFound)
        {
            Debug.Log($"Печка заполнена {currentCountOfLogs}/{countOfLogs}");

            inventory.RemoveItem(item);
            currentCountOfLogs++;
        }
    }
}
