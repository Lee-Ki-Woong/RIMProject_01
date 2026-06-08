using System;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] private Animator Animator_Character;
    [SerializeField] private Rigidbody2D RigidBody2D_Character;
    [SerializeField] private Collider2D Collider2D_Character;

    [SerializeField] private SpriteRenderer SpriteRenderer_OnFieldCharacter;



    public Character[] Characters { get; private set; }
    public Character OnFieldCharacter { get; private set; }

    public event Action<Character, Character[]> OnCharacterChanged;

    private List<Transform> m_enemiesTransform = new List<Transform>();

    private void Awake()
    {
        this.ActiveFalse();
    }

    public void InitCharacterData(List<CharacterData> characterDatas)
    {
        Characters = new Character [characterDatas.Count];

        for (int i = 0; i < characterDatas.Count; i++)
        {
            Characters[i] = new Character();
            Characters[i].InitCharacterData(characterDatas[i]);
        }

        ChangeOnFieldCharacter(Characters[0]);
        this.ActiveTrue();
    }

    public void OnFieldCharacterGetDamaged(int damage)
    {
        OnFieldCharacter.CharacterGetDamaged(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            m_enemiesTransform.Add(collision.transform);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            m_enemiesTransform.Remove(collision.transform);
        }
    }

    private void Update()
    {
        CheckingKeyDown();
        Move();
        AutoFireSkill();
    }

    private void AutoFireSkill()
    {
        if (OnFieldCharacter == null) return;

        for (int i = m_enemiesTransform.Count - 1; i >= 0; i--)
        {
            if (m_enemiesTransform[i] == null)
            {
                m_enemiesTransform.RemoveAt(i);
            }
        }

        if (m_enemiesTransform.Count == 0) return;

        OnFieldCharacter.UpdateAndFireSkill(Time.deltaTime, this.transform, m_enemiesTransform);
    }

    private void CheckingKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Characters[0] != null)
            {
                ChangeOnFieldCharacter(Characters[0]);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Characters[1] != null)
            {
                ChangeOnFieldCharacter(Characters[1]);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Characters[2] != null)
            {
                ChangeOnFieldCharacter(Characters[2]);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Characters[3] != null)
            {
                ChangeOnFieldCharacter(Characters[3]);
            }
        }
    }

    private void ChangeOnFieldCharacter(Character Character)
    {
        if (Character == null) return;
        if (OnFieldCharacter == Character) return;

        OnFieldCharacter = Character;
        SpriteRenderer_OnFieldCharacter.sprite = LoadUtil.Sync.LoadGeneric<Sprite>(Character.CharacterData_This.CharacterStandPath);

        OnCharacterChanged?.Invoke(OnFieldCharacter, Characters);
    }

    private void Move()
    {
        if(OnFieldCharacter == null)
        {
            this.LogError("온필드 캐릭터가 없습니다!");
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        RigidBody2D_Character.linearVelocity = moveDirection * OnFieldCharacter.CharacterData_This.MoveSpeed;
    }


}
