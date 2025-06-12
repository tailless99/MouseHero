using UnityEngine;

public class SkillShopConfirm : MonoBehaviour
{
    [SerializeField] private GameObject returnFailDialogUI;
    [SerializeField] private GameObject ConfirmDIalogUI;

    private SkillSO skillSO;

    // Buy 버튼을 통해 호출
    // 구매 가능 여부에 따라 열리는 Dialog를 구분해서 활성화
    public void ShowConfirmDialog(SkillSO skill) {
        skillSO = skill;
        var CanUseMoney = MainUIContainer.Instance.CanUseMoney(skillSO.cost); // 돈이 충분한가
        var CanAddSkill = SkillManager.Instance.CanAddNewSkill(); // 스킬 소지 갯수에 여유가 있는지

        // 구매할 수 있는지 여부에 따라 열리는 뷰가 달라짐
        if (CanUseMoney && CanAddSkill) ConfirmDIalogUI.SetActive(true);
        else returnFailDialogUI.SetActive(true);
    }

    // 모든 다이아 로그 창을 비활성화
    public void CloseConfirmDialogUI() {
        this.gameObject.SetActive(false);
        returnFailDialogUI.SetActive(false);
        ConfirmDIalogUI.SetActive(false);
    }

    // Confirm에서 확인 후 스킬을 구입하는 함수
    public void BuySkill() {
        // 스킬 획득 처리
        MainUIContainer.Instance.UpdateMoney(-skillSO.cost);
        SkillManager.Instance.AddNewSkill(skillSO);
        
        // UI 닫기
        CloseConfirmDialogUI();
        FullScreenUIManager.Instance.CloseFullScreenUI();
    }
}
