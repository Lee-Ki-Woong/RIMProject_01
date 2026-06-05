using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private Collider2D Collider;
    [SerializeField] private SpriteRenderer Renderer;

    private int m_damage;
    private int m_speed;

    private float m_lifeTime;

    private bool m_isSetData;
    private bool m_isSetAsset;

    private Vector3 m_moveDirection;

    private Transform m_enemyTransform;

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        BoolSetting();
    }

    private void BoolSetting()
    {
        m_isSetData = false;
        m_isSetAsset = false;
    }

    public void SetAsset(Sprite sprite)
    {
        Renderer.sprite = sprite;

        m_isSetAsset = true;
    }

    public void SetData(int damage, int speed, float lifeTime, Vector3 direction, Transform enemyTransform)
    {
        m_damage = damage;
        m_speed = speed;
        m_lifeTime = lifeTime;
        m_moveDirection = direction.normalized;
        m_enemyTransform = enemyTransform;

        m_isSetData = true;

        Invoke(nameof(DestroySkill), m_lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamaged(m_damage);
            }

            DestroySkill();
        }
    }

    private void Update()
    {
        if (!m_isSetData) return;
        
        Move();
    }

    private void Move()
    {
        if(m_enemyTransform != null)
        {
            m_moveDirection = (m_enemyTransform.position - transform.position).normalized;
        }

        transform.position += m_moveDirection * m_speed * Time.deltaTime;

        // 날아가는 방향으로 머리를 회전해주는 AI 코드
        if (m_moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void DestroySkill()
    {
        Destroy(this.gameObject);
    }

}
