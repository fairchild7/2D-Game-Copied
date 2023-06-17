using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSight : MonoBehaviour
{
    public Boss boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            boss.hasEnemy = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
    
        }
    }

}
