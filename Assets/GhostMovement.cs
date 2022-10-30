using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] private GameObject item;
    [SerializeField] float speed = 2f;
    public Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    public enum Movement { up, down, left, right, none };
    public Movement currentInput, lastInput;
    public GetAdjacentGridPoint collidedWith;

    public bool isMoving = false;
    Vector3 previousPosition = Vector3.zero;
    float stopTime = 0;

    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        animator = GetComponentInChildren<Animator>();
        itemList.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        if ((previousPosition - transform.position).magnitude < .26f)
        {
            stopTime += Time.deltaTime;
            isMoving = false;
            if (stopTime > .5f)
            {
                GhostMovementInput(UnityEngine.Random.Range(0, 4));
                stopTime = 0;
            }
        }
        if (lastInput == Movement.none)
        {
            stopTime += Time.deltaTime;
            if (stopTime > .5f)
            {
                GhostMovementInput(UnityEngine.Random.Range(0, 4));
                stopTime = 0;
            }
        }
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

    private void GhostMovementInput(int _movementIndex)
    {
        if (_movementIndex == 0)
        {
            lastInput = Movement.up;
            if (!isMoving && collidedWith) CheckForDestinationPoint();
        }
        else if (_movementIndex == 1)
        {
            lastInput = Movement.left;
            if (!isMoving && collidedWith) CheckForDestinationPoint();
        }
        else if (_movementIndex == 2)
        {
            lastInput = Movement.right;
            if (!isMoving && collidedWith) CheckForDestinationPoint();
        }
        else if (_movementIndex == 3)
        {
            lastInput = Movement.down;
            if (!isMoving && collidedWith) CheckForDestinationPoint();
        }
    }
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        print("Collider 2d" + collider2D.gameObject.name);
        if (collider2D.GetComponent<GetAdjacentGridPoint>())
        {
            collidedWith = collider2D.GetComponent<GetAdjacentGridPoint>();
            CheckForDestinationPoint();
        }
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
                    previousPosition = transform.position;
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
                    previousPosition = transform.position;
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
                    previousPosition = transform.position;
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
                    previousPosition = transform.position;
                }
                else { if (tweener.activeTweens.Count > 0) tweener.activeTweens.RemoveAt(0); x = collidedWith.transform.position.x; y = collidedWith.transform.position.y; lastInput = Movement.none; isMoving = false; }
                break;
        }
    }

    public void InScareState()
    {
        animator.SetBool("ScaredState", true);
        animator.SetBool("ScaredStateDone", false);


        Invoke("ScaredStateRecovering", 5f);
        Invoke("ScaredStateDone", 10f);
    }

    void ScaredStateRecovering()
    {
        animator.SetBool("ScaredStateRecovering", true);
    }
    void ScaredStateDone()
    {
        animator.SetBool("ScaredStateDone", true);
        animator.SetBool("ScaredStateRecovering", false);
        animator.SetBool("ScaredState", false);

        Manager.instance.enemiesCanDoDamage = true;
        Manager.instance.powwerTimerActive.SetActive(false);

    }
}
