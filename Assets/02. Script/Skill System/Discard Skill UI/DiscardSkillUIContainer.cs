using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardSkillUIContainer : MonoBehaviour
{
    [SerializeField] DiscardSkillDetail_UI discardSkillDetail_UI;
    [SerializeField] DiscardConfirm discardConfirm;
    [SerializeField] private TextMeshProUGUI countText;

    private void OnEnable() {
        // 이 오브젝트가 활성화 될 때 초기화 셋팅
        discardSkillDetail_UI.gameObject.SetActive(false);
        discardConfirm.gameObject.SetActive(false);

        countText.text = SkillManager.Instance.GetHasSkillList().Count.ToString();
    }

    // 스킬 카드 클릭 시, 디테일 슬롯 출력
    public void ShowSelectSkillDetaillUI(SkillCardSlot cardBase) {
        discardSkillDetail_UI.gameObject.SetActive(true);
        discardSkillDetail_UI.Initialize_CardSlotDetail(cardBase.skillSO);
    }

    // 뒤로가기 버튼 클릭시 실행 함수
    public void ReturnPrevView() {
        this.gameObject.SetActive(false);
    }

    // 모든 작업이 종료됬을 때, 모든 UI를 닫는 함수
    public void CloseDiscardSkillUI() {
        FullScreenUIManager.Instance.CloseFullScreenUI();
        this.gameObject.SetActive(false);
    }

    // 확인창 활성/비활성 함수
    public void ToggleActiveConfirmUI() {
        discardConfirm.gameObject.SetActive(!discardConfirm.gameObject.activeSelf);
    }

    // 스킬 삭제하는 함수
    public void DeleteSkillCard() {
        SkillManager.Instance.DeleteHasSkill(discardSkillDetail_UI.GetCurrentSkillSO());
        CloseDiscardSkillUI();
    }
}
