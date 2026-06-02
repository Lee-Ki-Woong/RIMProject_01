using Cysharp.Threading.Tasks;
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

    [SerializeField] private SkillList FirstSkill;
    [SerializeField] private SkillList SecondSkill;
    [SerializeField] private SkillList ThirdSkill;

    string[] SkillId;

    public void SetData(SkillData[] skillData)
    {
        SkillId = new string[] { skillData[0].Id, skillData[1].Id, skillData[2].Id };

        InitData(FirstSkill, skillData[0]);
        InitData(SecondSkill, skillData[1]);
        InitData(ThirdSkill, skillData[2]);
    }

    private void InitData(SkillList skillList, SkillData skillData)
    {
        skillList.Text_Skill.text = skillData.Name;
    }

    private async UniTask LoadSprite(string address)
    {
        await LoadUtil.Async.LoadSpriteAsync(address);
    }

    public void SetAsset(TMP_FontAsset fontAsset)
    {

    }

    private void InitAsset()
    {

    }
}
