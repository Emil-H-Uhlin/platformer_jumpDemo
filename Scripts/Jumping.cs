using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles jumping and various settings related to jumping
/// </summary>
public class Jumping : MonoBehaviour {
    [SerializeField] private JumpSettings settings;
    [SerializeField] private float forgivenessTime = 0.1f;
    [SerializeField] private float saveInputTime = 0.1f;

    public JumpSettings JumpSettings { get => settings; set => settings = value; }

    #region Rigidbody2D.addForce(..)-jump
    public bool rbAddForceJump { get; set; } = false;   // for demonstration purposes only

    public float RBAddForceAmount { 
        get => rbAddForceAmount; 
        set {
            if (value < .1) rbAddForceAmount = .1f;
            else rbAddForceAmount = value;
        } 
    }

    private float rbAddForceAmount = 17.5f;
    #endregion

    private PlayerController controller;
    private Rigidbody2D rb;

    private bool jumping = false;

    private float startJumpY = -1;          // at what y-position did the jump start
    private float startJumpTime = -1;       // at what time did the jump start
    private float defaultGravityScale = -1; // what is the 'default' gravity scale as defined by rigidbody.gravityScale in inspector

    private float endHover = -1;            // at what time does hovering forcedly stop

    private float jumpKeyPressedTime = -1;  // when did the jump key get pressed (for saving inputs when still falling)

    private void Awake() {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        defaultGravityScale = rb.gravityScale;
    }

    /// <summary>
    /// Handles input and gravity for the character
    /// </summary>
    private void Update() {
        // gravity is enabled when either rb.addForce()-jump is enabled or if the character is not jumping or hovering
        rb.gravityScale = rbAddForceJump || !(jumping || (Time.time < endHover && Input.GetKey(KeyCode.Space))) ? defaultGravityScale : 0;

        if (!jumping) {
            bool jumpKeyPressed = Input.GetKeyDown(KeyCode.Space);

            if (jumpKeyPressed) jumpKeyPressedTime = Time.time;                 // store time when jump key gets pressed

            bool savedInput = Time.time < jumpKeyPressedTime + saveInputTime;   // has player pressed jump key recently

            bool canJump = controller.Grounded || Time.time < controller.TimeLeftGround + forgivenessTime;

            if (canJump && (jumpKeyPressed || savedInput)) {                    // jump if character can jump and jump-key is pressed or pressed recently
                if (rbAddForceJump) {
                    jumping = true;
                    rb.AddForce(Vector2.up * rbAddForceAmount, ForceMode2D.Impulse);
                }
                else StartJump();
            }
        }
        else if (rbAddForceJump) {
            if (controller.JustLanded && rb.velocity.y < 0.1) {                 // character is no longer jumping if character has landed
                jumping = false;
            }
        }
    }

    private void FixedUpdate() => UpdateJump();

    private void StartJump() {
        jumping = true;
        startJumpY = transform.position.y;
        startJumpTime = Time.time;
    }

    private void UpdateJump() {
        if (!jumping || rbAddForceJump) return;                                         // jump does not need to update unless actually jumping without rb.addForce()

        float jumpHeight = transform.position.y - startJumpY;                           // how high has the character jumped

        // has the character exceeded the minimum jump range, and not holding jump-key still
        if (jumpHeight >= settings.MinJumpHeight && !Input.GetKey(KeyCode.Space)) {     
            jumping = false;                                                                // end jump
            rb.velocity = new Vector3(rb.velocity.x, settings.FullJumpHeight - jumpHeight); // make ending of jump smooth by setting velocity relative to the jumpheight

            return;
        }

        float timeFract = MathHelper.Fraction(Time.time, startJumpTime, startJumpTime + settings.TimeForMaxJump);   // how far along is the jump

        Vector3 newPos = transform.position;

        if (settings.IsLinear)
            newPos.y = MathHelper.Fractal(timeFract, startJumpY, startJumpY + settings.FullJumpHeight);                                 // linearly update to-be-y-position according to time-axis
        else 
            newPos.y = MathHelper.Fractal(1 - ((1 - timeFract) * (1 - timeFract)), startJumpY, startJumpY + settings.FullJumpHeight);   // smoothly update to-be-y-position for fast movement at the start of the jump, and slow movement at the end of the jump

        Vector3 v = rb.velocity;

        if (timeFract >= 1.0f) {                                // end of jump reached
            endHover = Time.time + settings.HoverTime;          // start hover-timer

            jumping = false;                                    // no longer jumping upwards
            newPos.y = startJumpY + settings.FullJumpHeight;    // do not allow player to accidentally jump higher than allowed

            transform.position = newPos;                        

            v.y = 0;
            rb.velocity = v;

            return;
        }

        v.y = (newPos - transform.position).y / Time.fixedDeltaTime;    // move character by using velocity to allow for collision detection

        rb.velocity = v;
    }
}
