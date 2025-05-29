using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    private static T instance;
    private static readonly object lockObject = new object(); // lock���� ����ϱ� ���� ����

    //public static T Instance { get { return instance; } }
    public static T Instance {
        get {
            if (instance == null) {
                lock (lockObject) {
                    if (instance == null) {
                        // ������ �ش� Ÿ���� ������Ʈ�� ã���ϴ�.
                        instance = FindObjectOfType<T>();

                        // ���� ���ٸ� ���ο� ���� ������Ʈ�� �����ϰ� Singleton ������Ʈ�� �߰��մϴ�.
                        if (instance == null) {
                            GameObject singletonObject = new GameObject(typeof(T).Name + " (Singleton)");
                            instance = singletonObject.AddComponent<T>();
                            DontDestroyOnLoad(singletonObject); // �� ��ȯ �� �ı� ���� (���� ����)
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

