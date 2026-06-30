using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Boss2AI : MonoBehaviour
{
    public Transform target;
    public Transform AttackPoint;
    public Vector2 attackSize;
    float attackDelay;
    Boss2 boss2;
    Animator enemyAnimator;
    public LayerMask enemyLayers;
    public bool isAttacking = false;
    public float attackDuration = 0.5f;
    private bool isTeleportAnimationFinished = false;
    private bool isAttack2AnimationFinished = false;
    public AnimationClip castskill;
    private AnimatorOverrideController overrideController;
    public Animator castEffectAnimator;
    public GameObject castEffectPrefab;
    
    void Start()
    {
        boss2 = GetComponent<Boss2>();
        enemyAnimator = boss2.enemyAnimator;

        
    }

    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        float distance = Vector3.Distance(transform.position, target.position);

        if (attackDelay == 0 && distance <= boss2.status.fieldOfVision)
        {
            FaceTarget();

            if (!isAttacking)
            {
                StartCoroutine(AttackTarget(distance <= boss2.status.atkRange));
            }
        }
        else
        {
            enemyAnimator.SetBool("moving", false);
        }
    }
    
    void MoveToTarget()
    {
        float dir = target.position.x - transform.position.x;
        dir = (dir < 0) ? -1 : 1;
        transform.Translate(new Vector2(dir, 0) * boss2.status.moveSpeed * Time.deltaTime);
        enemyAnimator.SetBool("moving", true);
    }

    void FaceTarget()
    {
        if (target.position.x - transform.position.x < 0) // 타겟이 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else // 타겟이 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // 이 메서드는 애니메이션 이벤트를 통해 호출됩니다.
    public void OnTeleportAnimationEnd()
    {
        isTeleportAnimationFinished = true;
    }

    // 이 메서드는 애니메이션 이벤트를 통해 호출됩니다.
    public void OnAttack2AnimationEnd()
    {
        isAttack2AnimationFinished = true;
    }

    IEnumerator AttackRoutine(string[] combo)
    {
        foreach (string attack in combo)
        {
            if(!boss2.dead && attack == "attack"){
                enemyAnimator.SetTrigger("attack");
                yield return new WaitForSeconds(enemyAnimator.GetCurrentAnimatorStateInfo(0).length);
            }
            else if(!boss2.dead && attack == "teleportAttack"){
                isTeleportAnimationFinished = false;
                enemyAnimator.SetTrigger("teleport");
                yield return new WaitUntil(() => isTeleportAnimationFinished);
                
                Player player = target.GetComponent<Player>();
                Vector3 newPosition = player.isFacingRight
                    ? new Vector3(player.transform.position.x - 2, player.transform.position.y + 1.529f, 0)
                    : new Vector3(player.transform.position.x + 2, player.transform.position.y + 1.529f, 0);
                transform.position = newPosition;

                isAttack2AnimationFinished = false;
                enemyAnimator.SetTrigger("attack2");
                yield return new WaitUntil(() => isAttack2AnimationFinished);
            }
            else if(!boss2.dead && attack == "castAttack"){
                enemyAnimator.SetTrigger("cast");
                enemyAnimator.SetTrigger("casting");
                yield return new WaitForSeconds(1.3f);
                yield return StartCoroutine(ExecuteAttacks(5));
                
            }
        }
    }

    

    IEnumerator ExecuteAttacks(int numberOfAttacks)
    {
        for (int i = 0; i < numberOfAttacks; i++)
        {
            Player player = target.GetComponent<Player>();
            Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, -1);
            yield return StartCoroutine(PlayCastSkillAnimation(newPosition));
            yield return new WaitForSeconds(0.2f);
            if(boss2.dead==true){
                    break;
                }
        }
    }

    IEnumerator PlayCastSkillAnimation(Vector3 position)
    {
        yield return new WaitForSeconds(0.5f);
        GameObject exprosion = Instantiate(castEffectPrefab, position, Quaternion.identity);
        exprosion.transform.parent = boss2.transform;
        Animator castEffectAnimator = exprosion.GetComponent<Animator>();
        castEffectAnimator.SetTrigger("play");
        yield return new WaitForSeconds(castEffectAnimator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(exprosion);

    }

    IEnumerator AttackTarget(bool isInAttackRange)
    {
        isAttacking = true;

        if (!target.GetComponent<Player>().isDashing) {
            string[] combo;

            if (isInAttackRange) {
                combo = new string[] { "attack" };
            } else {
                int comboIndex = Random.Range(1, 3);
                switch (comboIndex)
                {
                    case 1:
                        combo = new string[] { "teleportAttack" };
                        break;
                    case 2:
                        combo = new string[] { "castAttack" };
                        break;
                    default:
                        combo = new string[] { "teleportAttack" };
                        break;
                }
            }

            yield return StartCoroutine(AttackRoutine(combo));
        }
        
        attackDelay = boss2.status.atkSpeed;
        isAttacking = false;
    }

    void CreateHitBox()
    {
        BoxCollider2D hitBox = AttackPoint.gameObject.AddComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
        hitBox.size = attackSize;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint.position, attackSize, 0, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Player>().TakeDamage(boss2.status.atkDmg);
            }
        }
    }

    void RemoveHitBox()
    {
        BoxCollider2D hitBox = AttackPoint.GetComponent<BoxCollider2D>();
        if (hitBox != null)
        {
            Destroy(hitBox);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireCube(AttackPoint.position, attackSize);
    }

    public void ApplyDamage()
    {
        if (target != null)
    {
        Player player = target.GetComponent<Player>();
        if (player != null && !player.isDashing)
        {
            CreateHitBox();
            StartCoroutine(WaitAndPrint(attackDuration));
            RemoveHitBox();
        }
    }
    }

    void ExCreateHitBox()
    {
        UnityEngine.Vector2 exhitBox = new UnityEngine.Vector2(30, 20);
        BoxCollider2D hitBox = AttackPoint.gameObject.AddComponent<BoxCollider2D>();
        hitBox.isTrigger = true;
        hitBox.size = exhitBox;

        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPoint.position, exhitBox, 0, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Player"))
            {
                enemy.GetComponent<Player>().TakeDamage(boss2.status.atkDmg);
            }
        }
    }

    public void ExApplyDamage()
    {
        if (target != null)
    {
        Player player = target.GetComponent<Player>();
        if (player != null && !player.isDashing)
        {
            ExCreateHitBox();
            StartCoroutine(WaitAndPrint(attackDuration));
            RemoveHitBox();
        }
    }
    }

    IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}