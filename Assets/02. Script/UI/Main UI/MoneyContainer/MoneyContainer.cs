using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private int CurrentMoney = 0;

    private void Start() {
        // 000000 ���� �ʱ�ȭ
        UpdateMoney(0);
    }

    // ���� ������Ʈ�Ѵ�.
    public void UpdateMoney(int money) {
        // ���� 0 �̸��� �� �� ����.
        int tempMoney = CurrentMoney + money;
        CurrentMoney = tempMoney > 0 ? tempMoney : 0;
        moneyText.text = string.Format("{0:D6}", CurrentMoney);
    }

    // �ܺο��� ���� �Ҹ��ϱ� ���� �Ҹ� ��尡 ���Ǵ��� üũ
    public bool CanUseMoney(int money) {
        return CurrentMoney >= money ? true : false;
    }
}
