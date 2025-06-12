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
        // UI ��Ȱ��ȭ
        ToggleActiveObject(false);
    }


    public void Initialize_DetaillView(SkillSO skill) {
        skillSO = skill;
        this.icon.sprite = skillSO.skillIcon;
        this.skillName.text = skillSO.skillName;
        this.detaill.text = skillSO.detailDescription;
        this.formula.text = skillSO.formula;
        this.cost.text = skillSO.cost.ToString();
        
        // Ȱ��ȭ
        ToggleActiveObject(true);
    }

    // UI ��ҵ��� Ȱ��/��Ȱ��ȭ
    public void ToggleActiveObject(bool setActive) {
        icon.gameObject.SetActive(setActive);
        skillName.gameObject.SetActive(setActive);
        detaill.gameObject.SetActive(setActive);
        formula.gameObject.SetActive(setActive);
        cost.gameObject.SetActive(setActive);
        btnArea.gameObject.SetActive(setActive);
    }

    // ���� ��ų�� ��ũ��Ʈ ����� ��ȯ
    public SkillSO GetCurrentSkillSO() => skillSO;
}
