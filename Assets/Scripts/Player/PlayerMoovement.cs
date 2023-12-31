using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerMoovement : MonoBehaviour
{
    public float Speed = 20f;
    public float Gravity = -9.8f;

    public Vector3 direction;

    public CharacterController player;

    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    /// <summary>
    /// �������� ������ �� ���� ������������ � ������ ����������
    /// </summary>
    /// <param name="horizontalDirection">�������������� �����������</param>
    /// <param name="verticalDirection">������������ �����������</param>
    public void MovePlayer(float horizontalDirection, float verticalDirection)
    {
        direction = new Vector3(horizontalDirection * Speed, 0, verticalDirection * Speed);
        //����������� ��������
        direction = Vector3.ClampMagnitude(direction, Speed);

        //����������
        direction.y = -Gravity;

        direction *= Time.deltaTime;
        direction = transform.TransformDirection(direction);
        //��������
        player.Move(direction);

    }

    //private void Update()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;
    //}
}
