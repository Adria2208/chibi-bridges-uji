using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Horizontal movement parameters")]
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    
    [Header("Jump parameters")]
    public float jumpForce;
    [Range(0f, 0.5f)]
    public float jumpBufferTime;
    [Range(0f, 1f)]
    public float variableJumpTime;
    public float fastFallMultiplier;
    public float originalGravityScale;
}
