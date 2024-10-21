using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public float GetMoveSpeed() { return moveSpeed; }
    public void SetMoveSpeed(float speedToSet) { moveSpeed = speedToSet; }
}
