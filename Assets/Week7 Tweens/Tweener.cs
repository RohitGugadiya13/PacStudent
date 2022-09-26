using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;
    public List<Tween> activeTweens = new List<Tween>();
    public Tween activeTween;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(activeTweens.Count + " COUNT ");
        if (activeTweens.Count > 0)
        {
            float dist = Vector3.Distance(activeTweens[0]._Target.transform.position, activeTweens[0]._EndPos);
            if (dist > 0.1f)
            {
                activeTweens[0]._Target.transform.position = Vector3.Lerp(activeTweens[0]._StartPos, activeTweens[0]._EndPos, Time.time - activeTweens[0]._StartTime); //Linear interpolation

                float fraction = (Time.time - activeTweens[0]._StartTime) / activeTweens[0]._Duration; //Calculating easing in cubic interpolation considering the fraction is between 0 to 1
                                                                                                       //print(fraction + " FRACTIONS");
                activeTweens[0]._Target.position = Vector3.Lerp(activeTweens[0]._StartPos, activeTweens[0]._EndPos, fraction); //Cubic interpolation
                                                                                                                               //Debug.Log("Target : " + activeTween.Target.transform.position);

            }
            else
            {
                activeTweens[0]._Target.position = activeTweens[0]._EndPos;
                activeTweens.RemoveAt(0);
            }
        }
    }

    public void AddTweenSingle(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
    }



    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject) == false)
        {
            return InstantiateATween(targetObject, startPos, endPos, duration);
        }
        else
        {
            return false;
        }
    }

    public bool TweenExists(Transform target)
    {
        bool tweenExsit = false;

        for (int i = 0; i < activeTweens.Count; i++)
        {
            tweenExsit = activeTweens[i].Target == target ? true : false;
        }
        return tweenExsit;
    }

    bool InstantiateATween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        var activeTween = new Tween(targetObject, startPos, endPos, Time.time, duration);
        activeTweens.Add(activeTween);
        return true;
    }


}

