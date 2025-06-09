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
        // 초기화
        level = MainUIContainer.Instance.GetLevel();
        currentLevelText.text = (level - 1).ToString();
        addLevelText.text = level.ToString();
    }

    // 레벨 텍스트 애니메이션 실행
    public void OnLevelTextChanged() {
        animator.SetTrigger("LevelChanged");
    }

    // UI 종료를 위한 버튼 활성화
    public void OnAnimationEnd() {
        closeGuideText.SetActive(true);
        nextButton.SetActive(true);
    }
    
    // 비활성화시 초기화
    private void OnDisable() {
        closeGuideText.SetActive(false);
        nextButton.SetActive(false);
    }
}
