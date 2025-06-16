using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillStackContainer : MonoBehaviour
{
    //[SerializeField] private List<SkillStackSlot> slots;
    [SerializeField] private List<GameObject> slots;
    [SerializeField] int addSkillCost;

    /// <summary>
    /// 스킬 스택 슬롯이 사용중인지를 체크하고, 결과 여부에 따라 슬롯에 새로운 스킬을 대입하는 함수
    /// 슬롯이 전부 사용중인 경우 : false를 반환
    /// 슬롯이 전부 사용중이지 않은 경우 : 해당 슬롯에 새로 드로우한 스킬을 삽입하고, true 반환
    /// </summary>
    //public bool FindDeActiveSlot() {
    //    foreach(var slot in slots) {
    //        if(!slot.IsUsedSlot()) {
    //            var skill = SkillManager.Instance.GetHasSkill();
    //            slot.SlotSetting(skill);
    //            return true;
    //        }
    //    }
    //    return false;
    //}
    public bool FindDeActiveSlot() {
        foreach (var slot in slots) {
            if (!slot.gameObject.activeSelf) { // 사용중이 아닐 때
                slot.gameObject.SetActive(true);

                var ImgObject = slot.GetComponentInChildren<SkillStackSlot>();
                var skill = SkillManager.Instance.GetHasSkill();
                ImgObject.SlotSetting(skill);
                return true;
            }
        }
        return false;
    }

    // 스킬 카드를 추가한다.
    public void AddSkillCard() {
        // 비용이 충분히 있는지 체크
        if (!MainUIContainer.Instance.CanUseMoney(addSkillCost)) return;
        
        // 스킬 추가
        if (!FindDeActiveSlot()) return;
        
        // 스킬 추가 완료 후 소지금 차감
        MainUIContainer.Instance.UpdateMoney(-addSkillCost);
    }

    // 새로운 스킬 카드 드로우
    public void DrawNewSkillCard() {
        StartCoroutine(DrawSkill());
    }

    // 슬롯이 빈 곳이 생길 때까지 대기 후 카드 드로우
    private IEnumerator DrawSkill() {
        bool IsCanAddSkillCard = false;

        foreach (var slot in slots) {
            if (!slot.gameObject.activeSelf) { // 사용중이 아닐 때
                IsCanAddSkillCard = true;
                break;
            }
        }

        // 스킬을 추가할 수 있을 때까지 기다린다.
        yield return new WaitUntil(() => IsCanAddSkillCard);

        // 스킬 추가
        FindDeActiveSlot();
    }

    /// <summary>
    /// 슬롯에 삽입된 스킬을 사용하는 함수
    /// Params : 0 - 첫번 째 슬롯의 스킬 사용
    /// 1 - 두번 째 슬롯의 스킬 사용
    /// 2 - 세번 째 슬롯의 스킬 사용
    /// </summary>
    public void UseSkillCard(int index) {
        switch (index) {
            case 0:
                slots[0].GetComponentInChildren<SkillStackSlot>().UseSkillCard();
                break;
            case 1:
                slots[1].GetComponentInChildren<SkillStackSlot>().UseSkillCard();
                break;
            case 2:
                slots[2].GetComponentInChildren<SkillStackSlot>().UseSkillCard();
                break;
        }
    }

    public void SkillStackReset() {
        foreach(var skill in slots){
            skill.gameObject.SetActive(false);
        }
    }
}
