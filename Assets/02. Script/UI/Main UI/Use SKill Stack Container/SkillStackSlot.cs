using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillStackSlot : MonoBehaviour
{
    [SerializeField] private Sprite emptyImg;

    private Image skillStackImg; // 변환할 이미지 컴포넌트
    [SerializeField] private SkillSO currentSkillObject; // 현재 삽입된 스킬 정보
    private bool isUseSlot;
    private bool isStartedSkillLogic;

    private void Awake() {
        skillStackImg = GetComponent<Image>();
    }

    private void OnDisable() {
        // 초기화
        currentSkillObject = null;
        skillStackImg.sprite = emptyImg;
        isUseSlot = false;
    }

    /// <summary>
    /// 슬롯을 사용하기 위해 셋팅하는 함수
    /// </summary>
    /// <param name="imgSprite"></param>
    public void SlotSetting(SkillSO skill) {
        if (skill == null) return;

        currentSkillObject = skill;
        skillStackImg.sprite = currentSkillObject.skillIcon;
        isUseSlot = true;
    }

    // 스킬 사용 코루틴을 시작하는 함수
    public void RequestUseSkillCard() {
        if (currentSkillObject?.skillLogicPrefab == null || isStartedSkillLogic) {
            return;
        }

        // UseSkillCard 코루틴 시작
        StartCoroutine(UseSkillCardCoroutine());
    }

    /// <summary>
    /// 등록된 스킬을 사용하고, 슬롯을 다시 초기화 시키는 함수
    /// </summary>
    public IEnumerator UseSkillCardCoroutine() {
        bool isSkillUsed = true; // 스킬 정상 사용 체크 함수

        // 스킬이 비동기 스킬인지 확인
        if (currentSkillObject.skillLogicPrefab.IsCoroutineSkill) {
            // 스킬 처리가 완료될 때까지 대기
            yield return StartCoroutine(currentSkillObject.skillLogicPrefab.UseCoroutine(result => { isSkillUsed = result; }));
        }
        else {
            isSkillUsed = (bool)currentSkillObject?.skillLogicPrefab?.Use(); // 스킬의 발동 로직 실행
        }

        // 만약 스킬을 실행할 수 없는 상태라면 반환
        if (isSkillUsed == false) yield break;

        // 스킬 카드 사용 연출
        var parent = transform.parent.GetComponent<UseSkillSlot>();
        parent?.fade();
    }

    /// <summary>
    /// 슬롯을 사용중인지 여부를 반환하는 함수
    /// </summary>
    /// <returns></returns>
    public bool IsUsedSlot() => isUseSlot;
}
