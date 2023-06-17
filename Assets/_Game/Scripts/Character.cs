using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected CombatText combatTextPrefab;

    protected float hp;
    private float maxHp = 100f;
    public bool IsDead => hp <= 0;

    private string currentAnimName;

    private void Start()
    {
        OnInit();
        AutoHealing();
    }

    public virtual void OnInit()
    {
        hp = maxHp;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn()
    {
        
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath()
    {
        if (anim != null)
        {
            ChangeAnim("die");
        }

        Invoke(nameof(OnDespawn), 2f);
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;

            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }

            healthBar.setNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    protected IEnumerator Healing()
    {
        while (true)
        {
            if (hp < maxHp && !IsDead)
            {
                hp++;
                healthBar.setNewHp(hp);
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
    }

    protected virtual void AutoHealing()
    {
        StartCoroutine(Healing());
    }
}
