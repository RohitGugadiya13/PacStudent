using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RemoveBoxCollideir : MonoBehaviour
{
    BoxCollider2D[] boxCollider2Ds;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2Ds = GetComponentsInChildren<BoxCollider2D>();


        foreach(BoxCollider2D boxCollider2D in boxCollider2Ds)
        {
            DestroyImmediate(boxCollider2D);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
