using UnityEngine;
using UnityEngine.UI;

public static class UIExtension
{
    public static void SetButtonSprite(this Button button, Sprite highlightedSprite = default, Sprite pressedSprite = default)
    {
        SpriteState state = button.spriteState;

        if (highlightedSprite != null)
        {
            state.highlightedSprite = highlightedSprite;
        }

        if (pressedSprite != null)
        {
            state.pressedSprite = pressedSprite;
        }

        button.spriteState = state;
    }
}
