using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float jumpForce = 7f;
    public LayerMask groundLayer;

    public Rigidbody rb;
    public SpriteRenderer sr;

    bool isGrounded;
    float xInput;
    float yInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // stops player from tipping over
    }

    void Update()
    {
        // Movement input
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Sprite flip
        if (xInput < 0) sr.flipX = true;
        else if (xInput > 0) sr.flipX = false;

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x, 
                jumpForce, 
                rb.linearVelocity.z
            );
        }
    }

    void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(xInput, 0f, yInput).normalized;

        rb.velocity = new Vector3(
            moveDir.x * speed,
            rb.velocity.y,
            moveDir.z * speed
        );

        float checkDistance = 0.6f; // tweak to match your collider height
        Vector3 origin = transform.position + Vector3.up * 0.1f;

        isGrounded = Physics.Raycast(
            origin,
            Vector3.down,
            checkDistance,
            groundLayer
        );
    }
}
