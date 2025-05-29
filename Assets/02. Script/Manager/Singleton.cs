using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    private static T instance;
    private static readonly object lockObject = new object(); // lock문을 사용하기 위한 변수

    //public static T Instance { get { return instance; } }
    public static T Instance {
        get {
            if (instance == null) {
                lock (lockObject) {
                    if (instance == null) {
                        // 씬에서 해당 타입의 오브젝트를 찾습니다.
                        instance = FindObjectOfType<T>();

                        // 씬에 없다면 새로운 게임 오브젝트를 생성하고 Singleton 컴포넌트를 추가합니다.
                        if (instance == null) {
                            GameObject singletonObject = new GameObject(typeof(T).Name + " (Singleton)");
                            instance = singletonObject.AddComponent<T>();
                            DontDestroyOnLoad(singletonObject); // 씬 전환 시 파괴 방지 (선택 사항)
                        }
                    }
                }
            }
            return instance;
        }
    }

    protected virtual void Awake() {
        if (instance != null && this.gameObject != null) {
            Destroy(this.gameObject);
        }
        else {
            instance = (T)this;
        }

        if (!gameObject.transform.parent) {
            DontDestroyOnLoad(gameObject);
        }
    }
}

