using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveMoving : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 startPoint = new Vector3(4f, 2f, 0f);
    public Vector3 endPoint = new Vector3(-4f, 2f, 0f);
    public float t;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        radius = 0.5f * Mathf.Sqrt((startPoint.x - endPoint.x) * (startPoint.x - endPoint.x) + (startPoint.y - endPoint.y) * (startPoint.y - endPoint.y));
        t = 0;
        transform.position = startPoint;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        MoveCurve(t);
    }

    public Vector3 CurveValue(float t)
    {
        return new Vector3(radius * Mathf.Sin(t) + startPoint.x, radius * Mathf.Cos(t) + startPoint.y, 0);
    }

    void MoveCurve(float t)
    {
        Vector3 targetPostion = CurveValue(t);
        transform.position = Vector3.Lerp(transform.position, targetPostion, 1);
    }
}
