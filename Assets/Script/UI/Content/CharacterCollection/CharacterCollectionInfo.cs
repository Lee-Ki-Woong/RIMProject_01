using TMPro;
using UnityEngine;
using System;

public class CharacterCollectionInfo : MonoBehaviour
{
    [Serializable]
    private class CharacterInfo
    {
        public TMP_Text Text_Title;
        public TMP_Text Text_Data;
    }

    [SerializeField] private CharacterInfo[] Texts;

    [SerializeField] private TMP_Text Text_CharacterDescription;

    public void SetData(CharacterData data)
    {
        string[] datas = { data.Name, data.OtherName, data.Class, data.MaxHp.ToString(), data.MoveSpeed.ToString() };

        for(int i = 0; i < Math.Min(Texts.Length, datas.Length); i++)
        {
            Texts[i].Text_Data.text = datas[i];
        }

        Text_CharacterDescription.text = data.Description;
    }

    public void SetAsset(TMP_FontAsset fontAsset)
    {
        for (int i = 0; i < Texts.Length; i++)
        {
            Texts[i].Text_Data.font = fontAsset;
            Texts[i].Text_Title.font = fontAsset;
        }

        Text_CharacterDescription.font = fontAsset;
    }
}
