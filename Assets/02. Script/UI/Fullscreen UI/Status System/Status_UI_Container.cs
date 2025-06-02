using TMPro;
using UnityEngine;

public class Status_UI_Container : MonoBehaviour
{
    [Header("GameObject")]
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI totalCostText;
    public GameObject confirmView; // Confirm View
    public GameObject scrollContent; // 스크롤 뷰의 컨텐트 영역 오브젝트

    [Header("Data")]
    public int statusCost = 15;

    // 내장 함수
    private int totalCost = 0;


    private void Start() {
        // 초기화
        moneyText.text = MainUIContainer.Instance.GetCurrentMoney().ToString();
        totalCostText.text = totalCost.ToString();
    }

    /// <summary>
    /// 소지금과 소모금을 비교해서 소모금이 더 크다면 false를 반환,
    /// 소지금이 더 크다면 true를 반환하며 total Cost의 값을 증가하는 로직을 실행.
    /// </summary>
    /// <returns></returns>
    public bool StatusCardAddBtnClick() {
        // 소지금 보다 소모골드가 크다면
        bool result = MainUIContainer.Instance.CanUseMoney(totalCost + statusCost);
        if (!result) return result;

        // 소지금이 더 클 경우 로직 실행
        AddTotalCost();
        UpdateTotalCostText();

        return true;
    }

    public bool StatusCardMinusBtnClick() {
        // 토탈 코스트가 소모 코스트보다 작은 경우
        if (totalCost < statusCost) return false;

        // 토탈 코스트 감소 로직 실행
        totalCost -= statusCost;
        UpdateTotalCostText();

        return true;
    }

    // 코스트 연산 처리
    private void AddTotalCost() => totalCost += statusCost;

    // 총 코스트 UI의 텍스트 업데이트
    private void UpdateTotalCostText() => totalCostText.text = totalCost.ToString();

    // 활성/비활성화 토글 함수
    public void ToggleActive() {
        bool isActive = this.gameObject.activeSelf;
        this.gameObject.SetActive(!isActive);
    }

    // 확인창 활성/비활성화 토글
    public void OpenConfrimUI() {
        if (totalCost == 0) return;

        bool result = confirmView.gameObject.activeSelf;
        confirmView.gameObject.SetActive(!result);
    }

    // 카드마다 추가 스텟만큼 플레이어 스텟 증가
    public void UpdatePlayerStatus() {
        Transform contentTransform = scrollContent.transform;
        int childCount = contentTransform.childCount;

        for (int i = 0; i < childCount; i++) {
            Transform childTransform = contentTransform.GetChild(i);
            var childObject = childTransform.gameObject.GetComponent<StatusCardSlot>();
            childObject.UpdatePlayerStatus();
            childObject.UpdateStatusNumber();
        }
        
        // 후처리
        OpenConfrimUI(); // 확인창 닫기
        MainUIContainer.Instance.UpdateMoney(-totalCost); // 소지금 소모 반영
        moneyText.text = MainUIContainer.Instance.GetCurrentMoney().ToString(); // 소지금 소모 반영
        totalCost = 0;
        UpdateTotalCostText(); // 토탈 코스트 초기화
    }
}
