using UnityEngine;

public class FullScreenUIManager : Singleton<FullScreenUIManager>
{
    [SerializeField] private Status_UI_Container statusFullScreenUIContainer;
    [SerializeField] private SkillShopUIContainer skillShopUIContainer;
    [SerializeField] private BackPackUIContainer backPackUIContainer;

    private bool isPaused = false;

    protected override void Awake() {
        base.Awake();
        DeActiveAllFullScreenUI();
    }

    /// <summary>
    /// 모든 풀스크린 UI 클래스를 비활성화
    /// </summary>
    private void DeActiveAllFullScreenUI() {
        statusFullScreenUIContainer.gameObject.SetActive(false);
        skillShopUIContainer.gameObject.SetActive(false);
        backPackUIContainer.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// 멈춘 시간을 다시 돌리고, 모든 뷰를 비활성화하도록 묶어놓은 함수
    /// </summary>
    public void CloseFullScreenUI() {
        ToggleGamePause();
        DeActiveAllFullScreenUI();
    }

    /// <summary>
    /// 게임을 멈추거나 다시 재생하는 기능
    /// </summary>
    private void ToggleGamePause() {
        isPaused = !isPaused; // 현재 상태의 반대 값으로 변경
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // UI 오픈 함수
#region
    public void OpenStatusFullScreenUI() {
        ToggleGamePause();
        statusFullScreenUIContainer.ToggleActive();
    }

    public void OpenSkillShopUI() {
        ToggleGamePause();
        skillShopUIContainer.ToggleActive();
    }

    public void OpenBackPackUI() {
        ToggleGamePause();
        backPackUIContainer.ToggleActive();
    }
#endregion
}
