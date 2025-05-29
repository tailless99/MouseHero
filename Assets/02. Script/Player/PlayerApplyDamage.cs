using UnityEngine;

public class PlayerApplyDamage : MonoBehaviour
{
    private float attackBaseDamage = 1f;
    private float criticalPercent = 1f;
    
    private const float ATTACK_CORRCTION = 1.5f;
    private const float CRITICAL_DAMAGE = 2f;

    private bool isCritcal = false;

    private void Start() {
        UpdatePlayerStatus();
    }

    // 기본 데미지 반환
    public float GetAttackBaseDamage() => IsCritical() ?
        attackBaseDamage * CRITICAL_DAMAGE : attackBaseDamage;

    /// <summary>
    /// 스테이터스 기반 데미지 갱신
    /// 스테이터스가 달라졌을 때 호출
    /// </summary>
    public void UpdatePlayerStatus() {
        // 기본 데미지 업데이트
        attackBaseDamage = PlayerStatusManager.Instance.GetStatus(StatusType.Strength) * ATTACK_CORRCTION;

        // 소수점이 있을 경우
        if (attackBaseDamage != Mathf.Floor(attackBaseDamage)) {
            attackBaseDamage = Mathf.FloorToInt(attackBaseDamage) + 1;
        }
        // 소수점이 없다면
        else {
            attackBaseDamage = Mathf.FloorToInt(attackBaseDamage);
        }
        
        // 크리티컬 확률 업데이트
        criticalPercent = PlayerStatusManager.Instance.GetStatus(StatusType.Luck);
    }

    private bool IsCritical() {
        isCritcal = criticalPercent >= Random.Range(0, 101);
        return isCritcal;
    }

    public bool IsCriticalAttack() => isCritcal;
}

