using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCardSlot : MonoBehaviour
{
    [HideInInspector]
    public SkillSO skillSO;
    public TextMeshProUGUI titleText;
    public Image Icon;
    public TextMeshProUGUI description;

    // ī�� ���� �ʱ�ȭ
    public void Initialize_Slot(SkillSO skillSO) {
        this.skillSO = skillSO;

        titleText.text = skillSO.skillName;
        Icon.sprite = skillSO.skillIcon;
        description.text = skillSO.description;
    }
}
