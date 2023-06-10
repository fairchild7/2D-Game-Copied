using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask groundLayer;
    public float movingTime = 2f;

    void Start()
    {

    }

    void Update()
    {
        MouseClick();
        //Ex9MouseClick();
    }

    void Ex9MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Started");
            StopAllCoroutines();
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = transform.position.z;
            StartCoroutine(MoveInTime(transform.position, clickPos, movingTime));
        }
    }
    
    IEnumerator MoveInTime(Vector3 startPos, Vector3 endPos, float time)
    {
        float moveDist = Vector3.Distance(startPos, endPos);
        while (transform.position != endPos)
        {
            //transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime / time);
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveDist / time * Time.deltaTime);
            Debug.Log("On my way");
            yield return null;
        }
    }

    void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = transform.position.z;
            StartCoroutine(MoveAndCheckCoroutine(clickPos));
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ChangeColor(clickPos);
        }
    }

    IEnumerator MoveAndCheckCoroutine(Vector3 target)
    {
        while (transform.position != target)
        {
            Vector3 moveVector = target - transform.position;
            moveVector.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveVector, 0.5f, groundLayer);
            if (hit.collider != null) 
            {
                Debug.Log("Complete!");
            }
            yield return null;
        }
    }

    void ChangeColor(Vector3 clickPos)
    {
        Vector3 projVector = transform.position - clickPos;
        RaycastHit2D hit = Physics2D.Raycast(clickPos, projVector, 0.1f);
        if (hit.collider != null)
        {
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.red;
        }
    }
}
