using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ItemPickup))]
public class GatherResources : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    private Insrument insrument;
    private Camera mainCamera;
    private Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        ItemInfo temp = gameObject.GetComponent<ItemPickup>().item;
        insrument =(Insrument)temp;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GatherResource();
        }
    }

    /// <summary>
    /// Метод добычи ресурсов с помощью инструментов
    /// </summary>
    public void GatherResource()
    {
        //Ray ray = mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f, ~layerMask) )
        {
            ExtractedResource resource = hit.collider.gameObject.GetComponent<ExtractedResource>();
            if (resource != null)
            {
                inventory.TryAddItem(resource.partOfResource);
                resource.TakeDamage(insrument.Power);
            }
        }
    }
}
