using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetAdjacentGridPoint : MonoBehaviour
{
    public Transform upTransform, downTransform, leftTransform, rightTransform;

    public bool checkUp  = true, checkDown = true, checkLeft = true, checkRight = true;
    public float rayDistance = 1f;
    public LayerMask layerToIgnore;

    [ContextMenu("GET ADJACENT BLOCK")]
    private void GetAdjacentBlocks()
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, transform.up * -1f, rayDistance, ~layerToIgnore);
        RaycastHit2D downHit = Physics2D.Raycast(transform.position, transform.up, rayDistance, ~layerToIgnore);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, transform.right, rayDistance, ~layerToIgnore);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, transform.right * -1f, rayDistance, ~layerToIgnore);


        Debug.DrawLine(transform.position, upHit.point, Color.blue);
        Debug.DrawLine(transform.position, downHit.point, Color.yellow);
        Debug.DrawLine(transform.position, leftHit.point, Color.blue);
        Debug.DrawLine(transform.position, rightHit.point, Color.yellow);


        if (upHit && checkUp) upTransform = upHit.transform;
        if (downHit && checkDown) downTransform = downHit.transform;
        if (leftHit && checkLeft) leftTransform = leftHit.transform;
        if (rightHit && checkRight) rightTransform = rightHit.transform;
    }
}
