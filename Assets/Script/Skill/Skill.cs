using UnityEngine;
using System.Collections.Generic;

public class Skill : MonoBehaviour
{
    [SerializeField] protected Collider2D Collider;
    [SerializeField] protected SpriteRenderer Renderer;

    protected int m_damage;
    protected int m_speed;

    protected float m_lifeTime;

    protected bool m_isSetData;
    protected bool m_isSetAsset;

    protected Vector3 m_spawnPosition;

    protected List<Transform> m_enemiesTransform;

    protected void Awake()
    {
        AwakeSetting();
    }

    protected void AwakeSetting()
    {
        BoolSetting();
    }

    protected void BoolSetting()
    {
        m_isSetData = false;
        m_isSetAsset = false;
    }

    protected virtual void SetAsset(Sprite sprite)
    {
        Renderer.sprite = sprite;

        m_isSetAsset = true;
    }

    public virtual void SetData(int damage, int speed, float lifeTime, Vector3 direction, List<Transform> enemiesTransform)
    {
        m_damage = damage;
        m_speed = speed;
        m_lifeTime = lifeTime;
        m_spawnPosition = direction.normalized;
        m_enemiesTransform = enemiesTransform;

        m_isSetData = true;

        Invoke(nameof(DestroySkill), m_lifeTime);
    }

    protected virtual void Update()
    {
    }

    protected virtual void DestroySkill()
    {
        Destroy(this.gameObject);
    }

}
