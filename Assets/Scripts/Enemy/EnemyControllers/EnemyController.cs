using System.Collections;
using UnityEngine;
using Zenject;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	public abstract class EnemyController : MonoBehaviour, IDamageable, IEnemy, IAttack
	{
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		[SerializeField] private float radiusOfDetect = 10;
		[SerializeField] private float distanceToAtack = 3;

		[Header("Урон (не ведьма)")]
		[Tooltip("Урон ведьмы менять в фаербол")]
		public float Damage = 10;

		//[Header("Физика")]
		[field: SerializeField] public float MaxSpeed  {get; set;}
		[field: SerializeField] public float Speed { get; set; }
		[field: SerializeField] public float AngularSpeed  {get; set;}

		private Animator animator;

		[HideInInspector]
		public EnemyAnimation Animation { get; set; }
		private Rigidbody rb;
		protected StateMachine stateMachine;
		protected Health health;

		[Inject] protected PlayerMoovement player;
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
			animator = GetComponent<Animator>();
			Animation = new EnemyAnimation(animator);

			stateMachine = new StateMachine();
			stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.none, this));

			health = GetComponent<Health>();
			rb = GetComponent<Rigidbody>();
			health.OnChangeHealth += TakeDamage;
			health.OnDeadEvent += Die;
		}

		private void Start()
		{
			//StartCoroutine(StartScreaming());
		}

		/// <summary>
		/// Вой врага
		/// </summary>
		/// <returns></returns>
		private IEnumerator StartScreaming()
		{
			started = true;
			stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.scream, this));

			yield return new WaitForSeconds(3f);

			started = false;
		}

		protected virtual void Update()
		{
			var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

			foreach (var collider in colliders)
			{
				if (isTakingDamage) return;
				if (isDead) return;

				if (Vector3.Distance(transform.position, TargetPosition) < distanceToAtack)
				{
					stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.attack, this));
					return;
				}

				stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
				stateMachine.CurrentState.Update();
			}

			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.idle, this));
			stateMachine.CurrentState.Update();
			
			//if (started) 
			//{
			//	stateMachine.CurrentState.Update();
			//	return;
			//}

			//if (isTakingDamage) return;
			//if (isDead) return;

			//if (Vector3.Distance(transform.position, TargetPosition) < distanceToAtack)
			//{
			//	stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.attack, this));
			//	return;
			//}

			//stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
			//stateMachine.CurrentState.Update();


			////лучше 

			//var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

			//foreach (var collider in colliders)
			//{
			//	Debug.Log("Find Player");
			//}

		}

		public Rigidbody GetRigidBody() => rb;

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

			yield return new WaitForSeconds(5f);
			Destroy(this.gameObject);
		}
		#endregion

		#region Нанесение урона
		private void TakeDamage(float obj)
		{
			if (!isTakingDamage)
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
