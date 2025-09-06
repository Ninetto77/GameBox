using Attack.Overlap;
using Enemy.Abilities;
using ObjectPoolZenject;
using Points;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Enemy.States
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(Health))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent (typeof(AudioSource))]
	public abstract class EnemyController : MyPoolObject, IDamageable, IEnemy, IAttack, IWeaponVisitor
	{
		//public delegate void AccountHandler();
		//public event AccountHandler? OnEnemyDeath
		//{
		//	add { Debug.Log("add to OnEnemyDeath"); }
		//	remove { Debug.Log("delete OnEnemyDeath"); }
		//}
		public Action OnEnemyDeath;
		public string UniqueId { get; private set; }

		//public Action<EnemyController> OnEnemyDeath;
		public EnemyType Type;
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		[SerializeField] private float radiusOfDetect = 10f;
		[SerializeField] private float radiusOfDetectAfterDamage = 45f;
		[SerializeField] private float distanceToAtack = 3f;

		[Header("Урон по игроку (не ведьма)")]
		[Tooltip("Урон ведьмы менять в фаербол")]
		public float Damage = 10;
		[Header("Смерть")]
		[SerializeField] private float timeOfDeath = 3;
		[Header("Очки от смерти")]
		public int Points;

		[field: SerializeField] public float MaxSpeed  {get; set;}
		[field: SerializeField] public float Speed { get; set; }
		[field: SerializeField] public float AngularSpeed  {get; set;}

		[Header("Звуки")]
		[SerializeField] private AudioClip triggedPlayerSound;
		[SerializeField] protected AudioClip attackSound;
		[SerializeField] private AudioClip deathSound;

		[Header("Отображение HP")]
		[SerializeField] private Canvas hpCanvas;
		[SerializeField] private Slider hpSlider;

		private Animator animator;
		protected AudioSource audioSource;

		[HideInInspector]
		public EnemyAnimation AnimationEnemy { get; set; }
		private Rigidbody rb;
		private Camera camera;
		private DisappearAbility disappear;
		private AppearAbility appear;
		protected StateMachine stateMachine;
		protected Health health;

		[Inject] protected PlayerMoovement player;
		[Inject] private PointsLevel shop;

		public Vector3 TargetPosition => player.transform.position;
		public Transform EnemyTransform => transform;

		protected bool canMove;

		private bool isTakingDamage = false;
		private bool hasFirstDamage = false;
		private bool isDead = false;
		private bool noticePlayer = false;

		private float curRadiusOfDetect;


		[Inject] private SimpolZombiPool _zombiSimpolPool;
		[Inject] private HardZombiPool _zombiHardPool;
		[Inject] private SpiderPool _spiderPool;

		[Inject] private SkeletonPool _skeletonPool;
		[Inject] private MainWitchPool _mainWitchPool;
		[Inject] private WitchPool _witchPool;
		[Inject] private ManKillerPool _manKillerPool;


		[Inject]
		private void Construct(PlayerMoovement player)
		{
			this.player = player;
			UniqueId = Guid.NewGuid().ToString();

			//animator = GetComponent<Animator>();
			//AnimationEnemy = new EnemyAnimation(animator);

			//rb = GetComponent<Rigidbody>();

			//stateMachine = new StateMachine();
			//stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.none, this));

			//disappear = GetComponent<DisappearAbility>();
			//appear = GetComponent<AppearAbility>();

			//health = GetComponent<Health>();
			//health.OnChangeHealth += TakeDamage;
			//Debug.Log("health in Construct");

			//audioSource = GetComponent<AudioSource>();

			//player.OnPlayerDead += OnPlayerDead;
			//player.OnPlayerWin += OnPlayerWin;
			//canMove = true;


			//hasFirstDamage = false;
			//isTakingDamage = false;
			//isDead = false;
			//noticePlayer = false;
			//SetHPCanvas();
		}

		/// <summary>
		/// Кэширование компонентов
		/// </summary>
		private void CacheComponents()
		{
			animator = GetComponent<Animator>();
			AnimationEnemy = new EnemyAnimation(animator);

			rb = GetComponent<Rigidbody>();
			health = GetComponent<Health>();
			audioSource = GetComponent<AudioSource>();

			stateMachine = new StateMachine();

			disappear = GetComponent<DisappearAbility>();
			appear = GetComponent<AppearAbility>();
		}

		/// <summary>
		/// инициализировать значения
		/// </summary>
		private void InitValues()
		{
			curRadiusOfDetect = radiusOfDetect;
			SetHPCanvas();
		}

		//при создании 
		public override void OnCreate()
		{
			CacheComponents();
			InitValues();
		}

		//восстановление всех настроек
		public override void OnSpawned()
		{
			stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.idle, this));
			health.RestoreHealth();

			player.OnPlayerDead += OnPlayerDead;
			player.OnPlayerWin += OnPlayerWin;
			health.OnChangeHealth += TakeDamage;
			health.OnRestoreHealth += ChangeHPSliderValue;

			canMove = true;
			hasFirstDamage = false;
			isTakingDamage = false;
			isDead = false;
			noticePlayer = false;

			curRadiusOfDetect = radiusOfDetect;

			if (rb != null)
				rb.isKinematic = false;
			else
			{
				rb = GetComponent<Rigidbody>();
				rb.isKinematic = false;
			}

			SetDissapeareState(false);

			//Debug.Log($"Spawn {gameObject.name }!");
		}

		//при деспавне
		public override void OnDespawned()
		{
			health.OnChangeHealth -= TakeDamage;
			player.OnPlayerDead -= OnPlayerDead;
			player.OnPlayerWin -= OnPlayerWin;
			health.OnRestoreHealth -= ChangeHPSliderValue;

			//if (OnEnemyDeath != null)
			//{

			//}
		}


		protected virtual void Update()
		{
			if (!canMove) return;
			rb.AddForce(0, -20f, 0f, ForceMode.Acceleration); //гравитация вниз
			var colliders = Physics.OverlapSphere(transform.position, curRadiusOfDetect, PlayerMask.value);

			if (isTakingDamage) return;
			if (isDead) return;

			foreach (var collider in colliders)
			{
				StartCoroutine(NoticePlayer());
				if (Vector3.Distance(transform.position, TargetPosition) < distanceToAtack)
				{
					stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.attack, this));
					return;
				}

				stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.run, this));
			}


			if (colliders.Count() == 0 && (stateMachine.CurrentState is RunState) | (stateMachine.CurrentState is AttackState))
			{
				stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.idle, this));
			}

			stateMachine.CurrentState.Update();
		}

		/// <summary>
		/// Воспроизвести звук обнаружение игрока
		/// </summary>
		/// <returns></returns>
		private IEnumerator NoticePlayer()
		{
			if (stateMachine.CurrentState is IdleState)
			{
				if (noticePlayer == false)
				{
					noticePlayer = true;
					if (triggedPlayerSound != null)
						audioSource.PlayOneShot(triggedPlayerSound);

					yield return new WaitForSeconds(10f);
					noticePlayer = false;
				}
			}
		}

		private void LateUpdate()
		{
			RotateHPCanvas();
		}
		
		#region Канвас со здоровьем
		private void SetHPCanvas()
		{
			camera = Camera.main;

			if (hpCanvas)
			{
				hpCanvas.enabled = false;
				hpCanvas.worldCamera = camera;
			}

			if (hpSlider)
			{
				hpSlider.minValue = 0;
				hpSlider.maxValue = health.MaxHealth;
				hpSlider.value = health.MaxHealth;
			}
		}
		private void RotateHPCanvas()
		{
			if (hpCanvas && hpCanvas.worldCamera != null)
			{
				if (hpCanvas.enabled)
				{
					Quaternion vec = camera.transform.rotation;
					hpCanvas.transform.rotation = vec;
				}
			}
		}

		private void ChangeHPSliderValue(float health)
		{
			if (isDead) return;

			hpCanvas.enabled = true;
			hpSlider.value = health;
		}
		#endregion

		#region Смерть
		private void Die()
		{
			if (!isDead)
				StartCoroutine(StartGetDead());
		}

		private IEnumerator StartGetDead()
		{
			isDead = true;
			
			OnEnemyDeath?.Invoke();

			shop.AddPoints(new Point(Points));

			if (deathSound != null)
				audioSource.PlayOneShot(deathSound);

			SetDissapeareState(true);

			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.death, this));

			if (hpCanvas != null)
				hpCanvas.enabled = false;

			rb.isKinematic = true;
			yield return new WaitForSeconds(timeOfDeath);

			switch (Type)
			{
				case EnemyType.simpolZombi:
					_zombiSimpolPool.ReturnPooledObject(this);
					break;
				case EnemyType.hardZombi:
					_zombiHardPool.ReturnPooledObject(this);
					break;
				case EnemyType.skeleton:
					_skeletonPool.ReturnPooledObject(this);
					break;
				case EnemyType.manKiller:
					_manKillerPool.ReturnPooledObject(this);
					break;
				case EnemyType.witch:
					_witchPool.ReturnPooledObject(this);
					break;
				case EnemyType.mainWitch:
					_mainWitchPool.ReturnPooledObject(this);
					break;
				case EnemyType.spider:
					_spiderPool.ReturnPooledObject(this);
					break;
				default:
					break;
			}
			//Debug.Log("Despaw " + gameObject.name);
		}
		#endregion

		#region Нанесение урона
		private void TakeDamage(float value)
		{
			//если это был первый урон
			if (!hasFirstDamage)
			{
				hasFirstDamage = true;
				curRadiusOfDetect = radiusOfDetectAfterDamage;
			}

			ChangeHPSliderValue(value);

			if (health.GetCurrentHealth() > 0)
			{
				StartCoroutine(StartTakeDamage());
			}

			else if (health.GetCurrentHealth() <= 0) Die();
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
			if (isDead) return;

			if (health != null)
				health.TakeDamage(damage);
		}
		#endregion

		#region некоторые методы
		public Rigidbody GetRigidBody() => rb;

		private void OnPlayerDead() => canMove = false;
		private void OnPlayerWin() => canMove = false;
		#endregion

		/// <summary>
		/// Изменить прозрачность
		/// </summary>
		/// <param name="state">Сделать ли исчезновение объекта</param>
		public void SetDissapeareState(bool state)
		{
			if (state) 
			{
				if (disappear)
				{
					disappear.Execute(); 
				}
			}
			else
			{
				if (appear)
				{
					appear.Execute(); 
				}
			}
		}

		#region паттерн Visitor
		public virtual void Visit(MelleWeapon weapon)
		{
			DefaultOverlapVisit(weapon);
		}

		public virtual void DefaultOverlapVisit(MelleWeapon weapon)
		{
			weapon.onHit();
		}
		#endregion
	}
}
