using System.IO;
using UnityEngine;

public static class UIManagerExtension
{
    public static string GetPath(this UIManager uiManage, UIType uiType)
    {
        string path = uiType.ToString();
        return path;
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
