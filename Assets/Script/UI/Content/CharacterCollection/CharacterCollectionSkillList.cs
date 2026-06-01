using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollectionSkillList : MonoBehaviour
{
    [Serializable]
    private class SkillList
    {
        public Button Button_Skill;
        public Image Image_Skill;
        public TMP_Text Text_Skill;
    }

    [SerializeField] private SkillList[] Skills;

    public void SetData(CharacterData characterData)
    {
        string[] skillList = characterData.SkillList;

        for (int i = 0; i < Math.Min(skillList.Length, Skills.Length); i++)
        {
            Skills[i].Text_Skill.text = skillList[i];
        }
    }

    public void SetAsset(TMP_FontAsset fontAsset)
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].Text_Skill.font = fontAsset;
        }
    }

}
