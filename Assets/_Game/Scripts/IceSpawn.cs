using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawn : MonoBehaviour
{
    [SerializeField] IceFall iceFallPrefab;
    [SerializeField] float waitTime;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateIceFall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateIceFall()
    {
        StartCoroutine(IceFallCoroutine());
    }

    IEnumerator IceFallCoroutine()
    {
        while (true)
        {
            Instantiate(iceFallPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
