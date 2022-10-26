using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CheckOverLap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PopulatePointers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("MAKE GRID")]
    public void PopulatePointers()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector3.up, .1f);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector3.down, .1f);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector3.left, .1f);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector3.right, .1f);


        // if (upHit.point.y - transform.position.y > .6f) print("UP HIT " + (upHit.point.y - transform.position.y));
        // if (downHit.point.y - transform.position.y < .6f) print("down HIT" + (downHit.point.y - transform.position.y));
        // if (leftHit.point.x - transform.position.x < .6f) print("left HIT" + (transform.position.x - leftHit.point.x));
        // if (rightHit.point.x - transform.position.x > .6f) print("right HIT" + (rightHit.point.x - transform.position.x));

      


        Debug.DrawLine(transform.position, upHit.point, Color.white);
        Debug.DrawLine(transform.position, downHit.point, Color.yellow);
        Debug.DrawLine(transform.position, leftHit.point, Color.green);
        Debug.DrawLine(transform.position, rightHit.point, Color.blue);

        if (!upHit)
        {
            Instantiate(transform.gameObject, upHit.point, Quaternion.identity);
        }
        else if (!downHit)
        {
            Instantiate(transform.gameObject, downHit.point, Quaternion.identity);
        }
        else if (!leftHit)
        {
            Instantiate(transform.gameObject, leftHit.point, Quaternion.identity);
        }
       else if (!rightHit)
        {
            Instantiate(transform.gameObject, rightHit.point, Quaternion.identity);
        }


    }
}
