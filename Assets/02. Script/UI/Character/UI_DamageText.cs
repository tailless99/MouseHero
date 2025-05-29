using UnityEngine;

public class UI_DamageText : MonoBehaviour
{
    [SerializeField] private float moveUpSpeed = 1f;
    [SerializeField] private float destroyTime = 1f;

    float endTime = 0f;

    private void OnEnable() {
        endTime = destroyTime;
    }

    private void Update() {
        // 삭제할 시간이 되었다면 비활성화
        if(endTime <= 0f) this.gameObject.SetActive(false);

        // 데미지 텍스트가 위로 이동
        this.transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        // 시간 감소
        endTime -= Time.deltaTime;
    }
}
