using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    public Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    bool isMoving = false;

    public float topOffset = .001f;
    public enum Movement { up, down, left, right, none };
    public Movement currentInput, lastInput;


    public LayerMask layerToIgnore;
    [SerializeField] float speed = 2f;

    float upDistance, downDistance, leftDistance, rightDistance = 0f;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        itemList.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) )
        {
            lastInput = Movement.up;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Movement.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Movement.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) )
        {
            lastInput = Movement.down;
        }
        CheckForCollisions();

        for (int i = 0; i < itemList.Count; i++)
            {
                if (!tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(x, y, 0.0f), (Vector3.Distance(itemList[i].transform.position, new Vector3(x, y, 0.0f))) * speed))
                {
                    tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(x, y, 0.0f), (Vector3.Distance(itemList[i].transform.position, new Vector3(x, y, 0.0f))) * speed);
                    var tempItem = itemList[i];
                    itemList.RemoveAt(i);
                    itemList.Insert(itemList.Count, tempItem);
                }
                else
                {
                    DecideNextMoveDirection();
                    break;
                }
            }
    }

    private void DecideNextMoveDirection()
    { 
        switch (lastInput)
        {
            case Movement.up:
                currentInput = Movement.up;
                y = Mathf.Clamp(y + 1, -7f, 7f);
                break;
            case Movement.down:
                currentInput = Movement.down;
               y = Mathf.Clamp(y - 1, -7f, 7f);
                break;
            case Movement.left:
                currentInput = Movement.left;
                x = Mathf.Clamp(x - 1, -7f, 7f);
                break;
            case Movement.right:
                currentInput = Movement.right;
                x = Mathf.Clamp(x + 1, -7f, 7f);
                break;
        }

    }

    void CheckForCollisions()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector3.up, 100f, ~layerToIgnore);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector3.down, 100f, ~layerToIgnore);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector3.left, 100f, ~layerToIgnore);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector3.right, 100f, ~layerToIgnore);

        upDistance = Vector3.Distance(transform.position, upHit.collider.transform.position);
        downDistance = Vector3.Distance(transform.position, downHit.collider.transform.position);
        leftDistance = Vector3.Distance(transform.position, leftHit.collider.transform.position);
        rightDistance = Vector3.Distance(transform.position, rightHit.collider.transform.position);


        Debug.DrawLine(transform.position, upHit.point, Color.white);
        Debug.DrawLine(transform.position, downHit.point, Color.yellow);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.blue);
    }
}
