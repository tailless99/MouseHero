using UnityEngine;

public class SkillDraw : SkillBase {
    public override bool Use() {
        var skillStackContainer = MainUIContainer.Instance.GetSkillStackContainer();
        skillStackContainer.DrawNewSkillCard();

        return true;
    }
}
