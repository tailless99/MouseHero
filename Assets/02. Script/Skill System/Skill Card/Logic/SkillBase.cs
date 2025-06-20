using System.Collections;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    // 통상적인 즉발 스킬 사용
    public abstract bool Use();

    /// <summary>
    /// 이 스킬이 코루틴(비동기) 방식으로 작동하는 스킬인지 여부를 나타내는 프로퍼티 
    /// 자식 클래스에서 override 하여 true/false를 설정
    /// </summary>
    public virtual bool IsCoroutineSkill => false; // 기본값은 false (동기 스킬)

    /// <summary>
    /// 비동기 스킬이 구현해야 할 코루틴 메서드
    /// IsCoroutineSkill이 true인 스킬만 이 메서드를 구현한다.
    /// </summary>
    public virtual IEnumerator UseCoroutine(System.Action<bool> onClick) {
        onClick?.Invoke(false); // 델리게이트 디폴트
        yield break; // return; 대신 사용하는 구문
    }
}
