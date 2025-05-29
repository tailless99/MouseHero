using UnityEngine;
using UnityEngine.UI;

public class PlayerHPSliderContainer : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHp(float hpPercent) {
        slider.value = hpPercent;
    }
}
