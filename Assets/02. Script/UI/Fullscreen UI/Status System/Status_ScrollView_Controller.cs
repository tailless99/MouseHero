using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Status_ScrollView_Controller : MonoBehaviour
{
    [Header("Scroll View")]
    public ScrollRect scrollRect; // 스크롤 뷰 오브젝트
    public RectTransform statusCardSlot; // 카드 슬롯의 크기를 위한 컴포넌트
    public HorizontalLayoutGroup horizontalLayoutGroup; // 스페이싱을 가져오기 위한 컴포넌트
    public float scrollSpeed; // 스크롤 되는 스피드
    
    private int currentIndex = 0; // 스크롤 인데스
    private float cardWidth; // 카드의 넓이
    private float cardSpacing; // 카드 간 간격
    private RectTransform contentRect; // 스크롤의 내용 컴포넌트 위치
    
    private float movePosX; // 새로 움직일 위치의 X 위치
    private bool isScrolling = false; // 버튼 클릭을 이용한 스크롤 실행여부

    
    private void OnEnable() {
        // 위치 초기화
        if(contentRect != null) contentRect.anchoredPosition = Vector2.zero;
    }

    private void Start() {
        // 방어 구문
        if (!scrollRect || !scrollRect.content) return;

        contentRect = scrollRect.content;

        // Content의 넓이 초기 셋팅
        int cardCount = contentRect.childCount;
        contentRect.sizeDelta = new Vector2((cardWidth + cardSpacing) * cardCount - cardSpacing, contentRect.sizeDelta.y);

        // Content 초기 위치 셋팅
        contentRect.anchoredPosition = Vector2.zero;

        // 카드 크기 설정
        cardWidth = statusCardSlot.sizeDelta.x;

        // 카드 간 간격
        cardSpacing = horizontalLayoutGroup.spacing;
    }

    private void Update() {
        if (isScrolling) {
            SmoothScrollContent();
        }
    }

    // 왼쪽 스크롤 버튼 클릭 이벤트
    public void ScrollLeft() {
        if (currentIndex == 0) return;

        currentIndex--;
        movePosX = -(cardWidth + cardSpacing) * currentIndex;
        isScrolling = true;
    }

    // 오른쪽 스크롤 버튼 클릭 이벤트
    public void ScrollRight() {
        if (currentIndex >= contentRect.childCount - 1) return;

        currentIndex++;
        movePosX = -(cardWidth + cardSpacing) * currentIndex;
        isScrolling = true;
    }

    // 스크롤바 이동 함수
    public void SmoothScrollContent() {
        StartCoroutine(SmoothScrollCoroutine());
    }

    private IEnumerator SmoothScrollCoroutine() {
        float startTime = Time.unscaledTime;
        Vector2 startPosition = contentRect.anchoredPosition;
        Vector2 targetPosition = new Vector2(movePosX, 0f);
        float duration = Mathf.Abs(targetPosition.x - startPosition.x) / (scrollSpeed * (cardWidth + cardSpacing));

        while (Time.unscaledTime - startTime < duration) {
            float t = Mathf.Clamp01((Time.unscaledTime - startTime) / duration);
            contentRect.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        contentRect.anchoredPosition = targetPosition;
        isScrolling = false;
    }
}
