using UnityEngine;

public class HikerFollower : MonoBehaviour
{
    [Header("Follow Target")]
    public Transform target;          // The leader this hiker trails
    public float followDistance = 2.0f; // Target gap to maintain behind the leader

    [Header("Movement Settings")]
    public float maxSpeed = 5.5f;     // Matches Milo's Fast speed
    public float stoppingTolerance = 0.2f;

    private Rigidbody2D rb;
    private MiloMovement miloMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Find Milo in the scene to reference current state
        GameObject miloObj = GameObject.Find("Milo");
        if (miloObj != null)
        {
            miloMovement = miloObj.GetComponent<MiloMovement>();
        }
    }

    void FixedUpdate()
    {
        if (target == null) return;

        // Calculate horizontal offset from target
        float targetX = target.position.x - followDistance;
        float currentX = transform.position.x;
        float distanceToTarget = targetX - currentX;

        // Check if Milo has commanded the party to WAIT
        if (miloMovement != null && miloMovement.currentState == MiloMovement.MovementState.Wait)
        {
            // Come to a smooth halt
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        // If close enough to target position, stop moving horizontally
        if (Mathf.Abs(distanceToTarget) <= stoppingTolerance)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            return;
        }

        // Determine direction and follow speed based on Milo's current state
        float moveSpeed = 0f;
        if (miloMovement != null)
        {
            switch (miloMovement.currentState)
            {
                case MiloMovement.MovementState.Slow:
                    moveSpeed = miloMovement.slowSpeed;
                    break;
                case MiloMovement.MovementState.Normal:
                    moveSpeed = miloMovement.normalSpeed;
                    break;
                case MiloMovement.MovementState.Fast:
                    moveSpeed = miloMovement.fastSpeed;
                    break;
            }
        }

        // Catch up slightly faster if falling too far behind
        if (distanceToTarget > followDistance * 1.5f)
        {
            moveSpeed = maxSpeed;
        }

        float moveDirection = Mathf.Sign(distanceToTarget);
        rb.linearVelocity = new Vector2(moveDirection * moveSpeed, rb.linearVelocity.y);
    }
}