using UnityEngine;

public class SkillShopConfirm : MonoBehaviour
{
    [SerializeField] private GameObject returnFailDialogUI;
    [SerializeField] private GameObject ConfirmDIalogUI;

    private SkillSO skillSO;

    // Buy ��ư�� ���� ȣ��
    // ���� ���� ���ο� ���� ������ Dialog�� �����ؼ� Ȱ��ȭ
    public void ShowConfirmDialog(SkillSO skill) {
        skillSO = skill;
        var CanUseMoney = MainUIContainer.Instance.CanUseMoney(skillSO.cost); // ���� ����Ѱ�
        var CanAddSkill = SkillManager.Instance.CanAddNewSkill(); // ��ų ���� ������ ������ �ִ���

        // ������ �� �ִ��� ���ο� ���� ������ �䰡 �޶���
        if (CanUseMoney && CanAddSkill) ConfirmDIalogUI.SetActive(true);
        else returnFailDialogUI.SetActive(true);
    }

    // ��� ���̾� �α� â�� ��Ȱ��ȭ
    public void CloseConfirmDialogUI() {
        this.gameObject.SetActive(false);
        returnFailDialogUI.SetActive(false);
        ConfirmDIalogUI.SetActive(false);
    }

    // Confirm���� Ȯ�� �� ��ų�� �����ϴ� �Լ�
    public void BuySkill() {
        // ��ų ȹ�� ó��
        MainUIContainer.Instance.UpdateMoney(-skillSO.cost);
        SkillManager.Instance.AddNewSkill(skillSO);
        
        // UI �ݱ�
        CloseConfirmDialogUI();
        FullScreenUIManager.Instance.CloseFullScreenUI();
    }
}
