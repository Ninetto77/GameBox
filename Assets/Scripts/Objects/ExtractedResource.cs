using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]

public class ExtractedResource : Interactable
{

    [Header("����� ���������� �������")]
    public ItemInfo partOfResource;

    private Health health;


    private void Start()
    {
        health = GetComponent<Health>();
    }
    void Update()
    {
        FallApart();
    }

    /// <summary>
    /// ����� ����������� ���������� ��������
    /// </summary>
    public void FallApart()
    {
        if (health.GetCurrentHealth() <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// ����� ������� �������� � ���������� ��������
    /// </summary>
    public void TakeDamage(float damage)
    {
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log("Health of " + this.gameObject.name + " = " + health.GetCurrentHealth());
        }
    }

}
