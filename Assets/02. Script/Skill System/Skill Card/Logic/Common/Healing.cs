using UnityEngine;

public class Healing : SkillBase {
    
    public override bool Use() {
        // 최대 체력
        var maxHP = PlayerStatusManager.Instance.GetStatus(StatusType.HP);
        var healingPoint = maxHP * 0.15f; // 최대 체력의 15%
        PlayerStatusManager.Instance.PlayerHealing(healingPoint);

        return true;
    }
}
