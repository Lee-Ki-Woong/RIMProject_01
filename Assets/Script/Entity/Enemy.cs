using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator Animator_Enemy;
    [SerializeField] private Rigidbody2D RigidBody2D_Enemy;
    [SerializeField] private Collider2D Collider2D_Enemy;

    public EnemyData EnemyData_This;


}
