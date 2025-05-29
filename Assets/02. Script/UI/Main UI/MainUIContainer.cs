using UnityEngine;

public class MainUIContainer : Singleton<MainUIContainer> {
    [SerializeField] private PlayerHPSliderContainer hpSlider;


    // HP °»½Å
    public void UpdateHpPercent(float hpPercent) {
        hpSlider.SetHp(hpPercent);
    }
}
