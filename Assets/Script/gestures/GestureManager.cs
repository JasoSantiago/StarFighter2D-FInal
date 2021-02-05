using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GestureManager : MonoBehaviour
{

    public static GestureManager Instance;

    public SwipeProperty1 _swipeProperty;
    public SpreadProperty _spreadProperty;
    public event EventHandler<SwipeEventArgs> OnSwipe;
    public event EventHandler<SpreadEventArgs> OnPinchSpread; 
    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float GestureTime = 0;

    private Touch gesture_finger;
    private Touch gesture_finger2;

    private bool isTouching = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && isTouching == false)
        {
            if (Input.touchCount == 1)
            {
                gesture_finger = Input.GetTouch(0);

                if (gesture_finger.phase == TouchPhase.Began)
                {
                    startPoint = gesture_finger.position;
                    GestureTime = 0;
                }

                if (gesture_finger.phase == TouchPhase.Ended)
                {
                    endPoint = gesture_finger.position;

                    if (GestureTime <= _swipeProperty.MaxGestureTime && Vector2.Distance(startPoint, endPoint) >=
                        (_swipeProperty.MinSwipeDistance * Screen.dpi))
                    {
                        FireSwipeEvent();
                    }

                }
            }
            else if(Input.touchCount > 1)
            {
                gesture_finger = Input.GetTouch(0);
                gesture_finger2 = Input.GetTouch(1);

                if (gesture_finger.phase == TouchPhase.Moved || gesture_finger2.phase == TouchPhase.Moved)

                {
                    Vector2 prevpoint1 = getpreviousPoint(gesture_finger);
                    Vector2 prevpoint2 = getpreviousPoint(gesture_finger2);

                    float currDistance = Vector2.Distance(gesture_finger.position, gesture_finger2.position);
                    float prevDistance = Vector2.Distance(prevpoint1, prevpoint2);

                    if (Mathf.Abs(currDistance - prevDistance) >= (_spreadProperty.MinDistanceChange *Screen.dpi))
                    {
                        FIreSpreadEvent(currDistance - prevDistance);
                    }
                }
            }

        }

        if (Input.touchCount == 0)
        {
            isTouching = false;
        }

    }

    private void FireSwipeEvent()
    {
        Vector2 diff = endPoint - startPoint;
        Directions dir;
        if (diff.x > 0)
        {
            dir = Directions.RIGHT;
        }
        else
        {
            dir = Directions.LEFT;
        }

        SwipeEventArgs args = new SwipeEventArgs(startPoint, diff, dir);

        if (OnSwipe != null)
        {
            OnSwipe(this, args);
        }

        isTouching = true;

    }

    private void FIreSpreadEvent(float dist_diff)
    {
        if(dist_diff > 0)
        Debug.Log("Spread");
        else
        {
            Debug.Log("pinch");
        }

        SpreadEventArgs args = new SpreadEventArgs(gesture_finger, gesture_finger, dist_diff);

        if (OnPinchSpread != null)
        {
            OnPinchSpread(this, args);
        }

        isTouching = false;
    }
    private Vector2 getpreviousPoint(Touch t)
    {
        return t.position - t.deltaPosition;
    }
}
