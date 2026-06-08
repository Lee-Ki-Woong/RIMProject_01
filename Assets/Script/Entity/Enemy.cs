using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator Animator_Enemy;
    [SerializeField] private Rigidbody2D RigidBody2D_Enemy;
    [SerializeField] private Collider2D Collider2D_Enemy;

    private string m_name;
    private int m_maxHp;
    private int m_hp;
    private int m_moveSpeed;
    private int m_damage;
    private string m_assetAddress;

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            if(collision.gameObject.TryGetComponent(out Party party))
            {
                party.OnFieldCharacterGetDamaged(m_damage);
            }
        }
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.UnLoadAsset(m_assetAddress);
    }



    public void SetData(EnemyData enemyData)
    {
        m_name = enemyData.Name;
        m_maxHp = enemyData.MaxHp;
        m_hp = enemyData.MaxHp;
        m_moveSpeed = enemyData.MoveSpeed;
        m_damage = enemyData.Damage;
        m_assetAddress = enemyData.MonsterObjectPath;
    }

    public void GetDamaged(int damage)
    {
        m_hp -= damage;
        CheckingHp();
    }

    private void CheckingHp()
    {
        if(m_hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }




}
