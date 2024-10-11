using Cache;
using Enemy.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody))]
public class Voodoo : MonoCache, IDamageable, IEnemy
{
	
	[SerializeField] private float distanceToPlayer = 5;
	[field: SerializeField] public float Speed { get; set; }
	[field: SerializeField] public float MaxSpeed { get; set; }
	[field: SerializeField] public float AngularSpeed { get; set; }


	public Vector3 TargetPosition => player.transform.position;
	public EnemyAnimation Animation { get; set; }
	public Transform EnemyTransform => transform;


	protected StateMachine stateMachine;
	protected Health health;

	private Rigidbody rb;
	private Animator animator;

	private bool isTakingDamage = false;
	private bool isDead = false;

	protected PlayerMoovement player;

	[Inject]
	private void Construct(PlayerMoovement player)
	{
		this.player = player;
	}

	private void Awake()
	{
		animator = GetComponent<Animator>();
		Animation = new EnemyAnimation(animator);

		health = GetComponent<Health>();
		health.OnChangeHealth += TakeDamage;
		rb = GetComponent<Rigidbody>();

		stateMachine = new StateMachine();
		stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.none, this));
	}

	public override void OnTick()
	{
		if (isTakingDamage) return;
		if (isDead) return;

		if (Vector3.Distance(transform.position, TargetPosition) > distanceToPlayer)
		{
			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
		}
		else
		{
			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.idle, this));
		}
		stateMachine.CurrentState.Update();
	}

	#region Смерть
	private void Die()
	{
		if (!isDead)
			StartCoroutine(StartGetDead());
	}

	private IEnumerator StartGetDead()
	{
		isDead = true;
		stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.death, this));

		rb.isKinematic = true;
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}
	#endregion

	#region Нанесение урона
	private void TakeDamage(float damage)
	{
		if (!isTakingDamage)
		{
			if (health.GetCurrentHealth() > 0)
				StartCoroutine(StartTakeDamage(damage));
			else Die();
		}
	}

	private IEnumerator StartTakeDamage(float damage)
	{
		isTakingDamage = true;
		stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.damage, this));

		player.ApplyDamage(damage);

		yield return new WaitForSeconds(1.5f);
		isTakingDamage = false;
	}

	public void ApplyDamage(float damage)
	{
		health.TakeDamage(damage);
	}
	#endregion

	public Rigidbody GetRigidBody() => rb;
	private void OnDisable()
	{
		health.OnChangeHealth -= TakeDamage;
	}
}
