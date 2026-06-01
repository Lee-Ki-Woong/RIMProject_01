using Cysharp.Threading.Tasks;

public partial class UIManager
{
    public MainMenuPresenter m_mainMenuPresenter;
    public InGamePresenter m_inGamePresenter;
    public CharacterCollectionPresenter m_characterCollectionPresenter;

    public async UniTask OpenMainMenu()
    {
        if (m_mainMenuPresenter == null)
        {
            m_mainMenuPresenter = new MainMenuPresenter();
        }

        m_mainMenuPresenter.InitMainMenu(CreateUI<MainMenu>(UIType.MainMenu));

        await m_mainMenuPresenter.LoadAndSetAssetAsync();

        m_mainMenuPresenter.MainMenuUI.ActiveTrue();
        m_mainMenuPresenter.GoMainMenu();
    }

    public async UniTask OpenInGame()
    {
        if (m_inGamePresenter == null)
        {
            m_inGamePresenter = new InGamePresenter();
        }

        m_inGamePresenter.InitInGame(CreateUI<InGame>(UIType.InGame));

        await m_inGamePresenter.LoadAndSetAssetAsync();

        m_inGamePresenter.InGame.ActiveTrue();
    }

    public async UniTask OpenCharacterCollection()
    {
        if (m_characterCollectionPresenter == null)
        {
            m_characterCollectionPresenter = new CharacterCollectionPresenter();
        }

        m_characterCollectionPresenter.InitCharacterCollection(CreateUI<CharacterCollection>(UIType.CharacterCollection));

        await m_characterCollectionPresenter.LoadAndSetAssetAsync();

        m_characterCollectionPresenter.CharacterCollectionUI.ActiveTrue();
        m_characterCollectionPresenter.SetCharacterCollection();
    }

    public async UniTask OpenEndlessGameMode()
    {
    }

    private void Start()
    {
        OpenMainMenu().Forget();
    }
}
