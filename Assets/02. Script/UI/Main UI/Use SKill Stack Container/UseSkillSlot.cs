using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UseSkillSlot : MonoBehaviour
{
    [SerializeField] private Image frame;
    [SerializeField] private Image skillIcon;
    [SerializeField] private float fadeTime;
    [SerializeField] private float upMovingSpeed;


    private Rigidbody2D rb;
    private Vector3 startPos;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    private void OnEnable() {
        // 초기화
        transform.position = startPos;
        ImageAlphaChange(1f);
    }

    // 스킬 사용 연출
    public  void fade() {
        rb.linearVelocityY = upMovingSpeed;
        StartCoroutine(FadeCoroutine(fadeTime));
    }

    private IEnumerator FadeCoroutine(float fadeTime) {
        float time = fadeTime;
        float percent = 1;
        
        while(percent > 0) {
            Debug.Log("percent " + percent);
            time -= Time.deltaTime;
            percent = time / fadeTime;
            ImageAlphaChange(percent);
            yield return null;
        }
        
        // 페이드가 끝났다면 비활성화
        this.gameObject.SetActive(false);
    }

    // 자식 오브젝트의 알파값을 조절
    private void ImageAlphaChange(float alpha) {
        frame.color = new Color(frame.color.r, frame.color.g, frame.color.b, alpha);
        skillIcon.color = new Color(skillIcon.color.r, skillIcon.color.g, skillIcon.color.b, alpha);
    }
}
