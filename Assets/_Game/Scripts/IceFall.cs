using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFall : MonoBehaviour
{
    public void OnInit()
    {
        
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -4.5f)
        {
            OnDespawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Character>().OnHit(25f);
            OnDespawn();
        }
    }

}
