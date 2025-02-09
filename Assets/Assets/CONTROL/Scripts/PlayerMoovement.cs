using Cache;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(DamagePlayer))]
public class PlayerMoovement : MonoCache
{
	public Action OnPlayerDead;
	public Action OnPlayerWin;

	public float speed = 5;

	[Header("Running")]
	public bool canRun = true;
	public bool IsRunning { get; private set; }
	public float runSpeed = 9;

	public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

	[Header("Подбор предметов")]
	public float reachDistance = 4f;

	[Header("Удар")]
	public Transform hand;

	[HideInInspector]
	public Health health;

	[SerializeField] private Rigidbody rb;
	[SerializeField] private DamagePlayer damagePlayer;	
	private PlayerBrain brain;

	private void OnValidate()
	{
		rb ??= GetComponent<Rigidbody>();
		damagePlayer ??= GetComponent<DamagePlayer>();
	}

	private void Start()
	{
		brain = new PlayerBrain(reachDistance);
		health = damagePlayer.health;
		OnPlayerDead += damagePlayer.OnPlayerDead;
	}

	protected override void OnTick()
	{
		brain.Update();
	}

	/// <summary>
	/// Движение игрока по двум направлениям с учетом гравитации
	/// </summary>
	/// <param name="horizontalDirection">горизонтальное направление</param>
	/// <param name="verticalDirection">вертикальное направление</param>
	public void MovePlayer(float horizontalDirection, float verticalDirection, bool run)
	{
		IsRunning = canRun && run;

		float targetMovingSpeed = IsRunning ? runSpeed : speed;
		if (speedOverrides.Count > 0)
		{
			targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
		}

		Vector2 targetVelocity = new Vector2(horizontalDirection * targetMovingSpeed, verticalDirection * targetMovingSpeed);
		rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
	}

	/// <summary>
	/// Нанести урон
	/// </summary>
	/// <param name="damage"></param>
	public void ApplyDamage(float damage)
	{
		damagePlayer.ApplyDamage(damage);
	}
}