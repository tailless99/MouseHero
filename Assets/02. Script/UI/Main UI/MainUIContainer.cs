using UnityEngine;

public class MainUIContainer : Singleton<MainUIContainer> {
    [Header("Main UI Class")]
    [SerializeField] private PlayerHPSliderContainer hpSlider;
    [SerializeField] public MoneyContainer moneyContainer;


    // HP 갱신
    public void UpdateHpPercent(float hpPercent) => hpSlider.SetHp(hpPercent);

    // 돈 갱신
    public void UpdateMoney(int money) => moneyContainer.UpdateMoney(money);
    
    // 소모 골드보다 많은 골드를 가지고 있는지 체크
    public void CanUseMoney(int money) => moneyContainer.CanUseMoney(money);
}
