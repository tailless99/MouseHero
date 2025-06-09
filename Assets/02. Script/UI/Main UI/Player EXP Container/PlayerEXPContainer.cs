using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPContainer : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float maxExp = 100f; // ���� ������������ ����ġ
    private float currentExp = 0f; // ���� ����ġ
    private int playerLevel = 1; // �÷��̾� ����

    // ����ġ�� ȹ��
    public void AddExp(float exp) {
        // ����ġ ����
        currentExp += exp;

        // �ִ� ����ġ �̻��� �Ǿ��� ���
        if (currentExp >= maxExp) {
            LevelUp();
        }
        else {
            UpdateUI();
        }
    }

    // �÷��̾��� ������ ��´�.
    public int GetLevel() => playerLevel;

    // ������ ����
    private void LevelUp() {
        while (currentExp >= maxExp) {
            currentExp -= maxExp; // ����ġ�� �ʰ��и� �����.
            currentExp = Mathf.Max(currentExp, 0); // �Ҽ��� �����÷ο� ����
            maxExp *= 1.5f; // �ִ� ����ġ ����
            playerLevel += 1; // ���� ����

            // ������ �̺�Ʈ
            PlayerStatusManager.Instance.AddAllStatus(); // �ý��� + 1
            MainUIContainer.Instance.OpenLevelUpEvent(); // ������ UI ���
        }
        UpdateUI();
    }

    // ���� UI�� ����
    private void UpdateUI() {
        expSlider.maxValue = 1f;
        expSlider.value = currentExp / maxExp;
        levelText.text = playerLevel.ToString();
    }
}
