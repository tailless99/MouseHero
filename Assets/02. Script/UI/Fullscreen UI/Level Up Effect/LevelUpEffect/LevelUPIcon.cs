using TMPro;
using UnityEngine;

public class LevelUPIcon : MonoBehaviour
{
    [SerializeField] private GameObject closeGuideText;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI addLevelText;

    private Animator animator;
    private int level;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        // �ʱ�ȭ
        level = MainUIContainer.Instance.GetLevel();
        currentLevelText.text = (level - 1).ToString();
        addLevelText.text = level.ToString();
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
        closeGuideText.SetActive(false);
        nextButton.SetActive(false);
    }
}
