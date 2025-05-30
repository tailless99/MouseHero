using UnityEngine;

public class StatusSystemContainer : MonoBehaviour
{
    public void OpenStatusUI() {
        FullScreenUIManager.Instance.OpenStatusFullScreenUI();
    }
}
