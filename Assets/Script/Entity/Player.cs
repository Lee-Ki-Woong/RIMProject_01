using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;
using System;

public class Character
{
    public CharacterData CharacterData_This { get; private set; }
    public int Hp { get; private set; }

    public event Action<int, int> OnHp;

    private SkillData[] m_skillDatas = new SkillData[3];
    private GameObject[] m_skillPrefabs = new GameObject[3];
    private Sprite[] m_skillSprites = new Sprite[3];
    private float[] m_skillTimers = new float[3];

    private enum CharacterState : byte
    {
        Idle = 0,
        Move = 1,
    }

    public void CharacterGetDamaged(int damage)
    {
        Hp -= damage;
        CheckingHp();
    }

    private void CheckingHp()
    {
        OnHp?.Invoke(Hp, CharacterData_This.MaxHp);

        if (Hp <= 0)
        {

        }
    }

    public void InitCharacterData(CharacterData characterData)
    {
        CharacterData_This = characterData;
        Hp = CharacterData_This.MaxHp;

        LoadAsset();
    }

    private void LoadAsset()
    {
        if (CharacterData_This.SkillList.Length < 3)
        {
            Debug.LogError($"{CharacterData_This.Name}의 스킬이 3개가 아닙니다!!");
            return;
        }

        for (int i = 0; i < 3; i++)
        {
            GameDataManager.Instance.SkillDataList.TryGetValue(CharacterData_This.SkillList[i], out SkillData skillData);

            if (skillData == null)
            {
                Debug.LogError($"{i}번째 스킬 데이터가 없습니다!!");
                continue;
            }

            LoadAssetSync(i, skillData);
            LoadAssetAsync(i, skillData).Forget();
            m_skillDatas[i] = skillData;
        }
    }

    private void LoadAssetSync(int index, SkillData skillDtata)
    {
        GameObject prefab = LoadUtil.Sync.LoadPrefab(skillDtata.SkillObjectPath);
        m_skillPrefabs[index] = prefab;

    }

    private async UniTask LoadAssetAsync(int index, SkillData skillData)
    {
        Sprite sprite_skillData = await LoadUtil.Async.LoadSpriteAsync(skillData.SkillSpritePath);

        m_skillSprites[index] = sprite_skillData;
    }

    public void UpdateAndFireSkill(float deltaTime, Transform playerTransform, List<Transform> enemiesInRange)
    {
        for (int i = 0; i < 3; i++)
        {
            if (m_skillPrefabs[i] == null || m_skillDatas[i] == null) continue;

            m_skillTimers[i] += deltaTime;

            if (m_skillTimers[i] >= m_skillDatas[i].CoolDown)
            {
                m_skillTimers[i] = 0f;
                FireSkill(i, playerTransform, enemiesInRange);
            }
        }
    }
    private void FireSkill(int skillIndex, Transform spawnTransform, List<Transform> enemiesInRange)
    {
        GameObject skillInstance = UnityEngine.Object.Instantiate(m_skillPrefabs[skillIndex], spawnTransform.position, Quaternion.identity);

        if (skillInstance.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            if (m_skillSprites[skillIndex] != null)
            {
                spriteRenderer.sprite = m_skillSprites[skillIndex];
            }
        }

        if (skillInstance.TryGetComponent(out Skill skillScript))
        {
            SkillData skillData = m_skillDatas[skillIndex];

            // [수정] spawnTransform.position (Vector3) 대신 spawnTransform (Transform) 자체를 넘겨줍니다.
            skillScript.SetData(skillData.Damage, skillData.speed, skillData.LifeTime, spawnTransform, enemiesInRange);
        }
    }
}