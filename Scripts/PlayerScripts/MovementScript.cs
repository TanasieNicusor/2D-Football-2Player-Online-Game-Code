using UnityEngine;

public class MovementScript : MonoBehaviour
{
    private Alteruna.Avatar _avatar;

    void Start()
    {
        _avatar = GetComponentInParent<Alteruna.Avatar>();
    }

    void Update()
    {
        if (!_avatar.IsMe)
            return;

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower * 8f);
        }
        // 2. STOP JUMP (Variable Height)
        // Changed GetButtonDown -> GetButtonUp (check for release)
        // Changed GetKey -> GetKeyUp (check for release)
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W)) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);
        }


        Flip();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) == true)
            rb.velocity = new Vector2(horizontal * RunSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(horizontal * WalkSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public bool isMe()
    {
        return _avatar.IsMe;
    }

    private float horizontal;
    [SerializeField] private float WalkSpeed = 20f;
    [SerializeField] private float RunSpeed = 40f;
    [SerializeField] private float jumpingPower = 40f;

    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;
}


