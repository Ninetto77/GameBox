using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Inventory : MonoBehaviour
{
    #region Singlton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
            return;
        
        instance = this;
    }
    #endregion

    //�������� ��������. ���������� ��� ��������� ���������
    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    public List<ItemInfo> items = new List<ItemInfo>();
    public int Space = 3;
    public float reachDistance = 1.5f;


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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, reachDistance))
            {
                ItemPickup item = hit.collider.gameObject.GetComponent<ItemPickup>();
                if (item != null)
                {
                    item.Interact();
                }
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
