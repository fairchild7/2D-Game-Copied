using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppingMove : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float moveTimer = 0f;
    public bool isMoving = true;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 endPos = new Vector3(3f, 3f, 0f);
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = 0f;
        target = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveTimer < 1f)
        {
            moveTimer += Time.deltaTime;
            StartCoroutine(MoveTo(target));
        }
        else
        {
            isMoving = false;
            moveTimer = 0f;
        }
    }

    void SwitchTarget()
    {
        if (target == endPos)
        {
            target = startPos;
        }
        else if (target == startPos)
        {
            target = endPos;
        }
    }

    IEnumerator MoveTo(Vector3 targetPos)
    {
        if (isMoving)
        {
            if (transform.position != targetPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
                yield return null;
            }
            else
            {
                SwitchTarget();
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);
            isMoving = true;
        }
    }
}
