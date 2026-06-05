using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class Player
{
    public CharacterData CharacterData_This { get; private set; }

    public GameObject Prefab_FirstSkill {  get; private set; }
    public GameObject Prefab_SecondSkill {  get; private set; }
    public GameObject Prefab_ThirdSkill { get; private set; }
    public int Hp { get; private set; }

    private SkillData[] m_skillDatas = new SkillData[3];
    
    private GameObject[] m_skillPrefabs = new GameObject[3];
    
    private float[] m_skillTimers = new float[3];

    private enum CharacterState : byte
    {
        Idle = 0,
        Move = 1,
    }

    private event Action<int> DamagedEvent;

    public void CharacterGetDamaged(int damage)
    {
        Hp -= damage;
        CheckingHp();
    }

    private void CheckingHp()
    {
        if(Hp <= 0)
        {

        }
    }

    public void SetData(CharacterData characterData)
    {
        CharacterData_This = characterData;
        LoadAssetAsync();
    }

    private void LoadAssetAsync()
    {
        GameDataManager.Instance.SkillDataList.TryGetValue(CharacterData_This.SkillList[0], out SkillData firstSkillData);
        GameDataManager.Instance.SkillDataList.TryGetValue(CharacterData_This.SkillList[1], out SkillData secondSkillData);
        GameDataManager.Instance.SkillDataList.TryGetValue(CharacterData_This.SkillList[2], out SkillData thirdSkillData);

        var (preafab_FirstSkill, prefab_SecondSkill, prefab_ThirdSkill) = (
            LoadUtil.Sync.LoadPrefab(firstSkillData.SkillObjectPath),
            LoadUtil.Sync.LoadPrefab(secondSkillData.SkillObjectPath),
            LoadUtil.Sync.LoadPrefab(thirdSkillData.SkillObjectPath)
            );

        Prefab_FirstSkill = preafab_FirstSkill;
        Prefab_SecondSkill = prefab_SecondSkill;
        Prefab_ThirdSkill = prefab_ThirdSkill;

        m_skillPrefabs[0] = Prefab_FirstSkill;
        m_skillPrefabs[1] = Prefab_SecondSkill;
        m_skillPrefabs[2] = Prefab_ThirdSkill;

        m_skillDatas[0] = firstSkillData;
        m_skillDatas[1] = secondSkillData;
        m_skillDatas[2] = thirdSkillData;

        SetSkill();
    }

    private void SetSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            m_skillTimers[i] = 0f;
        }
    }

    public void UpdateSkillCooldowns(float deltaTime, Transform playerTransform)
    {
        for (int i = 0; i < 3; i++)
        {
            if (m_skillPrefabs[i] == null || m_skillDatas[i] == null) continue;

            m_skillTimers[i] += deltaTime;

            if (m_skillTimers[i] >= m_skillDatas[i].CoolDown)
            {
                m_skillTimers[i] = 0f;
                FireSkill(i, playerTransform);
            }
        }
    }

    private void FireSkill(int skillIndex, Transform spawnTransform)
    {
        Transform target = FindNearestEnemy(spawnTransform.position);

        GameObject skillObj = UnityEngine.Object.Instantiate(m_skillPrefabs[skillIndex], spawnTransform.position, Quaternion.identity);

        if (skillObj.TryGetComponent<Skill>(out var skill))
        {
            SkillData data = m_skillDatas[skillIndex];
            Vector3 direction;

            if (target != null)
            {
                direction = target.position - spawnTransform.position;
                skill.SetData(data.Damage, data.speed, data.LifeTime, direction, target);
            }
            else
            {
                float randomX = UnityEngine.Random.Range(-1f, 1f);
                float randomY = UnityEngine.Random.Range(-1f, 1f);
                direction = new Vector3(randomX, randomY, 0f);

                skill.SetData(data.Damage, data.speed, data.LifeTime, direction, null);
            }
        }
    }

    private Transform FindNearestEnemy(Vector3 centerPosition)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform nearestEnemy = null;
        float minDistance = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(centerPosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }
        return nearestEnemy;
    }

}