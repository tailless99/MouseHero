using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private PlayerCharacterBase currentCharacter;

    protected override void Awake() {
        base.Awake();
        SetCurrentCharacter(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterBase>());
    }

    // UI 버튼에서 캐릭터를 로드할 때 사용
    public void SetCurrentCharacter(PlayerCharacterBase character) {
        this.currentCharacter = character;
    }

    // 현재 캐릭터에 대해서 반환함
    public PlayerCharacterBase GetCharacter() => currentCharacter;
}
