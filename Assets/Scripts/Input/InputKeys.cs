using PlayerScripts;
using UnityEngine;

public class InputKeys : MonoBehaviour
{
    [HideInInspector]
    public PlayerMoovement player;
    private PlayerJump playerJump;

    public float horizontalDirection;
	public float verticalDirection;

	public bool jump ;
	public bool isRuning ;


	private void Start()
    {
        player = GetComponent<PlayerMoovement>();
		playerJump = GetComponent<PlayerJump>();
    }

	private void FixedUpdate()
	{
        horizontalDirection = Input.GetAxis(GlobalStringsVars.HORIZONTAL_AXIS);
        verticalDirection = Input.GetAxis(GlobalStringsVars.VERTICAL_AXIS);

        bool hit = Input.GetMouseButton(GlobalStringsVars.FIRE);
        if (hit) player.Hit(true);

		bool notHit = Input.GetMouseButtonUp(GlobalStringsVars.FIRE);
		if (notHit) player.Hit(false);

        isRuning = Input.GetKey(KeyCode.LeftShift);
		player.MovePlayer(horizontalDirection, verticalDirection, isRuning);

	}

	private void LateUpdate()
	{
		jump = Input.GetKeyDown(KeyCode.Space);
        if (jump)
        {
            playerJump.Jump();
		}
		
	}
}
