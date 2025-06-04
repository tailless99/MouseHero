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
        // UI ��Ȱ��ȭ
        ToggleActiveObject(false);
    }


    public void Initialize_DetaillView(Sprite icon, string skillName, string detaill, string formula, int cost) {
        this.icon.sprite = icon;
        this.skillName.text = skillName;
        this.detaill.text = detaill;
        this.formula.text = formula;
        this.cost.text = cost.ToString();
        
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
}
