using UnityEngine;

public static class UIManagerExtension
{
    public static string GetPath(this UIManager uiManager, UIType uiType)
    {
        switch(uiType)
        {
            case UIType.BackGround: return "UI/BackGround";
            case UIType.Main: return "UI/MainUI";
            default: return null;
        }
    }

    public static void OpenBackGround(this UIManager uiManager)
    {
        uiManager.OpenUI(UIType.BackGround);
    }

    public static void OpenMainUI(this UIManager uiManager)
    {
        uiManager.OpenUI(UIType.Main);
    }
}
