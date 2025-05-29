using TMPro;
using UnityEngine;

public class MoneyContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private int CurrentMoney = 0;

    private void Start() {
        // 000000 으로 초기화
        UpdateMoney(0);
    }

    // 돈을 업데이트한다.
    public void UpdateMoney(int money) {
        // 돈은 0 미만이 될 수 없다.
        int tempMoney = CurrentMoney + money;
        CurrentMoney = tempMoney > 0 ? tempMoney : 0;
        moneyText.text = string.Format("{0:D6}", CurrentMoney);
    }

    // 외부에서 돈을 소모하기 전에 소모 골드가 충당되는지 체크
    public bool CanUseMoney(int money) {
        return CurrentMoney >= money ? true : false;
    }
}
