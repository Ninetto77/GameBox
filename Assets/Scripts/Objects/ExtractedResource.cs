using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ExtractedResource : Interactable
{
    [Header("����� ���������� �������")]
    public ItemInfo PartOfResource;

	[Header("������ ������������")]
    public Material DisolveMaterial;
	public GameObject RemainderPrefab;

	private Health health;


    private void Start()
    {
        health = GetComponentInChildren<Health>();
        health.OnDeadEvent += FallApart;
	}

    /// <summary>
    /// ����� ����������� ���������� ��������
    /// </summary>
    public void FallApart()
    {
        StartCoroutine(DisolveResource());
    }

    /// <summary>
    /// ����� ������������ ��������
    /// </summary>
    /// <returns></returns>
    IEnumerator DisolveResource()
    {
        var collids = GetComponents<BoxCollider>();
        foreach (var item in collids)
        {
            item.enabled = false;
		}

        var render = gameObject.GetComponentInChildren<Renderer>();
        if (DisolveMaterial!=null && render != null )
			render.material = DisolveMaterial;
            
        yield return new WaitForSeconds(2f);

        if (RemainderPrefab != null)
        {
            var remainder = Instantiate(RemainderPrefab, transform.position, Quaternion.identity);
        }
		Destroy(this.gameObject);
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
