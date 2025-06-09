using UnityEngine;

public class LevelUpEffectContainer : MonoBehaviour
{
    [SerializeField] private GameObject levelUpEffectWindo;
    [SerializeField] private GameObject levelUpRewardShop;


    public void OnClickNextButton() {
        levelUpEffectWindo.SetActive(false);
        levelUpRewardShop.SetActive(true);
    }

    // 활성/비활성화 토글 함수
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }
}
