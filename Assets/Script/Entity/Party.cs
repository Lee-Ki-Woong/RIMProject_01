using System;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] private Animator Animator_Character;
    [SerializeField] private Rigidbody2D RigidBody2D_Character;
    [SerializeField] private Collider2D Collider2D_Character;

    [SerializeField] private SpriteRenderer SpriteRenderer_OnFieldCharacter;



    private Player[] m_players;
    private Player m_onFieldPlayer;

    private List<Transform> m_enemiesTransform = new List<Transform>();

    private void Awake()
    {
        AwakeSetting();
        this.ActiveFalse();
    }

    private void AwakeSetting()
    {

    }

    public void InitPlayerData(List<CharacterData> characterDatas)
    {
        m_players = new Player [characterDatas.Count];

        for (int i = 0; i < characterDatas.Count; i++)
        {
            m_players[i] = new Player();
            m_players[i].InitCharacterData(characterDatas[i]);
        }

        this.ActiveTrue();
    }

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        ChangeOnFieldCharacter(m_players[0]);
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
        if (m_onFieldPlayer == null) return;

        for (int i = m_enemiesTransform.Count - 1; i >= 0; i--)
        {
            if (m_enemiesTransform[i] == null)
            {
                m_enemiesTransform.RemoveAt(i);
            }
        }

        if (m_enemiesTransform.Count == 0) return;

        m_onFieldPlayer.UpdateAndFireSkill(Time.deltaTime, this.transform, m_enemiesTransform);
    }

    private void CheckingKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_players[0] != null)
            {
                ChangeOnFieldCharacter(m_players[0]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_players[1] != null)
            {
                ChangeOnFieldCharacter(m_players[1]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_players[2] != null)
            {
                ChangeOnFieldCharacter(m_players[2]);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (m_players[3] != null)
            {
                ChangeOnFieldCharacter(m_players[3]);
            }
        }
    }

    private void ChangeOnFieldCharacter(Player player)
    {
        if (player == null) return;
        if (m_onFieldPlayer == player) return;

        m_onFieldPlayer = player;
        SpriteRenderer_OnFieldCharacter.sprite = LoadUtil.Sync.LoadGeneric<Sprite>(player.CharacterData_This.CharacterStandPath);
    }

    private void Move()
    {
        if(m_onFieldPlayer == null)
        {
            this.LogError("온필드 캐릭터가 없습니다!");
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        RigidBody2D_Character.linearVelocity = moveDirection * m_onFieldPlayer.CharacterData_This.MoveSpeed;
    }


}
