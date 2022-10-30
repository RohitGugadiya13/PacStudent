using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentControllerN : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] float speed = 2f;
    public Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    public float topOffset = .001f;
    public enum Movement { up, down, left, right, none };
    public Movement currentInput, lastInput;
    public GetAdjacentGridPoint collidedWith;

    public bool isMoving = false;
    bool hasGameStarted = false;
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
            hasGameStarted = true;
            if (!isMoving) CheckForDestinationPoint();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            lastInput = Movement.left;
            hasGameStarted = true;

            if (!isMoving) CheckForDestinationPoint();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            lastInput = Movement.right;
            hasGameStarted = true;
            if (!isMoving) CheckForDestinationPoint();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            lastInput = Movement.down;
            hasGameStarted = true;
            if (!isMoving) CheckForDestinationPoint();
        }

        if (hasGameStarted)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(x, y, 0.0f), speed))
                {
                    tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(x, y, 0.0f), speed);
                    var tempItem = itemList[i];
                    itemList.RemoveAt(i);
                    itemList.Insert(itemList.Count, tempItem);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        print("Collider 2d" + collider2D.gameObject.name);
        PathDecider(collider2D);

    }

    private void PathDecider(Collider2D collider2D)
    {
        if (collider2D.GetComponent<GetAdjacentGridPoint>()) collidedWith = collider2D.GetComponent<GetAdjacentGridPoint>();
        CheckForDestinationPoint();
    }

    private void CheckForDestinationPoint()
    {
        switch (lastInput)
        {
            case Movement.up:
                if (collidedWith.upTransform)
                {
                    x = collidedWith.upTransform.position.x;
                    y = collidedWith.upTransform.position.y;
                    currentInput = Movement.up;
                    isMoving = true;
                }
                else { if (tweener.activeTweens.Count > 0) tweener.activeTweens.RemoveAt(0); x = collidedWith.transform.position.x; y = collidedWith.transform.position.y; lastInput = Movement.none; isMoving = false; }
                break;
            case Movement.down:
                if (collidedWith.downTransform)
                {
                    x = collidedWith.downTransform.position.x;
                    y = collidedWith.downTransform.position.y;
                    currentInput = Movement.down;
                    isMoving = true;
                }
                else { if (tweener.activeTweens.Count > 0) tweener.activeTweens.RemoveAt(0); x = collidedWith.transform.position.x; y = collidedWith.transform.position.y; lastInput = Movement.none; isMoving = false; }
                break;
            case Movement.left:
                if (collidedWith.leftTransform)
                {
                    x = collidedWith.leftTransform.position.x;
                    y = collidedWith.leftTransform.position.y;
                    currentInput = Movement.left;
                    isMoving = true;
                }
                else { if (tweener.activeTweens.Count > 0) tweener.activeTweens.RemoveAt(0); x = collidedWith.transform.position.x; y = collidedWith.transform.position.y; lastInput = Movement.none; isMoving = false; }
                break;
            case Movement.right:
                if (collidedWith.rightTransform)
                {
                    x = collidedWith.rightTransform.position.x;
                    y = collidedWith.rightTransform.position.y;
                    currentInput = Movement.right;
                    isMoving = true;
                }
                else { if (tweener.activeTweens.Count > 0) tweener.activeTweens.RemoveAt(0); x = collidedWith.transform.position.x; y = collidedWith.transform.position.y; lastInput = Movement.none; isMoving = false; }
                break;
        }
    }
}
