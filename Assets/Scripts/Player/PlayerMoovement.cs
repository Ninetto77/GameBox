using Cache;
using Sounds;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(InputKeys))]
[RequireComponent (typeof(MouseLook))]
public class PlayerMoovement : MonoCache, IDamageable
{
    [Header("Физика")]
    public float Gravity = -9.8f;
    public float JumpForce = 5f;
    [Header("Ходьба")]
    public float Speed = 40f;
    public float MaxSpeed = 20f;

	[Header("Бег")]
	public float RunSpeed = 20f;
	public float RunMaxSpeed = 10f;

	[Header("Удар")]
	//[SerializeField] private Transform aimTarget;
	public Transform hand;

	[Header("Подбор предметов")]
	public float reachDistance = 4f;

	[HideInInspector]
    public Vector3 direction;

	public Action OnPlayerDead;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;
    private PlayerBrain brain;
	private Rigidbody rb;
	[Inject]
	private AudioManager audioManager;
	[Inject]
	private UIManager uiManager;

	[HideInInspector]
	public Health health;

	private const string runName = GlobalStringsVars.RUN_SOUND_NAME;
	private const string walkName = GlobalStringsVars.WALK_SOUND_NAME;

	private const string deadName = GlobalStringsVars.DEATH_SOUND_NAME;
	private const string musicName = GlobalStringsVars.MAIN_MUSIC_NAME;

    private bool isJumping;
	private float curSpeed;
	private float curMaxSpeed;
	private bool isGoing = false;
	private string curSound = walkName;

	private void Awake()
	{
        health = GetComponent<Health>();
	}
	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
        brain = new PlayerBrain(reachDistance);
        rb = GetComponent<Rigidbody>();
	}
	protected override void OnTick()
    {
		if (Input.GetKeyDown(KeyCode.P))
		{
			ApplyDamage(99f);
		}
		TurnHead();
		brain.Update();
	}

	/// <summary>
	/// Движение игрока по двум направлениям с учетом гравитации
	/// </summary>
	/// <param name="horizontalDirection">горизонтальное направление</param>
	/// <param name="verticalDirection">вертикальное направление</param>
	public void MovePlayer(float horizontalDirection, float verticalDirection, bool isRuning)
    {
		direction = new Vector3(horizontalDirection, 0, verticalDirection);
		
		if (isRuning)
		{
			curSpeed = RunSpeed;
			curMaxSpeed = RunMaxSpeed;
			curSound = runName;
		}
		else
		{
			curSpeed = Speed;
			curMaxSpeed = MaxSpeed;
			curSound = walkName;
		}

		rb.AddRelativeForce(direction * curSpeed/*, ForceMode.Acceleration*/);

		if (rb.velocity.magnitude > curMaxSpeed)
			rb.velocity = Vector3.ClampMagnitude(rb.velocity, curMaxSpeed);

		PlayRunSound();
		
		if (animator != null)
			animations.SetRuningAnim(rb.velocity);
	}

	
	private void PlayRunSound()
	{
		if (isJumping) return;

		if (rb.velocity.magnitude >= 0.01f)
		{
			if (!isGoing)
			{
				isGoing = true;
				audioManager.PlaySound(curSound);
			}
		}
		else
		{
			if (isGoing)
			{
				isGoing = false;
				audioManager.StopSound(curSound);
			}
		}
	}

    private void TurnHead()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
        Vector3 desirePosition = ray.origin + ray.direction * 0.7f;
       // aimTarget.position = desirePosition;
	}

	public void ApplyDamage(float damage) {
		health.TakeDamage(damage);
		
		if (health.GetCurrentHealth() <=0)
		{
			StartCoroutine(DeadPlayer());
		}
		else 
			uiManager.OnPlayerDamage?.Invoke();
	}

	private IEnumerator DeadPlayer()
	{
		yield return new WaitForEndOfFrame();
		GetComponent<InputKeys>().enabled = false;
		GetComponent<MouseLook>().enabled = false;
		
		audioManager.StopSound(musicName);
		audioManager.StopSound(runName);
		audioManager.PlaySound(deadName);

		OnPlayerDead?.Invoke();
		uiManager.OnPlayerDead?.Invoke();
	}

	public void Hit(bool state)
	{
		if (animator != null)
			animations.SetHitAnim(state);
	}

	//private void Update()
	//{
	//    if (EventSystem.current.IsPointerOverGameObject())
	//        return;
	//}
}
