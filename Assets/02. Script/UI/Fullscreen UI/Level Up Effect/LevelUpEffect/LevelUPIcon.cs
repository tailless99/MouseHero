using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelUPIcon : MonoBehaviour {
    [SerializeField] private GameObject closeGuideText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI addLevelText;

    private Animator animator;
    private int level;
    private Vector3 currentTextPos;
    private Vector3 AddTextPos;

    private void Awake() {
        animator = GetComponent<Animator>();
        currentTextPos = currentLevelText.gameObject.transform.position;
        AddTextPos = addLevelText.gameObject.transform.position;
    }

    private void OnEnable() {
        // �ʱ�ȭ
        level = MainUIContainer.Instance.GetLevel();

        currentLevelText.text = (level - 1).ToString().Replace("\u200b", "");
        addLevelText.text = level.ToString().Replace("\u200b", "");
    }

    // ���� �ؽ�Ʈ �ִϸ��̼� ����
    public void OnLevelTextChanged() {
        animator.SetTrigger("LevelChanged");
    }

    // UI ���Ḧ ���� ��ư Ȱ��ȭ
    public void OnAnimationEnd() {
        closeGuideText.SetActive(true);
        nextButton.SetActive(true);
    }
    
    // ��Ȱ��ȭ�� �ʱ�ȭ
    private void OnDisable() {
        currentLevelText.transform.position = currentTextPos;
        currentLevelText.color = new Color(currentLevelText.color.r, currentLevelText.color.g, currentLevelText.color.b, 1f);
        addLevelText.transform.position = AddTextPos;
        addLevelText.color = new Color(addLevelText.color.r, addLevelText.color.g, addLevelText.color.b, 0f);

        closeGuideText.SetActive(false);
        nextButton.SetActive(false);
    }
}
