using UnityEngine;
using UnityEngine.InputSystem;

public class MainUIContainer : Singleton<MainUIContainer> {
    [Header("Main UI Class")]
    [SerializeField] private PlayerHPSliderContainer hpSlider;
    [SerializeField] public MoneyContainer moneyContainer;
    [SerializeField] private UseSkillStackContainer useSkillStackContainer;
    [SerializeField] private PlayerEXPContainer playerEXPContainer;

    [Header("FullScreen UI Class")]
    [SerializeField] public FullScreenUIManager fullScreenUIManager;

    // UI �Է� ���� Ŭ����
    private Player_Input_Action playerInput;


    protected override void Awake() {
        base.Awake();
        playerInput = new Player_Input_Action();
    }

    private void Start() {
        // Input system Button ����
        playerInput.Enable();

        // �Է� ��ư ���ε�
        playerInput.OpenUI.Status.performed += _ => OpenStatusUI();
        playerInput.OpenUI.Inventory.performed += _ => OpenBackPack();
        playerInput.Skill.Use_SkillA.performed += _ => useSkillStackContainer.UseSkillCard(0);
        playerInput.Skill.Use_SkillB.performed += _ => useSkillStackContainer.UseSkillCard(1);
        playerInput.Skill.Use_SkillC.performed += _ => useSkillStackContainer.UseSkillCard(2);
        playerInput.Skill.AddSkill.performed += _ => useSkillStackContainer.AddSkillCard();
    }

// �������� ĵ���� UI �Լ� ����
#region
    // HP ����
    public void UpdateHpPercent(float hpPercent) => hpSlider.SetHp(hpPercent);

    // �� ����
    public void UpdateMoney(int money) => moneyContainer.UpdateMoney(money);
    
    // ���� ������ ��ȯ
    public int GetCurrentMoney() => moneyContainer.GetCurrentMoney();

    // �Ҹ� ��庸�� ���� ��带 ������ �ִ��� üũ
    public bool CanUseMoney(int money) => moneyContainer.CanUseMoney(money);

    // ����ġ ȹ��
    public void AddExp(float exp) => playerEXPContainer.AddExp(exp);

    // �÷��̾� ���� ��ȯ
    public int GetLevel() => playerEXPContainer.GetLevel();
#endregion

    // Ǯ��ũ�� UI ���� �Լ� ����
    #region
    private void OpenStatusUI() => fullScreenUIManager.OpenStatusFullScreenUI();
    private void OpenBackPack() => fullScreenUIManager.OpenBackPackUI();
    public void OpenLevelUpEvent() => fullScreenUIManager.OpenLevelUpEffectUI();
#endregion
}
