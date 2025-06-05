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

        // ��ũ�� ������ �ڽĵ� ���� ��Ȱ��ȭ
        for (int i = 0; i < childCount; i++) {
            cardSlots[i] = scrollContents.transform.GetChild(i);
            cardSlots[i].gameObject.SetActive(false);
        }

        var skillList = SkillManager.Instance.GetHasSkillList();
        int index = 0;

        // ���� ��ų ī�� ���ڸ�ŭ�� Ȱ��ȭ �� ���� �ʱ�ȭ
        foreach (var skill in skillList) {
            cardSlots[index].gameObject.SetActive(true);
            cardSlots[index].TryGetComponent<SkillCardSlot>(out SkillCardSlot cardSlot);
            cardSlot.Initialize_Slot(skill);
            index++;
        }
        Debug.Log("�κ��丮");
    }
}
