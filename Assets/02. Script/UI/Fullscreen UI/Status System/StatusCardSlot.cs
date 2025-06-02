using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class StatusCardSlot : MonoBehaviour
{
    [Header("Status Controller")]
    public Status_UI_Container statusController;

    [Header("UI Setting")]
    public TextMeshProUGUI statusName;      // ���� �̸�
    public TextMeshProUGUI statusNumber;    // ���� ��ġ(����)
    public TextMeshProUGUI addStatNumber;    // �߰��� ���� ��ġ(����)
    public StatusType type;                 // ���� ����
    
    // ���� ��� ����
    private int currentAddStat = 0;

    private void Start() {
        statusName.text = type.ToString();
    }

    private void OnEnable() {
        UpdateStatusNumber();  // ���� ��������Ʈ ����
        addStatNumber.text = currentAddStat.ToString();
    }

    // ��ư Ŭ�� �̺�Ʈ
    public void AddStatus() {
        // �����ݺ��� �Ҹ� ��尡 �� ũ�ٸ� ����
        var result = statusController.StatusCardAddBtnClick();
        if (!result) return;

        // ���� ����
        currentAddStat++;
        UpdateAddStatusText();
    }

    // ��ư Ŭ�� �̺�Ʈ
    public void MinusStatus() {
        var result = statusController.StatusCardMinusBtnClick();
        if (currentAddStat <= 0 || !result) return;

        // ���� ����
        currentAddStat--;
        UpdateAddStatusText();
    }

    // �߰��� �������ͽ� ����Ʈ ����
    public void UpdateAddStatusText() {
        addStatNumber.text = currentAddStat.ToString();
    }

    // ���� �������ͽ� ��ġ ������Ʈ
    public void UpdateStatusNumber() {
        currentAddStat = 0;
        statusNumber.text = PlayerStatusManager.Instance.GetStatus(type).ToString();
    }

    // �÷��̾��� ������ �߰� ���ݸ�ŭ ������Ų��.
    public void UpdatePlayerStatus() {
        if (currentAddStat == 0) return;
        PlayerStatusManager.Instance.AddStatus(type, currentAddStat);
    }
}
