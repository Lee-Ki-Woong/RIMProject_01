using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator Animator_Character;
    [SerializeField] private Rigidbody2D RigidBody2D_Character;
    [SerializeField] private Collider2D Collider2D_Character;






    private class CharacterInstance
    {
        public string Name;
        public string OtherName;
        public string Class;
        public string Description;
        public int MaxHp;
        public int CharacterHp;
        public int MoveSpeed;
        public string[] SkillList;
    }

    private enum CharacterState : byte
    {
        Idle = 0,
        Move = 1,
    }

    private CharacterInstance m_character;

    public CharacterSkill[] CharacterSkills { get; private set; } = new CharacterSkill[3];
    
    private event Action<int> DamagedEvent;


    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        DamagedEvent = CharacterGetDamaged;
    }

    private void CharacterGetDamaged(int damage)
    {
        m_character.CharacterHp -= damage;
    }

    public void SetData(CharacterData characterData)
    {
        m_character.Name = characterData.Name;
        m_character.OtherName = characterData.OtherName;
        m_character.Class = characterData.Class;
        m_character.Description = characterData.Description;
        m_character.MaxHp = characterData.MaxHp;
        m_character.CharacterHp = characterData.MaxHp;
        m_character.MoveSpeed = characterData.MoveSpeed;
        m_character.SkillList = characterData.SkillList;

        SetSkill();


    }

    private void SetSkill()
    {
        for (int i = 0; i < 3; i++)
        {
            SkillData skillData = GameDataManager.Instance.SkillDataList[m_character.SkillList[i]];
        }





    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            
            if(enemy == null)
            {
                this.LogError($"{collision.gameObject.name}에 Enemy 컴포넌트가 없습니다!!");
            }

            DamagedEvent?.Invoke(enemy.EnemyData_This.Damage);
        }
    }


    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        RigidBody2D_Character.linearVelocity = moveDirection * m_character.MoveSpeed;

        if (moveDirection != Vector2.zero)
        {
            Animator_Character.SetBool("IsWalk", true);
        }
        else
        {
            Animator_Character.SetBool("IsWalk", false);
        }

    }
}