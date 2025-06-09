using UnityEngine;

public class LevelUpEffectContainer : MonoBehaviour
{
    [SerializeField] private GameObject levelUpEffectWindo;
    [SerializeField] private GameObject levelUpRewardShop;


    public void OnClickNextButton() {
        levelUpEffectWindo.SetActive(false);
        levelUpRewardShop.SetActive(true);
    }

    // Ȱ��/��Ȱ��ȭ ��� �Լ�
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }
}
