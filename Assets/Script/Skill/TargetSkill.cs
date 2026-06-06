using UnityEngine;
using System.Collections.Generic;

public class TargetSkill : Skill
{
    private Transform m_targetEnemy;
    private Vector3 m_moveDirection;

    public override void SetData(int damage, int speed, float lifeTime, Vector3 spawnPosition, List<Transform> enemiesInRange)
    {
        base.SetData(damage, speed, lifeTime, spawnPosition, enemiesInRange);

        // 스킬 생성 시점에 가장 가까운 적을 스스로 탐색
        m_targetEnemy = GetNearestEnemy(spawnPosition, enemiesInRange);
        if (m_targetEnemy != null)
        {
            m_moveDirection = (m_targetEnemy.position - transform.position).normalized;
        }
    }

    protected override void Update()
    {
        if (!m_isSetData) return;
        Move();
    }

    private void Move()
    {
        if (m_targetEnemy != null)
        {
            m_moveDirection = (m_targetEnemy.position - transform.position).normalized;
        }

        transform.position += m_moveDirection * m_speed * Time.deltaTime;

        if (m_moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(m_moveDirection.y, m_moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.GetDamaged(m_damage);
            }
            DestroySkill(); // 타격 후 파괴
        }
    }

    private Transform GetNearestEnemy(Vector3 centerPosition, List<Transform> enemies)
    {
        Transform nearestEnemy = null;
        float minDistanceSqr = float.MaxValue;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null) continue;

            float distanceSqr = (enemies[i].position - centerPosition).sqrMagnitude;
            if (distanceSqr < minDistanceSqr)
            {
                minDistanceSqr = distanceSqr;
                nearestEnemy = enemies[i];
            }
        }
        return nearestEnemy;
    }
}