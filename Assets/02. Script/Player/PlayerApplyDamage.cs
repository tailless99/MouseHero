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

    // �⺻ ������ ��ȯ
    public float GetAttackBaseDamage() => IsCritical() ?
        attackBaseDamage * CRITICAL_DAMAGE : attackBaseDamage;

    /// <summary>
    /// �������ͽ� ��� ������ ����
    /// �������ͽ��� �޶����� �� ȣ��
    /// </summary>
    public void UpdatePlayerStatus() {
        // �⺻ ������ ������Ʈ
        attackBaseDamage = PlayerStatusManager.Instance.GetStatus(StatusType.Strength) * ATTACK_CORRCTION;

        // �Ҽ����� ���� ���
        if (attackBaseDamage != Mathf.Floor(attackBaseDamage)) {
            attackBaseDamage = Mathf.FloorToInt(attackBaseDamage) + 1;
        }
        // �Ҽ����� ���ٸ�
        else {
            attackBaseDamage = Mathf.FloorToInt(attackBaseDamage);
        }
        
        // ũ��Ƽ�� Ȯ�� ������Ʈ
        criticalPercent = PlayerStatusManager.Instance.GetStatus(StatusType.Luck);
    }

    private bool IsCritical() {
        isCritcal = criticalPercent >= Random.Range(0, 101);
        return isCritcal;
    }

    public bool IsCriticalAttack() => isCritcal;
}

