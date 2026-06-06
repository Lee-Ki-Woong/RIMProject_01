using UnityEngine;
using System.Collections.Generic;

public class MultiTargetSkill : Skill
{
    [Header("발사할 개별 투사체 프리팹")]
    [SerializeField] private GameObject m_subProjectilePrefab;

    // 발사 후 본체는 바로 역할을 다하므로 lifeTime을 짧게 가져가거나 즉시 파괴되게 구성
    public override void SetData(int damage, int speed, float lifeTime, Vector3 spawnPosition, List<Transform> enemiesInRange)
    {
        base.SetData(damage, speed, lifeTime, spawnPosition, enemiesInRange);

        FireToAllEnemies();
    }

    private void FireToAllEnemies()
    {
        if (m_enemiesTransform == null || m_enemiesTransform.Count == 0)
        {
            DestroySkill();
            return;
        }

        foreach (Transform enemy in m_enemiesTransform)
        {
            if (enemy == null) continue;

            // 각 적을 향해 서브 투사체 생성
            GameObject subProjectile = Instantiate(m_subProjectilePrefab, m_spawnPosition, Quaternion.identity);

            // 서브 투사체는 '단일 타겟' 투사체 스크립트를 재사용
            if (subProjectile.TryGetComponent(out TargetSkill projSkill))
            {
                // 서브 투사체가 추적할 수 있도록 타겟 하나만 담은 임시 리스트 전달
                List<Transform> singleTargetList = new List<Transform> { enemy };
                projSkill.SetData(m_damage, m_speed, m_lifeTime, m_spawnPosition, singleTargetList);
            }
        }

        // 투사체를 전부 흩뿌렸으므로 이 다중 스킬 본체 오브젝트는 바로 파괴
        DestroySkill();
    }
}