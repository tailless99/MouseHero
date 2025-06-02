using TMPro;
using UnityEngine;

public class Status_UI_Container : MonoBehaviour
{
    [Header("GameObject")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI totalCostText;
    public GameObject confirmView; // Confirm View
    public GameObject scrollContent; // ��ũ�� ���� ����Ʈ ���� ������Ʈ

    [Header("Data")]
    public int statusCost = 15;

    // ���� �Լ�
    private int totalCost = 0;


    private void Start() {
        // �ʱ�ȭ
        moneyText.text = MainUIContainer.Instance.GetCurrentMoney().ToString();
        totalCostText.text = totalCost.ToString();
    }

    /// <summary>
    /// �����ݰ� �Ҹ���� ���ؼ� �Ҹ���� �� ũ�ٸ� false�� ��ȯ,
    /// �������� �� ũ�ٸ� true�� ��ȯ�ϸ� total Cost�� ���� �����ϴ� ������ ����.
    /// </summary>
    /// <returns></returns>
    public bool StatusCardAddBtnClick() {
        // ������ ���� �Ҹ��尡 ũ�ٸ�
        bool result = MainUIContainer.Instance.CanUseMoney(totalCost + statusCost);
        if (!result) return result;

        // �������� �� Ŭ ��� ���� ����
        AddTotalCost();
        UpdateTotalCostText();

        return true;
    }

    public bool StatusCardMinusBtnClick() {
        // ��Ż �ڽ�Ʈ�� �Ҹ� �ڽ�Ʈ���� ���� ���
        if (totalCost < statusCost) return false;

        // ��Ż �ڽ�Ʈ ���� ���� ����
        totalCost -= statusCost;
        UpdateTotalCostText();

        return true;
    }

    // �ڽ�Ʈ ���� ó��
    private void AddTotalCost() => totalCost += statusCost;

    // �� �ڽ�Ʈ UI�� �ؽ�Ʈ ������Ʈ
    private void UpdateTotalCostText() => totalCostText.text = totalCost.ToString();

    // Ȱ��/��Ȱ��ȭ ��� �Լ�
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }

    // Ȯ��â Ȱ��/��Ȱ��ȭ ���
    public void OpenConfrimUI() {
        if (totalCost == 0) return;

        bool result = confirmView.gameObject.activeSelf;
        confirmView.gameObject.SetActive(!result);
    }

    // ī�帶�� �߰� ���ݸ�ŭ �÷��̾� ���� ����
    public void UpdatePlayerStatus() {
        Transform contentTransform = scrollContent.transform;
        int childCount = contentTransform.childCount;

        for (int i = 0; i < childCount; i++) {
            Transform childTransform = contentTransform.GetChild(i);
            var childObject = childTransform.gameObject.GetComponent<StatusCardSlot>();
            childObject.UpdatePlayerStatus();
            childObject.UpdateStatusNumber();
        }
        
        // ��ó��
        OpenConfrimUI(); // Ȯ��â �ݱ�
        MainUIContainer.Instance.UpdateMoney(-totalCost); // ������ �Ҹ� �ݿ�
        moneyText.text = MainUIContainer.Instance.GetCurrentMoney().ToString(); // ������ �Ҹ� �ݿ�
        totalCost = 0;
        UpdateTotalCostText(); // ��Ż �ڽ�Ʈ �ʱ�ȭ
    }
}
