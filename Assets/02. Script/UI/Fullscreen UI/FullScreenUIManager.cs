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
    /// ��� Ǯ��ũ�� UI Ŭ������ ��Ȱ��ȭ
    /// </summary>
    private void DeActiveAllFullScreenUI() {
        statusFullScreenUIContainer.gameObject.SetActive(false);
        skillShopUIContainer.gameObject.SetActive(false);
        backPackUIContainer.gameObject.SetActive(false);
    }
    
    /// <summary>
    /// ���� �ð��� �ٽ� ������, ��� �並 ��Ȱ��ȭ�ϵ��� ������� �Լ�
    /// </summary>
    public void CloseFullScreenUI() {
        ToggleGamePause();
        DeActiveAllFullScreenUI();
    }

    /// <summary>
    /// ������ ���߰ų� �ٽ� ����ϴ� ���
    /// </summary>
    private void ToggleGamePause() {
        isPaused = !isPaused; // ���� ������ �ݴ� ������ ����
        Time.timeScale = isPaused ? 0f : 1f;
    }

    // UI ���� �Լ�
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
