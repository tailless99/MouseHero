using UnityEngine;

public class SkillShopUIContainer : MonoBehaviour
{
    [SerializeField] SkillDetaill skillDetaill;
    [SerializeField] SkillShopConfirm skillShopConfirm;

    // 스킬의 디테일한 설명 정보 바인딩
    public void ShowDetailUI(SkillCardSlot cardBase) {
        var data = cardBase.skillSO;
        skillDetaill.Initialize_DetaillView(data);
    }

    // Buy Button Click Func
    public void OpenConfirmDialog() {
        skillShopConfirm.gameObject.SetActive(true);
        skillShopConfirm.ShowConfirmDialog(skillDetaill.GetCurrentSkillSO());
    }

    // 활성/비활성화 토글 함수
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }
}
