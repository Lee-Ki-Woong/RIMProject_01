using UnityEngine;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void SetButtonSprite(this Button button, Sprite highlightedAndPressedSprite)
    {
        SpriteState state = button.spriteState;
        state.highlightedSprite = highlightedAndPressedSprite;
        state.pressedSprite =  highlightedAndPressedSprite;
        
        button.spriteState = state;
    }
}
