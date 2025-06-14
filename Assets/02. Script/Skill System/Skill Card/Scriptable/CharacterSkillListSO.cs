using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkillList", menuName = "Skills/CharacterSkillList")]
public class CharacterSkillListSO : ScriptableObject
{
    public List<SkillSO> NinjaFrog_Skill;
    //public List<SkillSO> CharacterB_Skill;
    //public List<SkillSO> CharacterC_Skill;

    public List<SkillSO> GetSkillsByCharacter(string characterId) {
        return characterId switch {
            "NinjaFrog" => NinjaFrog_Skill,
            //"B" => CharacterB_Skill,
            //"C" => CharacterC_Skill,
            _ => new List<SkillSO>()
        };
    }
}
