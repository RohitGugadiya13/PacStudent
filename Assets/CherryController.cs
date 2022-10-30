using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private GameObject item;
    public Tweener tweener;
    List<GameObject> itemList = new List<GameObject>();

    [SerializeField] float speed = 1f;
    int invertedValueX, invertedValueY = 0;
    // Start is called before the first frame update
    void Start()
    {
        tweener = GetComponent<Tweener>();
        itemList.Add(item);

        invertedValueX = (int)(0f - transform.position.x);
        invertedValueY = (int)(0f - transform.position.y);
        Destroy(this.gameObject, speed);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (!tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(invertedValueX, invertedValueY, 0.0f), speed))
            {
                tweener.AddTween(itemList[i].transform, itemList[i].transform.position, new Vector3(invertedValueX, invertedValueY, 0.0f), speed);
                var tempItem = itemList[i];
                itemList.RemoveAt(i);
                itemList.Insert(itemList.Count, tempItem);
            }
        }
    }
}
