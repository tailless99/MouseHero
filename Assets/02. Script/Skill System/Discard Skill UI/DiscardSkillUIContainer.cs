using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardSkillUIContainer : MonoBehaviour
{
    [SerializeField] DiscardSkillDetail_UI discardSkillDetail_UI;
    [SerializeField] DiscardConfirm discardConfirm;
    [SerializeField] private TextMeshProUGUI countText;

    private void OnEnable() {
        // �� ������Ʈ�� Ȱ��ȭ �� �� �ʱ�ȭ ����
        discardSkillDetail_UI.gameObject.SetActive(false);
        discardConfirm.gameObject.SetActive(false);

        countText.text = SkillManager.Instance.GetHasSkillList().Count.ToString();
    }

    // ��ų ī�� Ŭ�� ��, ������ ���� ���
    public void ShowSelectSkillDetaillUI(SkillCardSlot cardBase) {
        discardSkillDetail_UI.gameObject.SetActive(true);
        discardSkillDetail_UI.Initialize_CardSlotDetail(cardBase.skillSO);
    }

    // �ڷΰ��� ��ư Ŭ���� ���� �Լ�
    public void ReturnPrevView() {
        this.gameObject.SetActive(false);
    }

    // ��� �۾��� ������� ��, ��� UI�� �ݴ� �Լ�
    public void CloseDiscardSkillUI() {
        FullScreenUIManager.Instance.CloseFullScreenUI();
        this.gameObject.SetActive(false);
    }

    // Ȯ��â Ȱ��/��Ȱ�� �Լ�
    public void ToggleActiveConfirmUI() {
        discardConfirm.gameObject.SetActive(!discardConfirm.gameObject.activeSelf);
    }

    // ��ų �����ϴ� �Լ�
    public void DeleteSkillCard() {
        SkillManager.Instance.DeleteHasSkill(discardSkillDetail_UI.GetCurrentSkillSO());
        CloseDiscardSkillUI();
    }
}
