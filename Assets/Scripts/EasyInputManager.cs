using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeType
{
    NONE,
    LEFT,
    RIGHT
}
public class EasyInputManager : MonoBehaviour
{
    public static EasyInputManager instance;
    private Vector2 startPos, endPos;
    private Vector2 difference;
    private float swipeThreshold = 0.15f;

    private SwipeType swipe = SwipeType.NONE;
    public Action<SwipeType> swipeCallback;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = endPos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Input.mousePosition;
            DetectSwipe();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            swipe = SwipeType.LEFT;
            swipeCallback(swipe);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            swipe = SwipeType.RIGHT;
            swipeCallback(swipe);
        }
    }

    private void DetectSwipe()
    {
        swipe = SwipeType.NONE;
        difference = endPos - startPos;
        if (difference.magnitude > swipeThreshold * Screen.width)
        {
            if (difference.x > 0)
            {
                swipe = SwipeType.RIGHT;
            }
            else if (difference.x < 0)
            {
                swipe = SwipeType.LEFT;
            }
        }

        swipeCallback(swipe);
    }
}
