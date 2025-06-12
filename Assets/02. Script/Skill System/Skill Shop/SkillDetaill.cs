using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillDetaill : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI skillName;
    [SerializeField] TextMeshProUGUI detaill;
    [SerializeField] TextMeshProUGUI formula;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] GameObject btnArea;

    private SkillSO skillSO;

    private void OnEnable() {
        // UI 비활성화
        ToggleActiveObject(false);
    }


    public void Initialize_DetaillView(SkillSO skill) {
        skillSO = skill;
        this.icon.sprite = skillSO.skillIcon;
        this.skillName.text = skillSO.skillName;
        this.detaill.text = skillSO.detailDescription;
        this.formula.text = skillSO.formula;
        this.cost.text = skillSO.cost.ToString();
        
        // 활성화
        ToggleActiveObject(true);
    }

    // UI 요소들의 활성/비활성화
    public void ToggleActiveObject(bool setActive) {
        icon.gameObject.SetActive(setActive);
        skillName.gameObject.SetActive(setActive);
        detaill.gameObject.SetActive(setActive);
        formula.gameObject.SetActive(setActive);
        cost.gameObject.SetActive(setActive);
        btnArea.gameObject.SetActive(setActive);
    }

    // 현재 스킬의 스크립트 어블을 반환
    public SkillSO GetCurrentSkillSO() => skillSO;
}
