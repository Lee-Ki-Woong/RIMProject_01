using Cysharp.Threading.Tasks;

public partial class UIManager
{
    private MainMenuPresenter m_mainMenuPresenter;

    public void OpenMainMenu()
    {
        if (m_mainMenuPresenter == null)
        {
            m_mainMenuPresenter = new MainMenuPresenter();
        }

        m_activeUI.Add(UIType.MainMenu);
        m_mainMenuPresenter.InitMainMenu(CreateUI<MainMenu>(UIType.MainMenu));
    }

    public InGamePresenter m_inGamePresenter { get; private set; }

    public void OpenInGame()
    {
        if (m_inGamePresenter == null)
        {
            m_inGamePresenter = new InGamePresenter();
        }

        m_activeUI.Add(UIType.InGame);
        m_inGamePresenter.InitInGame(CreateUI<InGame>(UIType.InGame));
    }

    private EndlessGameModePresenter m_endlessGameModePresenter;

    public void OpenEndlessGameMode()
    {
        if (m_endlessGameModePresenter == null)
        {
            m_endlessGameModePresenter = new EndlessGameModePresenter();
        }

        m_activeUI.Add(UIType.EndlessGameMode);
        m_endlessGameModePresenter.InitEndlessGameMode(CreateUI<EndlessGameMode>(UIType.EndlessGameMode));
    }

    private CharacterCollectionPresenter m_characterCollectionPresenter;

    public void OpenCharacterCollection()
    {
        if (m_characterCollectionPresenter == null)
        {
            m_characterCollectionPresenter = new CharacterCollectionPresenter();
        }

        m_activeUI.Add(UIType.CharacterCollection);
        m_characterCollectionPresenter.InitCharacterCollection(CreateUI<CharacterCollection>(UIType.CharacterCollection));
    }

    private InGamePopupPresenter m_inGamePopupPresenter;

    public void OpenInGamePopup(int score)
    {
        if (m_inGamePopupPresenter == null)
        {
            m_inGamePopupPresenter = new InGamePopupPresenter();
        }

        m_activeUI.Add(UIType.InGamePopup);
        m_inGamePopupPresenter.InitInGamePopup(CreateUI<InGamePopup>(UIType.InGamePopup));
    }

    private SkillStatePopupPresenter m_skillStatePopupPresenter;

    public void OpenSkillState(SkillData skillData)
    {
        if (m_skillStatePopupPresenter == null)
        {
            m_skillStatePopupPresenter = new SkillStatePopupPresenter();
        }

        m_skillStatePopupPresenter.InitSkillStatePopup(CreateUI<SkillStatePopup>(UIType.SkillStatePopup));

        m_skillStatePopupPresenter.InitSkillData(skillData);
        m_activeUI.Add(UIType.SkillStatePopup);
    }

    private DiePopupPresenter m_diePopupPresenter;

    public void OpenDiePopup()
    {
        if (m_diePopupPresenter == null)
        {
            m_diePopupPresenter = new DiePopupPresenter();
        }

        m_diePopupPresenter.InitDiePopup(CreateUI<DiePopup>(UIType.DiePopup));
        m_activeUI.Add(UIType.DiePopup);
    }

    private FirstPopupPresenter m_firstPopupPresenter;

    public void OpenFirstPopup()
    {
        if (m_firstPopupPresenter == null)
        {
            m_firstPopupPresenter = new FirstPopupPresenter();
        }

        m_firstPopupPresenter.InitFirstPopup(CreateUI<FirstPopup>(UIType.FirstStartPopup));
        m_activeUI.Add(UIType.FirstStartPopup);
    }
}
