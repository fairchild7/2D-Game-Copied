using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : MonoBehaviour
{
    public float moveSpeed = 5f;

    //Ex1
    public Vector3[] moveArray = { new Vector3(1f, 0f, 0f), new Vector3(0f, 2f, 0f), new Vector3(1f, 1f, 0f) };
    private int currentIndex = 0;
    private bool isMoving = true;

    //Ex2
    public Rigidbody2D rb;
    public Animator animator;
    public Vector2 movement;

    //Ex4
    public Vector3 targetPosition = new Vector3(1f, 1.5f, 0f);
    public float waitTime = 1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        MouseInput();
    }

    private void FixedUpdate()
    {
        MovePosition();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = transform.position.z;
            StopAllCoroutines();
            StartCoroutine(MovePosition(targetPosition));
        }
    }

    private IEnumerator MovePosition(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void Move()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        if (Mathf.Abs(movement.x) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, movement.x > 0 ? 180 : 0, 0));
        }
    }

    private void MovePosition()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private IEnumerator EnumMoveThroughList()
    {
        while (isMoving)
        {
            Vector3 targetPosition = moveArray[currentIndex];

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentIndex++;
            if (currentIndex >= moveArray.Length)
            {
                currentIndex = 0;
                isMoving = false;
            }

            yield return null;
        }
    }

    public void MoveThroughList()
    {
        isMoving = true;
        currentIndex = 0;
        StopAllCoroutines();
        StartCoroutine(EnumMoveThroughList());
    }

    private IEnumerator EnumMoveToTarget(Vector3 targetPos)
    {
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator EnumMoveAroundTwoPoints()
    {
        Vector3 currentPos = transform.position;
        while (true)
        {
            yield return StartCoroutine(EnumMoveToTarget(targetPosition));
            yield return StartCoroutine(EnumMoveToTarget(currentPos));
            yield return new WaitForSeconds(waitTime);
        }
    }
    public void MoveAroundTwoPoints()
    {
        StopAllCoroutines();
        StartCoroutine(EnumMoveAroundTwoPoints());
    }

    public void MoveRandom()
    {
        isMoving = true;
        currentIndex = 0;
        StopAllCoroutines();
        StartCoroutine(EnumMoveRandom());
    }

    private IEnumerator EnumMoveRandom()
    {
        while (isMoving)
        {
            Vector3 targetPosition = moveArray[currentIndex];

            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            
            int nextIndex = Random.Range(0, 3);
            Debug.Log(currentIndex);
            while (nextIndex == currentIndex)
            {
                nextIndex = Random.Range(0, 3);
            }
            currentIndex = nextIndex;
            yield return null;
        }
    }
}
