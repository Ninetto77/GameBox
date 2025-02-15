using Cache;
using UnityEngine;
using Zenject;

public class Pumpkin : MonoCache
{
	[Header("Обнаружение игрока")]
	public LayerMask PlayerMask;
	[SerializeField] private float radiusOfDetect = 10;

	[Inject] protected PlayerMoovement player;

	[SerializeField] private float angularSpeed = 10;
	private Vector3 TargetPosition => player.transform.position;

	protected override void OnTick()
	{
		var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);
		
		foreach (var collider in colliders)
		{
			RotateToPlayer();
		}
	}

	/// <summary>
	/// поворот к игроку
	/// </summary>
	public void RotateToPlayer()
	{
		var direction = (TargetPosition - transform.position).normalized;
		direction.y = 0;
		var targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, angularSpeed * Time.deltaTime); ;
	}
}
