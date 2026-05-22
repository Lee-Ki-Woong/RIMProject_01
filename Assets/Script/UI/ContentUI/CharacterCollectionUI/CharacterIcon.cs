using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : BasePanel
{
    // [SerializeField]
    [SerializeField] Button Button_Character;
    [SerializeField] Image Image_CharacterButton;
    [SerializeField] GameObject GameObject_Selected;
    [SerializeField] Image Image_Selected;


    // [Event]
    private event Action<string> OnClick_Icon;


    // [Field]
    private string m_iconDataId;


    // [Life Cycle]
    private void Awake()
    {
        IsAssetLoad = false;
        Button_Character.onClick.AddListener(OnClick_CharacterButton);
    }


    // [Load Asset]
    public override async UniTask SetAssetAsync()
    {
        GameDataManager.Instance.CharacterDataList.TryGetValue(m_iconDataId, out CharacterData value);

        Sprite characterButtonSprite = await  LoadUtil.LoadSpriteAsync(value.Icon_path, destroyCancellationToken);

        Image_CharacterButton.sprite = characterButtonSprite;

        IsAssetLoad = true;
    }

    
    // [Set IconDataId]
    public void SetData(string id, Action<string> onClickCallback)
    {
        m_iconDataId = id;
        OnClick_Icon += onClickCallback;
    }


    // [Bind Button Event]
    private void OnClick_CharacterButton()
    {
        OnClick_Icon?.Invoke(m_iconDataId);
    }
}
