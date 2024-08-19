using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //�������� ��������. ���������� ��� ��������� ���������
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<ItemInfo> items = new List<ItemInfo>();
    public int Space = 3;
    public float reachDistance = 1.5f;

    private OutlineObjects lastOutline;
    private Camera mainCamera;


    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    private void Update()
    {
        GatherResource();
    }

    /// <summary>
    /// ����� ��������� ��������� � ���������
    /// </summary>
    public void GatherResource()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (lastOutline != null) 
            lastOutline.enabled = false;

        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            ItemPickup item = hit.collider.gameObject.GetComponent<ItemPickup>();

            if (item != null)
            {
                lastOutline = item.outline;
                item.outline.enabled = true ;
                if (Input.GetMouseButtonDown(0))
                {
                    item.Interact();
                }
            }
            else if(lastOutline !=null)
            {
                lastOutline.enabled = false;
                lastOutline = null;
            }
        }
    }


    /// <summary>
    /// ���� ������� ���� � ���������, ���������� 1. ����� 0
    /// </summary>
    /// <param name="newItem">�������</param>
    /// <returns></returns>
    public bool FindItem(ItemInfo newItem)
    {
        bool isExist = items.Find(newItem => newItem);
        return isExist;

    }

    /// <summary>
    /// ���� ������� �������� � ���������, ���������� 1. ����� 0
    /// </summary>
    /// <param name="newItem">�������</param>
    /// <returns></returns>
    public bool TryAddItem(ItemInfo newItem)
    {  
        if (items.Count >= Space)
        {
            return false;
        }
        items.Add(newItem);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
        return true;
    }
    /// <summary>
    /// ������� ���������� ������������� �������� � ���������
    /// </summary>
    /// <returns></returns>
    public int GetCountOfItem(string nameItem)
    {
        return items.Count(item => item.name == nameItem);
    }

    /// <summary>
    /// ������� ������� �� ���������
    /// </summary>
    /// <param name="newItem">�������</param>
    public void RemoveItem(ItemInfo newItem)
    {
        items.Remove(newItem);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
        
    }
}
