using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static AddressUtil.Async.Sprite.UI;


public class EndlessGameModePresenter : BasePresenter
{
    public EndlessGameMode EndlessGameModeUI { get; private set; }

    private GameObject m_prefab_endlessCharacterInfo;
    private GameObject m_prefab_characterButton;
    private GameObject m_prefab_skillButton;
    private GameObject m_prefab_partySlotButton;

    private EndlessCharacterInfo m_endlessCharacterInfo;

    private Sprite m_background;
    private Sprite m_selectCharacterButton;
    private Sprite m_startGameButton;
    private Sprite m_exitButton;

    private TMP_FontAsset m_baseFont;

    public List<CharacterData> m_partyData { get; private set; } = new();

    public void InitEndlessGameMode(EndlessGameMode endlessGameMode)
    {
        EndlessGameModeUI = endlessGameMode;
    }

    public void OpenEndlessGameModeUI()
    {
        string[] texts = { "출발", "게임 나가기" };
        Action[] actions = { OnClick_StartGameButton, OnClick_ExitButton };

        UIData endlessGameModeData = new()
        {
            Texts = texts,
            Actions = actions
        };

        EndlessGameModeUI.SetData(endlessGameModeData);
        EndlessGameModeUI.SetScore(GameManager.Instance.PlayerModel);
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (sprite_background, sprite_selectCharacterButton, sprite_startGameButton, sprite_exitButton, endlessCharacterInfo, prefab_characterButton, prefab_skillButton, prefab_partySlotButton, font_base) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.EndlessGameMode.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.EndlessGameMode.SelectCharacterButton),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.EndlessGameMode.StartGameButton),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.EndlessGameMode.ExitButton),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Panel.EndlessCharacterInfo),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Character),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Skill),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Party),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_background = sprite_background;
        m_selectCharacterButton = sprite_selectCharacterButton;
        m_startGameButton = sprite_startGameButton;
        m_exitButton = sprite_exitButton;
        m_prefab_endlessCharacterInfo = endlessCharacterInfo;
        m_prefab_characterButton = prefab_characterButton;
        m_prefab_skillButton = prefab_skillButton;
        m_prefab_partySlotButton = prefab_partySlotButton;
        m_baseFont = font_base;

        EndlessGameModeUI.SetAsset(m_background, m_selectCharacterButton, m_startGameButton, m_exitButton, m_baseFont);
        CreateButtonsAndPanel();

        IsAssetLoad = true;
    }

    private void CreateButtonsAndPanel()
    {
        Dictionary<string, CharacterData> characterData = GameDataManager.Instance.CharacterDataList;
        foreach (KeyValuePair<string, CharacterData> dataKV in characterData)
        {
            CharacterData data = dataKV.Value;
            if (data == null || data.Id == null) continue;

            EndlessGameModeUI.CreateCharacterButtons(m_prefab_characterButton, data, OnClick_CharacterButton);
        }

        m_endlessCharacterInfo = EndlessGameModeUI.CreateCharacterInfo(m_prefab_endlessCharacterInfo);
        m_endlessCharacterInfo.SetAsset(m_baseFont);
    }

    private void OnClick_CharacterButton(string id)
    {
        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData) == false)
        {
            return;
        }

        m_endlessCharacterInfo.SetData(characterData);

        if (characterData.SkillList == null || characterData.SkillList.Length == 0)
        {
            Debug.LogError(characterData.Name + "캐릭터의 스킬 리스트가 비어있습니다.");
            return;
        }


        Sprite characterStaindingSprite = LoadUtil.Sync.LoadGeneric<Sprite>(characterData.CharacterStandPath);
        EndlessGameModeUI.SetAssetCharacterStanding(characterStaindingSprite);

        EndlessGameModeUI.DestroyAllSkillButtons();

        for (int i = 0; i < characterData.SkillList.Length; i++)
        {
            GameDataManager.Instance.SkillDataList.TryGetValue(characterData.SkillList[i], out SkillData skillData);

            if (skillData == null)
            {
                Debug.LogError(characterData.Name + $"캐릭터의 {i}번째 스킬 데이터가 존재하지 않습니다.");
                continue;
            }

            EndlessGameModeUI.CreateSkillButtons(m_prefab_skillButton, skillData, OnClick_SkillButton);
        }

        EndlessGameModeUI.BindSelectCharacterButtonEvent(characterData, OnClick_SelectCharacterButton);
    }

    private void OnClick_SkillButton(string id)
    {

    }

    private void OnClick_SelectCharacterButton(string id)
    {
        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData) == false)
        {
            Debug.LogError(id + "에 맞는 characterData가 GameDataManager의 CharacterDataList에 없습니다!!");
            return;
        }

        if (m_partyData.Contains(characterData))
        {
            Debug.LogWarning(characterData.Name + "은 이미 파티에 있습니다!!");
            return;
        }

        m_partyData.Add(characterData);
        EndlessGameModeUI.CreatePartySlot(m_prefab_partySlotButton, characterData, OnClick_CharacterSlotButton);
    }

    private void OnClick_CharacterSlotButton(string id)
    {
        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData) == false)
        {
            Debug.LogError(id + "에 맞는 characterData가 GameDataManager의 CharacterDataList에 없습니다!!");
            return;
        }

        m_partyData.Remove(characterData);
        EndlessGameModeUI.DestroyPartySlot(characterData);
    }

    private void OnClick_StartGameButton()
    {
        UIManager.Instance.CloseUI(UIType.MainMenu);
        UIManager.Instance.CloseUI(UIType.EndlessGameMode);
        GameManager.Instance.SetPartyData(m_partyData);
        UIManager.Instance.OpenInGame().Forget();
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.EndlessGameMode);
    }
}