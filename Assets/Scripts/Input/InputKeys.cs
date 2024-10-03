using UnityEngine;

public class InputKeys : MonoBehaviour
{
    [HideInInspector]
    public PlayerMoovement player;

    public float horizontalDirection;
	public float verticalDirection;

	public bool jump ;


	private void Start()
    {
        player = GetComponent<PlayerMoovement>();
    }

    /// <summary>
    /// Принимает клавиши клавиатуры и отправляет их к скрипту движения игрока
    /// </summary>
    private void Update()
    {
        horizontalDirection = Input.GetAxis(GlobalStringsVars.HORIZONTAL_AXIS);
        verticalDirection = Input.GetAxis(GlobalStringsVars.VERTICAL_AXIS);

        bool hit = Input.GetMouseButton(GlobalStringsVars.FIRE);
        if (hit) player.Hit(true);

		bool notHit = Input.GetMouseButtonUp(GlobalStringsVars.FIRE);
		if (notHit) player.Hit(false);

        jump = Input.GetKeyDown(KeyCode.Space);
           // if (jump) Debug.Log("jump");
        player.MovePlayer(horizontalDirection, verticalDirection, jump);
	}

	private void FixedUpdate()
	{

	}
}
