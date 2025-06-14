using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private void Start() {
        Time.timeScale = 0;
    }
}
