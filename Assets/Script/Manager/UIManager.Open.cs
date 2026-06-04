using Cysharp.Threading.Tasks;

public partial class UIManager
{
    public MainMenuPresenter m_mainMenuPresenter { get; private set; }
    public InGamePresenter m_inGamePresenter { get; private set; }
    public CharacterCollectionPresenter m_characterCollectionPresenter { get; private set; }
    public EndlessGameModePresenter m_endlessGameModePresenter { get; private set; }

    public async UniTask OpenMainMenu()
    {
        if (m_mainMenuPresenter == null)
        {
            m_mainMenuPresenter = new MainMenuPresenter();
        }

        m_mainMenuPresenter.InitMainMenu(CreateUI<MainMenu>(UIType.MainMenu));

        await m_mainMenuPresenter.LoadAndSetAssetAsync();

        m_activeUI.Add(UIType.MainMenu);
        m_mainMenuPresenter.MainMenuUI.ActiveTrue();
        m_mainMenuPresenter.OpenMainMenuUI();
    }

    public async UniTask OpenInGame()
    {
        if (m_inGamePresenter == null)
        {
            m_inGamePresenter = new InGamePresenter();
        }

        m_inGamePresenter.InitInGame(CreateUI<InGame>(UIType.InGame));

        await m_inGamePresenter.LoadAndSetAssetAsync();

        m_activeUI.Add(UIType.InGame);
        m_inGamePresenter.InGame.ActiveTrue();
    }

    public async UniTask OpenEndlessGameMode()
            {
        if (m_endlessGameModePresenter == null)
        {
            m_endlessGameModePresenter = new EndlessGameModePresenter();
        }
        m_endlessGameModePresenter.InitEndlessGameMode(CreateUI<EndlessGameMode>(UIType.EndlessGameMode));
        await m_endlessGameModePresenter.LoadAndSetAssetAsync();
        m_activeUI.Add(UIType.EndlessGameMode);
        m_endlessGameModePresenter.EndlessGameModeUI.ActiveTrue();
        m_endlessGameModePresenter.OpenEndlessGameModeUI();
    }

    public async UniTask OpenCharacterCollection()
    {
        if (m_characterCollectionPresenter == null)
        {
            m_characterCollectionPresenter = new CharacterCollectionPresenter();
        }

        m_characterCollectionPresenter.InitCharacterCollection(CreateUI<CharacterCollection>(UIType.CharacterCollection));

        await m_characterCollectionPresenter.LoadAndSetAssetAsync();

        m_activeUI.Add(UIType.CharacterCollection);
        m_characterCollectionPresenter.CharacterCollectionUI.ActiveTrue();
        m_characterCollectionPresenter.OpenCharacterCollectionUI();
    }

}
