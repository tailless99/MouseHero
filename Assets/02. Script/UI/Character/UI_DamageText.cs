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
        // ������ �ð��� �Ǿ��ٸ� ��Ȱ��ȭ
        if(endTime <= 0f) this.gameObject.SetActive(false);

        // ������ �ؽ�Ʈ�� ���� �̵�
        this.transform.position += Vector3.up * moveUpSpeed * Time.deltaTime;

        // �ð� ����
        endTime -= Time.deltaTime;
    }
}
