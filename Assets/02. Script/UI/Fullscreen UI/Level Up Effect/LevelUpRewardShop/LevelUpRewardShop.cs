using UnityEngine;

public class LevelUpRewardShop : MonoBehaviour
{
    [SerializeField] private float playerHealingPoint = 30f; // �÷��̾� ȸ����
    
    // ī�� �� ���� ī���� ��ư �̺�Ʈ
    public void OpenCardShopUI() {
        // ���ӿ�����Ʈ �ʱ�ȭ
        this.transform.parent.gameObject.GetComponent<LevelUpEffectContainer>().ToggleActive();
        FullScreenUIManager.Instance.CloseFullScreenUI(); // ���� �ð��� �ٽ� ���ư��� �ϰ�
        FullScreenUIManager.Instance.OpenSkillShopUI(); // ��ų ���� ���� �ٽ� �ð��� �����.
    }

    // �÷��̾� ȸ�� ī���� ��ư �̺�Ʈ
    public void PlayerHealing() {
        // ���ӿ�����Ʈ �ʱ�ȭ
        this.transform.parent.gameObject.GetComponent<LevelUpEffectContainer>().ToggleActive();
        PlayerStatusManager.Instance.PlayerHealing(playerHealingPoint);
        FullScreenUIManager.Instance.CloseFullScreenUI();
    }
}
