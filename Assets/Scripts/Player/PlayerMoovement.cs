using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
    public float Speed = 20f;
    public float Gravity = -9.8f;

    public Vector3 direction;

    private CharacterController player;
    private Animator animator;
    private PlayerAnimations animations;


	void Start()
    {
        player = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
		animations =  new PlayerAnimations(animator);
    }

    /// <summary>
    /// Движение игрока по двум направлениям с учетом гравитации
    /// </summary>
    /// <param name="horizontalDirection">горизонтальное направление</param>
    /// <param name="verticalDirection">вертикальное направление</param>
    public void MovePlayer(float horizontalDirection, float verticalDirection)
    {
        direction = new Vector3(horizontalDirection * Speed, 0, verticalDirection * Speed);
        //ограничение скорости
        direction = Vector3.ClampMagnitude(direction, Speed);

        //гравитация
        direction.y = -Gravity;

        direction *= Time.deltaTime;
        direction = transform.TransformDirection(direction);
        //движение
        player.Move(direction);
        animations.SetRuningAnim(player.velocity);
	}

     

    //private void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;
    //}
}
