using UnityEngine;

public class MiloMovement : MonoBehaviour
{
    public enum MovementState
    {
        Wait,
        Slow,
        Normal,
        Fast
    }

    [Header("Current State")]
    public MovementState currentState = MovementState.Normal;

    [Header("Speed Values")]
    public float waitSpeed = 0f;
    public float slowSpeed = 1.5f;
    public float normalSpeed = 3.0f;
    public float fastSpeed = 5.5f;

    private Rigidbody2D rb;
    private float targetSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetState(MovementState.Wait);
    }

    void FixedUpdate()
    {
        // Move horizontally along the 2D plane
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocity.y);
    }

    // Public method so UI buttons can change speed later
    public void SetState(MovementState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case MovementState.Wait:
                targetSpeed = waitSpeed;
                break;
            case MovementState.Slow:
                targetSpeed = slowSpeed;
                break;
            case MovementState.Normal:
                targetSpeed = normalSpeed;
                break;
            case MovementState.Fast:
                targetSpeed = fastSpeed;
                break;
        }
    }

    // Temporary keyboard debug keys for testing before UI is hooked up
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetState(MovementState.Wait);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetState(MovementState.Slow);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetState(MovementState.Normal);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SetState(MovementState.Fast);
    }
}