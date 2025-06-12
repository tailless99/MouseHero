using UnityEngine;

public class SkillShopUIContainer : MonoBehaviour
{
    [SerializeField] SkillDetaill skillDetaill;
    [SerializeField] SkillShopConfirm skillShopConfirm;

    // ��ų�� �������� ���� ���� ���ε�
    public void ShowDetailUI(SkillCardSlot cardBase) {
        var data = cardBase.skillSO;
        skillDetaill.Initialize_DetaillView(data);
    }

    // Buy Button Click Func
    public void OpenConfirmDialog() {
        skillShopConfirm.gameObject.SetActive(true);
        skillShopConfirm.ShowConfirmDialog(skillDetaill.GetCurrentSkillSO());
    }

    // Ȱ��/��Ȱ��ȭ ��� �Լ�
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }
}
