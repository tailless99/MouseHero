using UnityEngine;

public class GetGold : SkillBase
{
    public override bool Use() {
        MainUIContainer.Instance.UpdateMoney(30); // ÇÃ·¹ÀÌ¾î µ· È¹µæ

        return true;
    }
}
