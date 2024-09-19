using System;
using System.Collections;
using UnityEngine;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class EnemyController : MonoBehaviour, IEntity
	{
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		public GameObject Player;
		[SerializeField] private float radiusOfDetect = 10;
		[SerializeField] private float distanceToAtack = 3;

		[Header("Физика")]
		public float MaxSpeed = 10;
		public float Speed = 35;
		public float AngularSpeed = 10;

		private Animator animator;

		[HideInInspector]
		public EnemyAnimation Animation;
		private StateMachine stateMachine;
		private Health health;
		private Rigidbody rb;

		private bool started = true;
		private bool isTakingDamage = false;
		private bool isDead = false;

		private void Awake()
		{
			animator = GetComponent<Animator>();
			Animation = new EnemyAnimation(animator);
			stateMachine = new StateMachine();
			health = GetComponent<Health>();
			rb = GetComponent<Rigidbody>();
			health.OnChangeHealth += TakeDamage;
			health.OnDeadEvent += Die;
		}



		private void Start()
		{
			StartCoroutine(StartScreaming());
		}

		private IEnumerator StartScreaming()
		{
			started = true;
			stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.scream, this));

			yield return new WaitForSeconds(3f);

			started = false;
		}

		private void Update()
		{
			if (started) 
			{
				stateMachine.CurrentState.Update();
				return;
			}

			if (isTakingDamage) return;
			if (isDead) return;

			if (Vector3.Distance(transform.position, Player.transform.position) < distanceToAtack)
			{
				stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.attack, this));
				return;
			}

			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
			stateMachine.CurrentState.Update();


			//Debug.Log(1);

			var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

			foreach (var collider in colliders)
			{
				Debug.Log("Find Player");
			}
			//Collider[] colliders = null;
			//var v = Physics.OverlapSphereNonAlloc(transform.position, radiusOfDetect, colliders, PlayerMask.value);

			//foreach (var collider in colliders)
			//{
			//	Debug.Log("Find Player");
			//}

		}

		public Rigidbody GetRigidBody() => rb;

		#region Смерть
		private void Die()
		{
			StartCoroutine(StartGetDead());
		}

		private IEnumerator StartGetDead()
		{
			isDead = true;
			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.death, this));

			yield return new WaitForSeconds(5f);
			Destroy(this.gameObject);
		}
		#endregion

		#region Нанесение урона
		private void TakeDamage(float obj)
		{
			StartCoroutine(StartTakeDamage());
		}

		private IEnumerator StartTakeDamage()
		{
			isTakingDamage = true;
			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.damage, this));

			yield return new WaitForSeconds(1.5f);
			isTakingDamage = false;

		}

		public void ApplyDamage(float damage)
		{
			health.TakeDamage(damage);
		}
		#endregion

	}
}
