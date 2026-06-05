using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator Animator_Enemy;
    [SerializeField] private Rigidbody2D RigidBody2D_Enemy;
    [SerializeField] private Collider2D Collider2D_Enemy;

    public string Name;
    public int MaxHp;
    public int Hp;
    public int MoveSpeed;
    public int Damage;
    public string AssetAddress;

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnDestroy()
    {
        ResourceManager.Instance.UnLoadAsset(AssetAddress);
    }



    public void SetData(EnemyData enemyData)
    {
        Name = enemyData.Name;
        MaxHp = enemyData.MaxHp;
        Hp = enemyData.MaxHp;
        MoveSpeed = enemyData.MoveSpeed;
        Damage = enemyData.Damage;
        AssetAddress = enemyData.MonsterObjectPath;
    }

    public void GetDamaged(int damage)
    {
        Hp -= damage;
        CheckingHp();
    }

    private void CheckingHp()
    {
        if(Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }




}
