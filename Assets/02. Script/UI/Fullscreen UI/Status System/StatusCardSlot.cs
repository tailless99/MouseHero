using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class StatusCardSlot : MonoBehaviour
{
    [Header("Status Controller")]
    public Status_UI_Container statusController;

    [Header("UI Setting")]
    public TextMeshProUGUI statusName;      // 스텟 이름
    public TextMeshProUGUI statusNumber;    // 스텟 수치(숫자)
    public TextMeshProUGUI addStatNumber;    // 추가될 스텟 수치(숫자)
    public StatusType type;                 // 스텟 종류
    
    // 내장 멤버 변수
    private int currentAddStat = 0;

    private void Start() {
        statusName.text = type.ToString();
    }

    private void OnEnable() {
        UpdateStatusNumber();  // 현재 스텟포인트 갱신
        addStatNumber.text = currentAddStat.ToString();
    }

    // 버튼 클릭 이벤트
    public void AddStatus() {
        // 소지금보다 소모 골드가 더 크다면 종료
        var result = statusController.StatusCardAddBtnClick();
        if (!result) return;

        // 증감 연산
        currentAddStat++;
        UpdateAddStatusText();
    }

    // 버튼 클릭 이벤트
    public void MinusStatus() {
        var result = statusController.StatusCardMinusBtnClick();
        if (currentAddStat <= 0 || !result) return;

        // 증감 연산
        currentAddStat--;
        UpdateAddStatusText();
    }

    // 추가할 스테이터스 포인트 갱신
    public void UpdateAddStatusText() {
        addStatNumber.text = currentAddStat.ToString();
    }

    // 현재 스테이터스 수치 업데이트
    public void UpdateStatusNumber() {
        currentAddStat = 0;
        statusNumber.text = PlayerStatusManager.Instance.GetStatus(type).ToString();
    }

    // 플레이어의 스텟을 추가 스텟만큼 증가시킨다.
    public void UpdatePlayerStatus() {
        if (currentAddStat == 0) return;
        PlayerStatusManager.Instance.AddStatus(type, currentAddStat);
    }
}
