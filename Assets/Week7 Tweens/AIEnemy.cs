using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] private GameObject item;
    Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    bool isMoving = false;
    public enum Movement { up, down, left, right, none };
    public Movement enemyMovement;
    public LayerMask layerToIgnore;

    Animator animator;

    Vector3 previousPosition;
    public float timeToCheck;
    [SerializeField] float speed =.2f;


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

        AIMovement();

    }

    private void AIMovement()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector3.up, 100f, ~layerToIgnore);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector3.down, 100f, ~layerToIgnore);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector3.left, 100f, ~layerToIgnore);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector3.right, 100f, ~layerToIgnore);



        var upDistance = Mathf.Abs(transform.position.y - upHit.collider.transform.position.y);
        var downDistance = Mathf.Abs(transform.position.y - downHit.collider.transform.position.y);
        var leftDistance = Mathf.Abs(transform.position.x - leftHit.collider.transform.position.x);
        var rightDistance = Mathf.Abs(transform.position.x - rightHit.collider.transform.position.x);

        if (Vector3.Distance(previousPosition, transform.position) < .1f)
        {
            timeToCheck += Time.deltaTime;
            if (timeToCheck > .3f)
            {
                timeToCheck = 0;
                switch (Random.Range(0, 4))
                {
                    case 0:
                        if (upDistance > 1f)
                        {
                            MoveUp();
                        }
                        else if (downDistance > 1f)
                        {
                            MoveDown();
                        }
                        else if (leftDistance > 1f)
                        {
                            MoveLeft();
                        }
                        else if (rightDistance > 1f)
                        {
                            MoveRight();
                        }
                        break;
                    case 1:


                        if (downDistance > 1f)
                        {
                            MoveDown();
                        }
                        else if (upDistance > 1f)
                        {
                            MoveUp();
                        }
                        else if (leftDistance > 1f)
                        {
                            MoveLeft();
                        }
                        else if (rightDistance > 1f)
                        {
                            MoveRight();
                        }
                        break;

                    case 2:

                        if (leftDistance > 1f)
                        {
                            MoveLeft();
                        }
                        else if (rightDistance > 1f)
                        {
                            MoveRight();
                        }
                        else if (upDistance > 1f)
                        {
                            MoveUp();
                        }
                        else if (downDistance > 1f)
                        {
                            MoveDown();
                        }
                        break;
                    case 3:
                        if (leftDistance > 1f)
                        {
                            MoveLeft();
                        }
                        else if (rightDistance > 1f)
                        {
                            MoveRight();
                        }
                        else if (upDistance > 1f)
                        {
                            MoveUp();
                        }
                        else if (downDistance > 1f)
                        {
                            MoveDown();
                        }
                        break;
                }

            }
        }
        else
        {
            previousPosition = transform.position;
            timeToCheck = 0;
        }

        if (isMoving)
        {
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
                    break;
                }
            }
        }
    }

    private void MoveDown()
    {
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 180);
        enemyMovement = Movement.down;
    }

    private void MoveRight()
    {
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, -90);
        enemyMovement = Movement.right;
    }

    private void MoveLeft()
    {
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 90);
        enemyMovement = Movement.left;
    }

    private void MoveUp()
    {
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        enemyMovement = Movement.up;
    }

    void FixedUpdate()
    {
        // RaycastHit2D upHit = Physics2D.Raycast(transform.position, transform.up, 100f, ~layerToIgnore);
        RaycastHit2D upHit = Physics2D.BoxCast(transform.position, Vector2.one / 2f, 0, transform.up, 100f, ~layerToIgnore);

        if (upHit.collider != null)
        {
            switch (enemyMovement)
            {
                case Movement.up:
                    x = transform.position.x;
                    y = upHit.point.y - .5f;
                    break;
                case Movement.down:
                    x = transform.position.x;
                    y = upHit.point.y + .5f;
                    break;
                case Movement.right:
                    x = upHit.point.x - 0.5f;
                    y = transform.position.y;
                    break;
                case Movement.left:
                    x = upHit.point.x + 0.5f;
                    y = transform.position.y;
                    break;
            }
            isMoving = true;
        }
        else
        {
            isMoving = false;
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
    }

}
