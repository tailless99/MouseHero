using UnityEngine;
using UnityEngine.InputSystem;

public class MainUIContainer : Singleton<MainUIContainer> {
    [Header("Main UI Class")]
    [SerializeField] private PlayerHPSliderContainer hpSlider;
    [SerializeField] public MoneyContainer moneyContainer;

    [Header("FullScreen UI Class")]
    [SerializeField] public FullScreenUIManager fullScreenUIManager;

    // UI 입력 관리 클래스
    private Player_Input_Action playerInput;


    protected override void Awake() {
        base.Awake();
        playerInput = new Player_Input_Action();
    }

    private void Start() {
        // Input system Button 셋팅
        playerInput.Enable();

        // 입력 버튼 바인딩
        playerInput.OpenUI.Status.performed += _ => OpenStatusUI();
        playerInput.OpenUI.Inventory.performed += _ => OpenBackPack();
    }

// 오버레이 캔버스 UI 함수 모음
#region
    // HP 갱신
    public void UpdateHpPercent(float hpPercent) => hpSlider.SetHp(hpPercent);

    // 돈 갱신
    public void UpdateMoney(int money) => moneyContainer.UpdateMoney(money);
    
    // 현재 소지금 반환
    public int GetCurrentMoney() => moneyContainer.GetCurrentMoney();

    // 소모 골드보다 많은 골드를 가지고 있는지 체크
    public bool CanUseMoney(int money) => moneyContainer.CanUseMoney(money);
#endregion

// 풀스크린 UI 오픈 함수 모음
#region
    private void OpenStatusUI() => fullScreenUIManager.OpenStatusFullScreenUI();
    private void OpenSkillSHop() => fullScreenUIManager.OpenSkillShopUI();
    private void OpenBackPack() => fullScreenUIManager.OpenBackPackUI();
#endregion
}
