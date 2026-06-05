using System;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] private Animator Animator_Character;
    [SerializeField] private Rigidbody2D RigidBody2D_Character;
    [SerializeField] private Collider2D Collider2D_Character;

    [SerializeField] private SpriteRenderer SpriteRenderer_OnFieldCharacter;



    public Player[] players = new Player[4];
    public Player OnFieldPlayer { get; private set; } 

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {

    }

    private void Start()
    {
        StartSetting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void StartSetting()
    {
        for(int i = 0; i < Math.Min(players.Length, GameManager.Instance.PartyData.Count); i++)
        {
            players[i] = new Player();
            players[i].SetData(GameManager.Instance.PartyData[i]);
        }

        ChangeOnFieldCharacter(players[0]);

    }

    private void Update()
    {
        CheckingKeyDown();
        Move();
        CoolTime();
    }

    private void CoolTime()
    {
        for(int i = 0; i < players.Length;i++)
        {
            players[i].UpdateSkillCooldowns(Time.deltaTime, this.transform);
        }
    }


    private void CheckingKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeOnFieldCharacter(players[0]);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeOnFieldCharacter(players[1]);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) ChangeOnFieldCharacter(players[2]);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) ChangeOnFieldCharacter(players[3]);
    }

    private void ChangeOnFieldCharacter(Player player)
    {
        if (player == null) return;
        if (OnFieldPlayer == player) return;

        OnFieldPlayer = player;
        SpriteRenderer_OnFieldCharacter.sprite = LoadUtil.Sync.LoadGeneric<Sprite>(player.CharacterData_This.CharacterStandPath);
    }

    private void Move()
    {
        if(OnFieldPlayer == null)
        {
            this.LogError("온필드 캐릭터가 없습니다!");
            return;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;

        RigidBody2D_Character.linearVelocity = moveDirection * OnFieldPlayer.CharacterData_This.MoveSpeed;
    }


}
