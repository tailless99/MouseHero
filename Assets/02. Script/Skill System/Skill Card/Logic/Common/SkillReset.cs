using UnityEngine;

public class SkillReset : SkillBase
{
    public override bool Use() {
        var skillStackContainer = MainUIContainer.Instance.GetSkillStackContainer();
        skillStackContainer.SkillStackReset();
        return true;
    }

}
