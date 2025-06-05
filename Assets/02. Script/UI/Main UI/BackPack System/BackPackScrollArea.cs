using UnityEngine;

public class BackPackScrollArea : MonoBehaviour
{
    [SerializeField] private GameObject scrollContents;

    private void OnEnable() {
        ShopSkillScrollAreaSetting();
    }

    private void ShopSkillScrollAreaSetting() {
        int childCount = scrollContents.transform.childCount;
        Transform[] cardSlots = new Transform[childCount];

        // 스크롤 영역의 자식들 전부 비활성화
        for (int i = 0; i < childCount; i++) {
            cardSlots[i] = scrollContents.transform.GetChild(i);
            cardSlots[i].gameObject.SetActive(false);
        }

        var skillList = SkillManager.Instance.GetHasSkillList();
        int index = 0;

        // 소지 스킬 카드 숫자만큼만 활성화 및 정보 초기화
        foreach (var skill in skillList) {
            cardSlots[index].gameObject.SetActive(true);
            cardSlots[index].TryGetComponent<SkillCardSlot>(out SkillCardSlot cardSlot);
            cardSlot.Initialize_Slot(skill);
            index++;
        }
        Debug.Log("인벤토리");
    }
}
