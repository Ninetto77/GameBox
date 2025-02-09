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

		isRuning = Input.GetKey(KeyCode.LeftShift);
		player.MovePlayer(horizontalDirection, verticalDirection, isRuning);
	}
}
