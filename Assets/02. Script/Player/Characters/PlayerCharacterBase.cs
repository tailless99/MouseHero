using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterBase : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] protected float hp = 1f;
    [SerializeField] protected float strength = 1f;
    [SerializeField] protected float defence = 1f;
    [SerializeField] protected float luck = 1f;

    [Header("Player Skill")]
    [SerializeField] protected string skill_JSON_Name;

    [Header("Player Attack Base")]
    [SerializeField] protected float attackMaxCoolTime = 0.1f;

    [Header("Use Character Setteing")]
    [SerializeField] private string CharacterName;

    // 컴포넌트 모음
    private Player_Input_Action playerInput;
    private PlayerApplyDamage playerAttack;

    // 사용 변수 모음
    private float currentAttackCoolTime = 0f;


    private void Awake() {
        playerInput = new Player_Input_Action();
        playerAttack = GetComponent<PlayerApplyDamage>();
    }

    private void Start() {
        PlayerController.Instance.SetCurrentCharacter(this);

        // Input system Button 설정
        playerInput.Enable();
        
        // Z, X, LBMouse 모두 공격을 하는 기능을 가지고, 공격에 연타가 필요한 클릭커 게임이기 때문에 공격 기능을 세개의 키에 배정했다.
        playerInput.Attack.Z.performed += _ => Attack();
        playerInput.Attack.X.performed += _ => Attack();
        playerInput.Attack.LeftMouse.performed += _ => Attack();


        // 스테이터스 설정
        PlayerStatusManager.Instance.SetCharacterStatus(hp, strength, defence, luck);
    }

    protected virtual void Update() {
        if(currentAttackCoolTime > 0) {
            currentAttackCoolTime -= Time.deltaTime;
        }
    }

    // 자식 클래스에서 재정의되는 공격 함수
    public virtual void Attack() {
        if (currentAttackCoolTime > 0) return;

        // 쿨타임 설정
        currentAttackCoolTime = attackMaxCoolTime;

        // 공격 이펙트 위치 설정
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 spawnPos = mousePos;
        spawnPos.x += Random.Range(-0.3f, 0.3f); // 랜덤 보정치 추가
        spawnPos.y += Random.Range(-0.3f, 0.3f); // 랜덤 보정치 추가
        spawnPos.z = 0;

        var attackEffect = ObjectPoolingManager.Instance.MakeObj(CharacterName);
        attackEffect.transform.position = spawnPos;
        attackEffect.transform.rotation = Quaternion.identity;

        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0.1f, LayerMask.GetMask("Enemy"));

        if(ray) {
            if (ray.collider.gameObject.TryGetComponent<EnemyHitBox>(out EnemyHitBox enemyHitBox)) {
                float damage = playerAttack.GetAttackBaseDamage();
                enemyHitBox.TakeDamage(damage, spawnPos, playerAttack.IsCriticalAttack());
            }
        }
    }
}
