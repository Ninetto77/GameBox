using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

	public float sensitivityHor = 9f;
	public float sensitivityVer = 9f;

	Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        character = GetComponentInParent<PlayerMoovement>().transform;
    }

    void Update()
    {
        if (Time.timeScale > 0f)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") * sensitivityHor, Input.GetAxisRaw("Mouse Y") * sensitivityVer);
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }
    }
}
