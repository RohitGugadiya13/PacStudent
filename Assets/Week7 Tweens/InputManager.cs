using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
   public  Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    public float x, y = 0;

    bool isMoving = false;

    public float topOffset = .001f;
    public enum Movement { up, down, left, right, none };
    public Movement playerMovement;
    public LayerMask layerToIgnore;

    Animator animator;
    [SerializeField]float speed = 2f;




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

        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector3.up, 100f, ~layerToIgnore);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector3.down, 100f, ~layerToIgnore);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector3.left, 100f, ~layerToIgnore);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector3.right, 100f, ~layerToIgnore);


        // if (upHit.point.y - transform.position.y > .6f) print("UP HIT " + (upHit.point.y - transform.position.y));
        // if (downHit.point.y - transform.position.y < .6f) print("down HIT" + (downHit.point.y - transform.position.y));
        // if (leftHit.point.x - transform.position.x < .6f) print("left HIT" + (transform.position.x - leftHit.point.x));
        // if (rightHit.point.x - transform.position.x > .6f) print("right HIT" + (rightHit.point.x - transform.position.x));

        var upDistance = Vector3.Distance(transform.position, upHit.collider.transform.position);
        var downDistance = Vector3.Distance(transform.position, downHit.collider.transform.position);
        var leftDistance = Vector3.Distance(transform.position, leftHit.collider.transform.position);
        var rightDistance = Vector3.Distance(transform.position, rightHit.collider.transform.position);


        Debug.DrawLine(transform.position, upHit.point, Color.white);
        Debug.DrawLine(transform.position, downHit.point, Color.yellow);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.blue);

        if (Input.GetKeyDown(KeyCode.W) && upDistance > .6f)
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A) && leftDistance > .6f)
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) && rightDistance > .6f)
        {
            MoveRight();
        }
        else if (Input.GetKeyDown(KeyCode.S) && downDistance > .6f)
        {
            MoveDown();
        }

        if (isMoving)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                if (!tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(x, y, 0.0f), (Vector3.Distance(itemList[i].transform.position, new Vector3(x, y, 0.0f)))*speed))
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
        animator.SetBool("GameStarted", true);
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 180);
        playerMovement = Movement.down;
    }

    private void MoveRight()
    {
        animator.SetBool("GameStarted", true);
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, -90);
        playerMovement = Movement.right;
    }

    private void MoveLeft()
    {
        animator.SetBool("GameStarted", true);
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 90);
        playerMovement = Movement.left;
    }

    private void MoveUp()
    {
        animator.SetBool("GameStarted", true);
        if (tweener.activeTweens.Count > 0) { tweener.activeTweens.RemoveAt(0); x = transform.position.x; y = transform.position.y; }
        transform.rotation = Quaternion.Euler(0, 0, 0);
        playerMovement = Movement.up;
    }

    void FixedUpdate()
    {
        // RaycastHit2D upHit = Physics2D.Raycast(transform.position, transform.up, 100f, ~layerToIgnore);
        RaycastHit2D upHit = Physics2D.BoxCast(transform.position, Vector2.one / 2f, 0, transform.up, 100f, ~layerToIgnore);


        //print(upHit.collider.name);

        if (upHit.collider != null && !Tweener.stopMovement)
        {
            switch (playerMovement)
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



    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
