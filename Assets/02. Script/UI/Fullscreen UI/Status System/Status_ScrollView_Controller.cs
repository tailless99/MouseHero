using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Status_ScrollView_Controller : MonoBehaviour
{
    [Header("Scroll View")]
    public ScrollRect scrollRect; // ��ũ�� �� ������Ʈ
    public RectTransform statusCardSlot; // ī�� ������ ũ�⸦ ���� ������Ʈ
    public HorizontalLayoutGroup horizontalLayoutGroup; // �����̽��� �������� ���� ������Ʈ
    public float scrollSpeed; // ��ũ�� �Ǵ� ���ǵ�
    
    private int currentIndex = 0; // ��ũ�� �ε���
    private float cardWidth; // ī���� ����
    private float cardSpacing; // ī�� �� ����
    private RectTransform contentRect; // ��ũ���� ���� ������Ʈ ��ġ
    
    private float movePosX; // ���� ������ ��ġ�� X ��ġ
    private bool isScrolling = false; // ��ư Ŭ���� �̿��� ��ũ�� ���࿩��

    
    private void OnEnable() {
        // ��ġ �ʱ�ȭ
        if(contentRect != null) contentRect.anchoredPosition = Vector2.zero;
    }

    private void Start() {
        // ��� ����
        if (!scrollRect || !scrollRect.content) return;

        contentRect = scrollRect.content;

        // Content�� ���� �ʱ� ����
        int cardCount = contentRect.childCount;
        contentRect.sizeDelta = new Vector2((cardWidth + cardSpacing) * cardCount - cardSpacing, contentRect.sizeDelta.y);

        // Content �ʱ� ��ġ ����
        contentRect.anchoredPosition = Vector2.zero;

        // ī�� ũ�� ����
        cardWidth = statusCardSlot.sizeDelta.x;

        // ī�� �� ����
        cardSpacing = horizontalLayoutGroup.spacing;
    }

    private void Update() {
        if (isScrolling) {
            SmoothScrollContent();
        }
    }

    // ���� ��ũ�� ��ư Ŭ�� �̺�Ʈ
    public void ScrollLeft() {
        if (currentIndex == 0) return;

        currentIndex--;
        movePosX = -(cardWidth + cardSpacing) * currentIndex;
        isScrolling = true;
    }

    // ������ ��ũ�� ��ư Ŭ�� �̺�Ʈ
    public void ScrollRight() {
        if (currentIndex >= contentRect.childCount - 1) return;

        currentIndex++;
        movePosX = -(cardWidth + cardSpacing) * currentIndex;
        isScrolling = true;
    }

    // ��ũ�ѹ� �̵� �Լ�
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
