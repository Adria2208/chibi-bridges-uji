using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float jumpForce;
    public float jumpBufferTime;
    public float variableJumpTime;
    public float fastFallMultiplier;
    public float originalGravityScale;
}
