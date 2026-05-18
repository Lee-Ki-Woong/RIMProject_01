public static class UIManagerExtension
{
    public static string GetPath(this UIManager uiManage, UIRootType uiRootType, UIType uiType)
    {
        string path = $"UI/{uiRootType}/{uiType}";
        return path;
    }

    public static void OpenBackGround(this UIManager uiManager)
    {
        uiManager.OpenUI(UIRootType.BackGroundUI, UIType.BackGround);
    }

    public static void OpenMainUI(this UIManager uiManager)
    {
        uiManager.OpenUI(UIRootType.MainUI, UIType.MainMenu);
    }

    public static void OpenLoadingUI(this UIManager uiManager)
    {
        uiManager.OpenUI(UIRootType.TopUI, UIType.LoadingPopup);
    }

    public static void OpenInGameOptionUI(this UIManager uiManager)
    {
        uiManager.OpenUI(UIRootType.PopupUI, UIType.InGameOptionPopup);
    }
}
