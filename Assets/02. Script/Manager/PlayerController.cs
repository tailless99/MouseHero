using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    private PlayerCharacterBase currentCharacter;

    protected override void Awake() {
        base.Awake();
        SetCurrentCharacter(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacterBase>());
    }

    // UI ��ư���� ĳ���͸� �ε��� �� ���
    public void SetCurrentCharacter(PlayerCharacterBase character) {
        this.currentCharacter = character;
    }

    // ���� ĳ���Ϳ� ���ؼ� ��ȯ��
    public PlayerCharacterBase GetCharacter() => currentCharacter;
}
