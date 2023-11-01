using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeys : MonoBehaviour
{
    public PlayerMoovement player;

    private void Start()
    {
        player = GetComponent<PlayerMoovement>();
    }

    /// <summary>
    /// ��������� ������� ���������� � ���������� �� � ������� �������� ������
    /// </summary>
    private void Update()
    {
        float horizontalDirection = Input.GetAxis(GlobalStringsVars.HORIZONTAL_AXIS);
        float verticalDirection = Input.GetAxis(GlobalStringsVars.VERTICAL_AXIS);

        player.MovePlayer(horizontalDirection, verticalDirection);
    }
}
