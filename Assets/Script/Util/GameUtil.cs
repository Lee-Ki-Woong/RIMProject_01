public static class GameUtil
{
    public static string SetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                {
                    return "Enlish";
                }
            case Language.Korean:
                {
                    return "Korean";
                }
            default:
                {
                    return "Korean";
                }
        }
    }

    public static Language GetLanguage(string language)
    {
        switch(language)
        {
            case "English":
                {
                    return Language.English;
                }
            case "Korean":
                {
                    return Language.Korean;
                }
                default:
                {
                    return Language.English;
                }
        }
    }
}
