using UnityEngine;

public class MainUIContainer : Singleton<MainUIContainer> {
    [Header("Main UI Class")]
    [SerializeField] private PlayerHPSliderContainer hpSlider;
    [SerializeField] public MoneyContainer moneyContainer;


    // HP ����
    public void UpdateHpPercent(float hpPercent) => hpSlider.SetHp(hpPercent);

    // �� ����
    public void UpdateMoney(int money) => moneyContainer.UpdateMoney(money);
    
    // �Ҹ� ��庸�� ���� ��带 ������ �ִ��� üũ
    public void CanUseMoney(int money) => moneyContainer.CanUseMoney(money);
}
