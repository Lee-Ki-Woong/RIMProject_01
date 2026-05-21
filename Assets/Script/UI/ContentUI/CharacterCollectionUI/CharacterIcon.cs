using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIcon : BaseUI
{
    // [SerializeField]
    [Header("Character")]
    [SerializeField] Button Button_Character;
    [SerializeField] Image Image_CharacterButton;
    [SerializeField] GameObject GameObject_Selected;
    [SerializeField] Image Image_Selected;


    // [Event]
    private event Action<string> m_onClick_Icon;

    // [Field]
    private string m_iconDataId;

    // [Life Cycle]
    private void Awake()
    {
        Button_Character.onClick.AddListener(OnClick_CharacterButton);
    }

    // [Load]
    public override async UniTask SetAssetAsync()
    {
        GameDataManager.Instance.CharacterDataList.TryGetValue(m_iconDataId, out CharacterData value);

        (Image_CharacterButton.sprite, Image_Selected.sprite) =await UniTask.WhenAll
            (
            LoadUtil.LoadSpriteAsync(value.Icon_path, destroyCancellationToken),
            LoadUtil.LoadSpriteAsync("", destroyCancellationToken)
            );
    }

    // [Bind Button Event]
    private void OnClick_CharacterButton()
    {
        m_onClick_Icon?.Invoke(m_iconDataId);
    }

}
