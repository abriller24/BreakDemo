using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField]float moveSpeed = 2f;

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
}
