using UnityEngine;
using System.Collections.Generic;

public class AoeSkill : Skill
{
    [SerializeField] private float m_tickRate = 0.5f; // 타격 주기 (예: 0.5초마다 데미지)
    private float m_tickTimer;

    private List<Enemy> m_enemiesInside = new List<Enemy>();

    protected override void Update()
    {
        if (!m_isSetData) return;

        m_tickTimer += Time.deltaTime;
        if (m_tickTimer >= m_tickRate)
        {
            m_tickTimer = 0f;
            ApplyAreaDamage();
        }
    }

    private void ApplyAreaDamage()
    {
        // 리스트를 역순으로 순회하며 죽은 적(null)은 리스트에서 제거하고, 살아있는 적에게만 데미지
        for (int i = m_enemiesInside.Count - 1; i >= 0; i--)
        {
            if (m_enemiesInside[i] == null)
            {
                m_enemiesInside.RemoveAt(i);
                continue;
            }
            m_enemiesInside[i].GetDamaged(m_damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (!m_enemiesInside.Contains(enemy))
                    m_enemiesInside.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (m_enemiesInside.Contains(enemy))
                    m_enemiesInside.Remove(enemy);
            }
        }
    }
}