using UnityEngine;

public class LevelUpRewardShop : MonoBehaviour
{
    [SerializeField] private float playerHealingPoint = 30f; // 플레이어 회복량
    
    // 카드 샵 오픈 카드의 버튼 이벤트
    public void OpenCardShopUI() {
        // 게임오브젝트 초기화
        this.transform.parent.gameObject.GetComponent<LevelUpEffectContainer>().ToggleActive();
        FullScreenUIManager.Instance.CloseFullScreenUI(); // 멈춘 시간을 다시 돌아가게 하고
        FullScreenUIManager.Instance.OpenSkillShopUI(); // 스킬 샵을 열때 다시 시간이 멈춘다.
    }

    // 플레이어 회복 카드의 버튼 이벤트
    public void PlayerHealing() {
        // 게임오브젝트 초기화
        this.transform.parent.gameObject.GetComponent<LevelUpEffectContainer>().ToggleActive();
        PlayerStatusManager.Instance.PlayerHealing(playerHealingPoint);
        FullScreenUIManager.Instance.CloseFullScreenUI();
    }
}
