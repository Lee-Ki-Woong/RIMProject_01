public static class AddressUtil
{
    public static class Sync
    {
        public static class Prefab
        {
            public static string BasePrefab = "Base/Prefab";
            public static string BaseParty = "Prefab/Party";
            public static string BaseEnemy = "Prefab/Enemy";

            public static class UI
            {
                public const string MainMenu = "Prefab/UI/MainMenu";
                public const string InGame = "Prefab/UI/InGame";
                public const string EndlessGameMode = "Prefab/UI/EndlessGameMode";
                public const string CharacterCollection = "Prefab/UI/CharacterCollection";
                public const string InGamePopup = "Prefab/UI/InGamePopup";
                public const string NewMainMenu = "Prefab/UI/NewMainMenu";
                public const string SkillStatePopup = "Prefab/UI/SkillStatePopup";
                public const string FirstStartPopup = "Prefab/UI/FirstStartPopup";
                public const string DiePopup = "Prefab/UI/DiePopup";
                public const string LanguagePopup = "Prefab/UI/LanguagePopup";
            }
        }
    }

    public static class Async
    {
        public static class Prefab
        {
            public static string BasePrefab = "Base/Prefab";

            public static class Button
            {
                public const string Character = "Prefab/Button/Character";
                public const string Skill = "Prefab/Button/Skill";
                public const string Party = "Prefab/Button/Party";
            }

            public static class Panel
            {
                public const string CharacterInfo = "Prefab/Panel/CharacterInfo";
                public const string EndlessCharacterInfo = "Prefab/Panel/EndlessCharacterInfo";
            }
        }

        public static class Sprite
        {
            public static string BaseSprite = "Base/Sprite";

            public static class UI
            {
                public const string Button_Empty = "Sprite/UI/Button_Empty";

                public static class MainMenu
                {
                    public const string TitleText = "Sprite/UI/MainMenu/TitleText";
                    public const string TitleImage = "Sprite/UI/MainMenu/TitleImage";
                    public const string MenuButton_Highlighted = "Sprite/UI/MainMenu/Button";
                }

                public static class CharacterCollection
                {
                    public const string Background = "Sprite/UI/CharacterCollection/Background";
                    public const string MenuButton_Highlighted = "Sprite/UI/CharacterCollection/Highlighted";
                    public const string MenuButton_Selected = "Sprite/UI/CharacterCollection/Selected";
                    public const string ExitButton = "Sprite/UI/CharacterCollection/Exit";
                }

                public static class CharacterButton
                {
                    public const string Edge = "Sprite/UI/CharacterButton/Edge";
                    public const string Mask = "Sprite/UI/CharacterButton/Mask";
                    public const string Selected = "Sprite/UI/CharacterButton/Selected";
                }

                public static class SkillButton
                {
                    public const string Selected = "Sprite/UI/SkillButton/Selected";
                }

                public static class EndlessGameMode
                {
                    public const string Background = "Sprite/UI/EndlessGameMode/Background";
                    public const string SelectCharacterButton = "Sprite/UI/EndlessGameMode/SelectCharacter";
                    public const string StartGameButton = "Sprite/UI/EndlessGameMode/StartGame";
                    public const string ExitButton = "Sprite/UI/CharacterCollection/Exit";
                }

                public static class InGame
                {
                    public const string MenuPopupButton = "Sprite/UI/InGame/MenuPopupButton";
                    public const string Misson = "Sprite/UI/InGame/Misson";

                }

                public static class InGamePopup
                {
                    public const string Background = "Sprite/UI/InGamePopup/Background";
                    public const string Button = "Sprite/UI/InGamePopup/Button";
                }

                public static class FirstPopup
                {
                    public const string Background = "Sprite/UI/FirstPopup/Background";
                    public const string ResumeButton = "Sprite/UI/FirstPopup/ResumeButton";
                }

                public static class SkillState
                {
                    public const string Background = "Sprite/UI/SkillState/Background";
                    public const string ExitButton = "Sprite/UI/SkillState/ExitButton";
                }

                public static class DiePopup
                {
                    public const string Background = "Sprite/UI/DiePopup/Background";
                    public const string ExitButton = "Sprite/UI/DiePopup/ExitButton";
                }

                public static class LanguagePopup
                {
                    public const string Background = "Sprite/UI/LanguagePopup/Background";
                    public const string MenuButtons = "Sprite/UI/LanguagePopup/MenuButtons";
                }
            }
        }

        public static class Font
        {
            public static string BaseFont = "Base/Font";
        }
    }

}
