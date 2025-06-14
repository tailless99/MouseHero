using System.Collections;
using UnityEngine;

public class NinjaFrog : PlayerCharacterBase
{
    [SerializeField] private GameObject bulletTimeEffect;

    public override void Attack() {
        base.Attack();
    }

    // �ҷ�Ÿ�� ��ų
    public bool BulletTime(float time, int moneySpent) {
        // �ҷ�Ÿ���� �ߺ� ������ �Ұ����ϴ�
        if (bulletTimeEffect.activeSelf) return false;
        bulletTimeEffect.SetActive(true); // �Ҹ� Ÿ�� Ÿ�̸� ����Ʈ Ȱ��ȭ

        BulletTimeStart(time, moneySpent);

        return true;
    }

    private IEnumerator BulletTimeStart(float time, int moneySpent) {
        // ��� �Ҹ�
        MainUIContainer.Instance.UpdateMoney(-moneySpent);

        // ���� ����
        var str = PlayerStatusManager.Instance.GetStatus(StatusType.Strength);
        var addStr = ((moneySpent * 1.1f) + (str * 1.5f)) * str; // ���� ������
        PlayerStatusManager.Instance.AddStatus(StatusType.Strength, addStr); // ���� ����
        yield return new WaitForSecondsRealtime(time);

        // ���� �ð� ���� �� �������ͽ� ���󺹱�
        PlayerStatusManager.Instance.AddStatus(StatusType.Strength, -addStr); // ���� ����
    }

    // �Ҹ� �ִϸ��̼ǿ��� ȣ���� �Լ�
    public void BulletTimeEnd() => bulletTimeEffect.SetActive(false);
}
