using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    public float jumpStrength = 2;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void OnValidate()
    {
        rb ??= GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded))
        {
            rb.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }
}
