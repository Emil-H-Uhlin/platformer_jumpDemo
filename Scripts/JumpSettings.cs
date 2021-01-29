using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Serialized jump settings used in jump-script
/// Can be created as files using the jump-setting option in inspector
/// </summary>
[CreateAssetMenu(fileName = "New jump settings", menuName = null)] public class JumpSettings : ScriptableObject {
    [SerializeField] private float fullJumpHeight = 3;
    [SerializeField] private float minJumpHeight = 1;

    [SerializeField] private float timeUntilMaxHeightReached = .6f;
    [SerializeField] private float maxHoverTime = 0.2f;

    [SerializeField] private bool linearJump = false;

    public float FullJumpHeight { 
        get => fullJumpHeight; 
        set {
            if (value < .0) fullJumpHeight = .0f; 
            else fullJumpHeight = value; 
        } 
    }
    
    public float MinJumpHeight { 
        get => minJumpHeight; 
        set => minJumpHeight = Mathf.Clamp(value, 0, FullJumpHeight);   // clamped between 0 and max jump height
    }

    public float TimeForMaxJump { 
        get => timeUntilMaxHeightReached;
        set {
            if (value < .0) timeUntilMaxHeightReached = .0f;
            else timeUntilMaxHeightReached = value;
        }
    }

    public float HoverTime { 
        get => maxHoverTime;
        set {
            if (value < .0) maxHoverTime = .0f;
            else maxHoverTime = value;
        }
    }
    
    public bool IsLinear { get => linearJump; set => linearJump = value; }
}
