using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss: Character
{
    [SerializeField] GameObject telePoint;

    public bool hasEnemy = false;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEnemy)
        {
            StartBoss();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        hp *= 10;
        healthBar.OnInit(1000, transform);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
        telePoint.SetActive(true);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
    }

    private void StartBoss()
    {
        telePoint.SetActive(false);
        StartCoroutine(IceSkill());
    }

    IEnumerator IceSkill()
    {
        hasEnemy = false;
        List<GameObject> childObjects = new List<GameObject>();

        foreach (Transform child in transform)
        {
            childObjects.Add(child.gameObject);
        }

        while (childObjects.Count > 0)
        {
            int randomIndex = Random.Range(0, childObjects.Count);
            GameObject randomChild = childObjects[randomIndex];
            randomChild.SetActive(true);
            childObjects.RemoveAt(randomIndex);
            yield return new WaitForSeconds(5f);
        }
    }
}
