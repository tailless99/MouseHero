using UnityEngine;

public class FullScreenUIManager : Singleton<FullScreenUIManager>
{
    [SerializeField] private Status_UI_Container statusFullScreenUIContainer;

    private bool isPaused = false;

    protected override void Awake() {
        base.Awake();
        DeActiveAllFullScreenUI();
    }

    /// <summary>
    /// ��� Ǯ��ũ�� UI Ŭ������ ��Ȱ��ȭ
    /// </summary>
    private void DeActiveAllFullScreenUI() {
        //statusFullScreenUIContainer.gameObject.SetActive(false);
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

#endregion
}
