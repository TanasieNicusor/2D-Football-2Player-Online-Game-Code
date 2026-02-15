using UnityEngine;

public class FootballBootScript : MonoBehaviour
{
    void Start()
    {
        startPosition = transform.localPosition;
        startRotation = transform.localRotation;

        _avatar = GetComponentInParent<Alteruna.Avatar>();
        // Safety check
        if (GetComponent<Collider2D>() == null)
            Debug.LogError("Boot is missing a Collider2D!");

        // Ensure we have a Rigidbody2D for collisions to work reliably
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            // Add one automatically if missing (optional, mostly for testing)
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    void Update()
    {
        if (!_avatar.IsMe)
            return;
        // Handle Input in Update (responsive)
        isKicking = Input.GetKey(KeyCode.P);

        // Update progress logic
        float direction = isKicking ? 1f : -1f;
        kickProgress += (Time.deltaTime / kickDuration) * direction;
        kickProgress = Mathf.Clamp01(kickProgress);

        ApplyKickMotion(kickProgress);

    }

    void ApplyKickMotion(float t)
    {
        // Smooth the linear 0-1 value to a curve
        float smoothedT = Mathf.SmoothStep(0f, 1f, t);

        // Rotation
        float zRot = Mathf.Lerp(0f, kickRotation, smoothedT);
        transform.localRotation = startRotation * Quaternion.Euler(0, 0, zRot);

        // Position
        //Vector3 targetPos = startPosition + transform.right * kickDistance;
        Vector3 targetPos = startPosition + new Vector3(transform.right.x * kickDistance, Mathf.Abs(kickDistance), 0f);

        transform.localPosition = Vector3.Lerp(startPosition, targetPos, smoothedT);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("ball"))
            return;

        if (!isKicking)
            return;

        Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (ballRb == null)
            return;
        
        // Direction: forward + slight up
        Vector2 kickDir = (collision.transform.position - transform.position).normalized;
        kickDir += Vector2.up * 0.2f;
        kickDir.Normalize();
        
        // Smooth force curve (0 → 1 → 0)
        float forceFactor = Mathf.Sin(kickProgress * Mathf.PI);
        float force = maxKickForce * forceFactor;

        ballRb.AddForce(kickDir * force, ForceMode2D.Impulse);

        Debug.Log($"Kicked ball | progress: {kickProgress:F2} | force: {force:F2}");
    }

    [Header("Settings")]
    public float kickRotation = 40f;
    public float kickDistance = 0.15f;
    public float kickDuration = 0.3f;
    public float maxKickForce = 12f;

    [Header("Debug")]
    [SerializeField] private float kickProgress = 0f;
    [SerializeField] private bool isKicking;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private bool isFacingRight;

    private Alteruna.Avatar _avatar;


}