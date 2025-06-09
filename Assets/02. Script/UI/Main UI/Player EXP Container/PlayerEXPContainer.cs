using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPContainer : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float maxExp = 100f; // 다음 레벨업까지의 경험치
    private float currentExp = 0f; // 실제 경험치
    private int playerLevel = 1; // 플레이어 레벨

    // 경험치를 획득
    public void AddExp(float exp) {
        // 경험치 증가
        currentExp += exp;

        // 최대 경험치 이상이 되었을 경우
        if (currentExp >= maxExp) {
            LevelUp();
        }
        else {
            UpdateUI();
        }
    }

    // 플레이어의 레벨을 얻는다.
    public int GetLevel() => playerLevel;

    // 레벨업 로직
    private void LevelUp() {
        while (currentExp >= maxExp) {
            currentExp -= maxExp; // 경험치를 초과분만 남긴다.
            currentExp = Mathf.Max(currentExp, 0); // 소수점 오버플로우 방지
            maxExp *= 1.5f; // 최대 경험치 증가
            playerLevel += 1; // 레벨 증가

            // 레벨업 이벤트
            PlayerStatusManager.Instance.AddAllStatus(); // 올스텟 + 1
            MainUIContainer.Instance.OpenLevelUpEvent(); // 레벨업 UI 출력
        }
        UpdateUI();
    }

    // 레벨 UI를 갱신
    private void UpdateUI() {
        expSlider.maxValue = 1f;
        expSlider.value = currentExp / maxExp;
        levelText.text = playerLevel.ToString();
    }
}
