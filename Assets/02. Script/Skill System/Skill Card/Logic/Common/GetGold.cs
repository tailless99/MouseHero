using UnityEngine;

public class GetGold : SkillBase
{
    public override bool Use() {
        MainUIContainer.Instance.UpdateMoney(30); // �÷��̾� �� ȹ��

        return true;
    }
}
