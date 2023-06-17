using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] private TeleportPoint target;

    private Collider2D coll;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            target.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Teleport(collision.gameObject.transform));
            Debug.Log("Teleport " + target.name);
        }
    }

    IEnumerator Teleport(Transform playerTransform)
    {
        playerTransform.position = target.transform.position + Vector3.right;
        yield return new WaitForSeconds(5f);
        target.GetComponent<BoxCollider2D>().enabled = true;
    }
}
