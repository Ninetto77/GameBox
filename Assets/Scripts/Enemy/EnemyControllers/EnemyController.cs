using Attack.Overlap;
using Enemy.Abilities;
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
	public abstract class EnemyController : MonoBehaviour, IDamageable, IEnemy, IAttack, IWeaponVisitor
	{
		public Action OnEnemyDeath;
		[Header("Обнаружение игрока")]
		public LayerMask PlayerMask;
		[SerializeField] private float radiusOfDetect = 10;
		[SerializeField] private float distanceToAtack = 3;

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

        [Inject]
		private void Construct(PlayerMoovement player)
        {
            this.player = player;

			animator = GetComponent<Animator>();
			AnimationEnemy = new EnemyAnimation(animator);

			rb = GetComponent<Rigidbody>();

			stateMachine = new StateMachine();
			stateMachine.Init(FactoryState.GetStateEnemy(StatesEnum.none, this));
			
			disappear = GetComponent<DisappearAbility>();
			appear = GetComponent<AppearAbility>();

			health = GetComponent<Health>();
			health.OnChangeHealth += TakeDamage;

			audioSource = GetComponent<AudioSource>();

			player.OnPlayerDead += OnPlayerDead;
			player.OnPlayerWin += OnPlayerWin;
			canMove = true;

			SetHPCanvas();
		}

		protected virtual void Update()
		{
			if (!canMove) return;
			rb.AddForce(0, -20f, 0f, ForceMode.Acceleration); //гравитация вниз
			var colliders = Physics.OverlapSphere(transform.position, radiusOfDetect, PlayerMask.value);

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

			if (disappear)
				disappear.Execute();

			stateMachine.ChangeState(FactoryState.GetStateEnemy(StatesEnum.death, this));

			if (hpCanvas != null)
				hpCanvas.enabled = false;

			rb.isKinematic = true;
			yield return new WaitForSeconds(timeOfDeath);
			Destroy(this.gameObject);
		}
		#endregion

		#region Нанесение урона
		private void TakeDamage(float value)
		{
			//если это был первый урон
			if (!hasFirstDamage)
			{
				hasFirstDamage = true;
				radiusOfDetect = 45f;
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
					disappear.Execute();
			}
			else
			{
				if (appear)
					appear.Execute();
			}
		}

		private void OnDisable()
		{
			health.OnChangeHealth -= TakeDamage;
			player.OnPlayerDead -= OnPlayerDead;
			player.OnPlayerWin -= OnPlayerWin;
		}

		public virtual void Visit(MelleWeapon weapon)
		{
			DefaultOverlapVisit(weapon);
		}

		public virtual void DefaultOverlapVisit(MelleWeapon weapon)
		{
			weapon.onHit();
		}
	}
}
