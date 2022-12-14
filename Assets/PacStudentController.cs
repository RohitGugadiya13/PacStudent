using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] float speed = 2f;
    [SerializeField] float distanceOffset = 7;

    public Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    bool isMoving = false;

    public float topOffset = .001f;
    public enum Movement { up, down, left, right, none };
    public Movement currentInput, lastInput;


    public LayerMask layerToIgnore;

    float upDistance, downDistance, leftDistance, rightDistance = 0f;
    public bool shallKeepGoing = false;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        itemList.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
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
        else if (Input.GetKeyDown(KeyCode.S))
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
                if (!shallKeepGoing) { lastInput = Movement.none; break; }
                isMoving = true;
                currentInput = Movement.up;
                y = Mathf.Clamp(y + 0.25f, -6.25f, 6.25f);
                break;
            case Movement.down:
                if (!shallKeepGoing) { lastInput = Movement.none; break; }
                isMoving = true;
                currentInput = Movement.down;
                y = Mathf.Clamp(y - 0.25f, -6.25f, 6.25f);
                break;
            case Movement.left:
                if (!shallKeepGoing) { lastInput = Movement.none; break; }
                isMoving = true;
                currentInput = Movement.left;
                x = Mathf.Clamp(x - 0.25f, -7f, 7f);
                break;
            case Movement.right:
                if (!shallKeepGoing) { lastInput = Movement.none; break; }
                isMoving = true;
                currentInput = Movement.right;
                x = Mathf.Clamp(x + 0.25f, -7f, 7f);
                break;
        }

    }

    void CheckForCollisions()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, transform.up, 100f, ~layerToIgnore);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, transform.up * -1f, 100f, ~layerToIgnore);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, transform.right * -1f, 100f, ~layerToIgnore);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, transform.right, 100f, ~layerToIgnore);

        Debug.DrawLine(transform.position, upHit.point, Color.white);
        Debug.DrawLine(transform.position, downHit.point, Color.yellow);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.blue);

        switch (lastInput)
        {
            case Movement.up:
                if (upHit) shallKeepGoing = true;
                else
                    if (tweener.activeTweens.Count > 0)
                {
                    shallKeepGoing = false;
                    tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y;
                }
                break;
            case Movement.down:
                if (downHit) shallKeepGoing = true;
                else
                    if (tweener.activeTweens.Count > 0)
                {
                    shallKeepGoing = false;
                    tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y;
                }
                break;
            case Movement.left:
                if (leftHit) shallKeepGoing = true;
                else
                    if (tweener.activeTweens.Count > 0)
                {
                    shallKeepGoing = false;
                    tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y;
                }
                break;
            case Movement.right:
                if (rightHit) shallKeepGoing = true;
                else
                    if (tweener.activeTweens.Count > 0)
                {
                    shallKeepGoing = false;
                    tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y;
                }
                break;
        }

    }
}
