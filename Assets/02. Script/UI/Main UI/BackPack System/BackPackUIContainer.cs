using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackPackUIContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;

    private void OnEnable() {
        countText.text = SkillManager.Instance.GetHasSkillList().Count.ToString();
    }

    // Ȱ��/��Ȱ��ȭ ��� �Լ�
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }
}
