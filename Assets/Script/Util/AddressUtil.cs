public static class AddressUtil
{
    public static class Sync
    {
        public static class Prefab
        {
            public static string BasePrefab = "Base/Prefab";

            public static class UI
            {
                public const string MainMenu = "Prefab/UI/MainMenu";
                public const string InGame = "Prefab/UI/InGame";
                public const string EndlessGameMode = "Prefab/UI/EndlessGameMode";
                public const string CharacterCollection = "Prefab/UI/CharacterCollection";
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
            }

            public static class Panel
            {
                public const string CharacterInfo = "Prefab/Panel/CharacterInfo";
                public const string CharacterSkill = "Prefab/Panel/CharacterSkill";
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
                    public const string ExitButton = "Sprite/UI/CharacterCollection/ExitButton";
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
                    public const string SelectCharacterButton = "Sprite/UI/EndlessGameMode/SelectCharacterButton";
                    public const string StartGameButton = "Sprite/UI/EndlessGameMode/StartGameButton";
                    public const string ExitButton = "Sprite/UI/CharacterCollection/ExitButton";
                }

                public static class InGame
                {
                    public const string MenuPopupButton = "Sprite/UI/InGame/MenuPopupButton";
                    public const string Misson = "Sprite/UI/InGame/Misson";

                }
            }
        }

        public static class Font
        {
            public static string BaseFont = "Base/Font";
        }
    }
}
