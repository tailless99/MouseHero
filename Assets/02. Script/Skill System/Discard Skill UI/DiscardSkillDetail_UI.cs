using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiscardSkillDetail_UI : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI detaill;
    
    private SkillSO skillSO;

    public void Initialize_CardSlotDetail(SkillSO skill) {
        skillSO = skill;
        this.icon.sprite = skillSO.skillIcon;
        this.skillName.text = skillSO.skillName;
        this.detaill.text = skillSO.detailDescription;
    }

    // 현재 스킬의 스크립트 어블을 반환
    public SkillSO GetCurrentSkillSO() => skillSO;
}
