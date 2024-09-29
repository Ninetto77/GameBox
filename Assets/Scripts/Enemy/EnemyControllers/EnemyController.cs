using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public class EnemyController : MonoBehaviour, IEntity, IEnemy
	{
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		[SerializeField] private float radiusOfDetect = 10;
		[SerializeField] private float distanceToAtack = 3;


		//[Header("Физика")]
		[field: SerializeField] public float MaxSpeed  {get; set;}
		[field: SerializeField] public float Speed { get; set; }
		[field: SerializeField] public float AngularSpeed  {get; set;}

		private Animator animator;

		[HideInInspector]
		public EnemyAnimation Animation { get; set; }
		private StateMachine stateMachine;
		private Health health;
		private Rigidbody rb;

		[Inject] private PlayerMoovement player;
		public Vector3 TargetPosition => player.transform.position;
		public Transform EnemyTransform => transform;


		private bool started = true;
		private bool isTakingDamage = false;
		private bool isDead = false;

        [Inject]
		private void Construct(PlayerMoovement player)
        {
            this.player = player;
        }

        private void Awake()
		{
			radiusOfDetect = 10;
			distanceToAtack = 3;

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

			if (Vector3.Distance(transform.position, TargetPosition) < distanceToAtack)
			{
				stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.attack, this));
				return;
			}

			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
			stateMachine.CurrentState.Update();


			//лучше 

			var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

			foreach (var collider in colliders)
			{
				Debug.Log("Find Player");
			}
			//лучше 

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

		private void OnDisable()
		{
			health.OnChangeHealth -= TakeDamage;
			health.OnDeadEvent -= Die;
		}

	}
}
