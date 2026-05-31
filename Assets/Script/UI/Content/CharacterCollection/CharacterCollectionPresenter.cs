using Cysharp.Threading.Tasks;
using UnityEngine;

public class CharacterCollectionPresenter : BasePresenter
{
    public CharacterCollection CharacterCollection { get; private set; }

    private GameObject m_characterIconPrefab;
    private GameObject m_characterInfoPrefab;
    private GameObject m_characterSkillListPrefab;
    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MenuButton_Highlighted;

    public void InitCharacterCollection(CharacterCollection characterCollection)
    {
        CharacterCollection = characterCollection;
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        IsAssetLoad = true;
    }

    public void GoCharacterCollection()
    {
    }
}
