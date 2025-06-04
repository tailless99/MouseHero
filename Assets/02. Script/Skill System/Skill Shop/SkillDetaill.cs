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


    private void OnEnable() {
        // UI 비활성화
        ToggleActiveObject(false);
    }


    public void Initialize_DetaillView(Sprite icon, string skillName, string detaill, string formula, int cost) {
        this.icon.sprite = icon;
        this.skillName.text = skillName;
        this.detaill.text = detaill;
        this.formula.text = formula;
        this.cost.text = cost.ToString();
        
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
}
