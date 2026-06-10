using System;

[Serializable]
public class UIDataBase
{
    public string Id;
}

[Serializable]
public class FirstPopupData : UIDataBase
{
    public string tutorialText;
    public string resumeButtonText;
}

[Serializable]
public class MainMenuData : UIDataBase
{
    public string FirstButton;
    public string SecondButton;
    public string ThirdButton;
    public string FourthButton;
    public string FifthButton;
}

[Serializable]
public class CharacterCollectionData : UIDataBase
{
    public string CharacterInfoButton;
    public string CharacterSkillButton;
}

[Serializable]
public class EndlessGameModeData : UIDataBase
{
    public string GameStartButton;
    public string ReturnButton;
}

[Serializable]
public class LanguagePopupData : UIDataBase
{
    public string KoreanButton;
    public string EnglishButton;
    public string ExitButton;
}

[Serializable]
public class InGamePopupData : UIDataBase
{
    public string ResumeButton;
    public string ReStartButton;
    public string GameOptionButton;
    public string MainMenuButton;
}

[Serializable]
public class DiePopupData : UIDataBase
{
    public string GameOverText;
    public string ScoreText;
    public string ExitButton;
}
