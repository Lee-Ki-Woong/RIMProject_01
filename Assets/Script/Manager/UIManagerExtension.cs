using UnityEngine;

public static class UIManagerExtension
{
    public static string GetPath(this UIManager uiManager, UIType uiType)
    {
        switch(uiType)
        {
            case UIType.BackGround: return "UI/BackGround";
            default: return null;
        }
    }
}
