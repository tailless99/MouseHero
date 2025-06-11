using UnityEngine;

public class LevelUpEffectContainer : MonoBehaviour
{
    [SerializeField] private GameObject levelUpEffectWindo;
    [SerializeField] private GameObject levelUpRewardShop;


    // ������ UI�� �������� �Լ�
    public void OpenRewardUI() {
        this.gameObject.SetActive(true);
        levelUpEffectWindo.SetActive(false);
        levelUpRewardShop.SetActive(true);
    }

    // Ȱ��/��Ȱ��ȭ ��� �Լ�
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
        levelUpEffectWindo.SetActive(true);
        levelUpRewardShop.SetActive(false);
    }
}
