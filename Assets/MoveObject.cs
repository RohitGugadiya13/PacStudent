using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public Transform[] targetPoints;
    public float speed;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoints[i].position, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, targetPoints[i].position) < 1f) i = (int)Mathf.Repeat(i + 1, 4);
    }
}
