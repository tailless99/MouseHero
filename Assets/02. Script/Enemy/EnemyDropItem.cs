using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    public int minDropMoney = 1;
    public int maxDropMoney = 5;
    public float GiveExp = 5f;

    public void DropItem() {
        // ��� ���� ����ġ �߿� �����ϰ� ȹ��
        int addMoney = Random.Range(minDropMoney, maxDropMoney + 1);
        MainUIContainer.Instance.UpdateMoney(addMoney);
    }

    // ����� �÷��̾ ȹ���� ����ġ�� ��ȯ�Ѵ�.
    public float GetExp() => GiveExp;
}
