using UnityEngine;

public class LevelUpEffectContainer : MonoBehaviour
{
    [SerializeField] private GameObject levelUpEffectWindo;
    [SerializeField] private GameObject levelUpRewardShop;


    // 리워드 UI를 열기위한 함수
    public void OpenRewardUI() {
        this.gameObject.SetActive(true);
        levelUpEffectWindo.SetActive(false);
        levelUpRewardShop.SetActive(true);
    }

    // 활성/비활성화 토글 함수
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
        levelUpEffectWindo.SetActive(true);
        levelUpRewardShop.SetActive(false);
    }
}
