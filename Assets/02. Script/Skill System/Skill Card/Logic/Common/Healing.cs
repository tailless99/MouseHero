using UnityEngine;

public class Healing : SkillBase {
    
    public override bool Use() {
        // �ִ� ü��
        var maxHP = PlayerStatusManager.Instance.GetStatus(StatusType.HP);
        var healingPoint = maxHP * 0.15f; // �ִ� ü���� 15%
        PlayerStatusManager.Instance.PlayerHealing(healingPoint);

        return true;
    }
}
