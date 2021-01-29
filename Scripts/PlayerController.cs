using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField, Tooltip("What is ground - what does the character 'land' on?")] private LayerMask groundLayer;
    [SerializeField, Tooltip("More are better but more costly")] private int horizontalRayCount;            // how many rays are used to detect grounding
    [SerializeField, Range(.01f, 0.99f)] private float padding;

    private new Collider2D collider;

    private bool prevGrounded = false;                  // was the character grounded the previous update?

    public float TimeLeftGround { get; private set; }   // when did the character leave the ground?

    public bool Grounded { get; private set; }          // is the character grounded?
    public bool JustLanded { get; private set; }        // did the character land this frame?

    /// <summary>
    /// Uses raycasts to check if the character is on the ground
    /// </summary>
    /// <returns>True if the character is on ground with correct layermask. False if the character is in the air or the ground is incorrectly layered.</returns>
    private bool IsGrounded() {
        bool retVal = false;

        for (int i = 0; i < horizontalRayCount; i++) {
            // evenly divides rays along the bottom of the characters colldier
            Vector2 origin = transform.position - Vector3.right * collider.bounds.extents.x * (1 - padding);

            // cast ray downward using the ground-layer
            RaycastHit2D hit = Physics2D.Raycast(origin +
                Vector2.right * ((collider.bounds.size.x * (1 - padding)) / (horizontalRayCount - 1)) * i,
                Vector2.down,
                collider.bounds.extents.y + 0.1f, 
                groundLayer);

            // if any one ray hits the return value will be true
            if (hit) {
                retVal = true;
                break;
            }
        }

        // character just landed if the character was not grounded the previous frame, yet grounded this one
        JustLanded = !prevGrounded && retVal;
        if (prevGrounded && !retVal) TimeLeftGround = Time.time;    // character just left the ground - set time

        return prevGrounded = retVal;
    }

    private void Awake() => collider = GetComponent<Collider2D>();
    private void Update() => Grounded = IsGrounded();   // check whether character is grounded every frame once rather than using IsGrounded() in multiple other update-methods.
}
