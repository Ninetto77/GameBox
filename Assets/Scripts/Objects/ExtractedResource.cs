using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class ExtractedResource : Interactable
{
    [Header("„асти добываемых ресурса")]
    public ItemInfo PartOfResource;

	[Header("Ёффект исчезновени€")]
    public Material DisolveMaterial;
	public GameObject RemainderPrefab;

	private Health health;


    private void Start()
    {
        health = GetComponentInChildren<Health>();
        health.OnDeadEvent += FallApart;
	}

    /// <summary>
    /// ћетод уничтожени€ добываемых ресурсов
    /// </summary>
    public void FallApart()
    {
        StartCoroutine(DisolveResource());
    }

    /// <summary>
    /// ћетод исчезновени€ предмета
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
    /// ћетод отн€ти€ здоровь€ у добываемых ресурсов
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
