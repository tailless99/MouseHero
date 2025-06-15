using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SkillStackContainer : MonoBehaviour
{
    //[SerializeField] private List<SkillStackSlot> slots;
    [SerializeField] private List<GameObject> slots;
    [SerializeField] int addSkillCost;

    /// <summary>
    /// ��ų ���� ������ ����������� üũ�ϰ�, ��� ���ο� ���� ���Կ� ���ο� ��ų�� �����ϴ� �Լ�
    /// ������ ���� ������� ��� : false�� ��ȯ
    /// ������ ���� ��������� ���� ��� : �ش� ���Կ� ���� ��ο��� ��ų�� �����ϰ�, true ��ȯ
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
            if (!slot.gameObject.activeSelf) { // ������� �ƴ� ��
                slot.gameObject.SetActive(true);

                var ImgObject = slot.GetComponentInChildren<SkillStackSlot>();
                var skill = SkillManager.Instance.GetHasSkill();
                ImgObject.SlotSetting(skill);
                return true;
            }
        }
        return false;
    }

    // ��ų ī�带 �߰��Ѵ�.
    public void AddSkillCard() {
        // ����� ����� �ִ��� üũ
        if (!MainUIContainer.Instance.CanUseMoney(addSkillCost)) return;
        
        // ��ų �߰�
        if (!FindDeActiveSlot()) return;
        
        // ��ų �߰� �Ϸ� �� ������ ����
        MainUIContainer.Instance.UpdateMoney(-addSkillCost);
    }

    /// <summary>
    /// ���Կ� ���Ե� ��ų�� ����ϴ� �Լ�
    /// Params : 0 - ù�� ° ������ ��ų ���
    /// 1 - �ι� ° ������ ��ų ���
    /// 2 - ���� ° ������ ��ų ���
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
}
