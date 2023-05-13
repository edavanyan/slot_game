using Plugins.Demigiant.DOTween.Modules;
using UnityEngine;

namespace Gestures
{
    public class GestureDetector : MonoBehaviour
    {
        public RectTransform slotScreen;
        public RectTransform mainUI;
        
        public float minSwipeDistance = 50f; // Minimum swipe distance to register as a swipe

        private Vector2 _startPos;
        private bool _isSwipe;

        private void Update()
        {
            // For touch devices
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _startPos = touch.position;
                    _isSwipe = true;
                }
                else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    _isSwipe = false;
                }
                else if (touch.phase == TouchPhase.Moved && _isSwipe)
                {
                    DetectSwipe(touch.position);
                }
            }
            // For mouse input (PC)
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _startPos = Input.mousePosition;
                    _isSwipe = true;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    _isSwipe = false;
                }
                else if (Input.GetMouseButton(0) && _isSwipe)
                {
                    DetectSwipe(Input.mousePosition);
                }
            }
        }

        private void DetectSwipe(Vector2 endPos)
        {
            Vector2 swipeDelta = endPos - _startPos;

            // Check if the swipe distance is greater than the minimum swipe distance
            if (swipeDelta.magnitude > minSwipeDistance)
            {
                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x > 0)
                    {
                        OnSwipeRight();
                    }
                    else
                    {
                        OnSwipeLeft();
                    }
                }
                else
                {
                    if (swipeDelta.y > 0)
                    {
                        OnSwipeUp();
                    }
                    else
                    {
                        OnSwipeDown();
                    }
                }

                _isSwipe = false;
            }
        }

        private void OnSwipeUp()
        {
            slotScreen.DOAnchorPosY(2500, 0.25f);
            mainUI.DOAnchorPosY(0, 0.25f);
        }

        private void OnSwipeDown()
        {
            slotScreen.DOAnchorPosY(0, 0.25f);
            mainUI.DOAnchorPosY(-2500, 0.25f);
        }

        private void OnSwipeLeft()
        {
            Debug.Log("Swipe Left Detected");
            // Implement your swipe left functionality here
        }

        private void OnSwipeRight()
        {
            Debug.Log("Swipe Right Detected");
            // Implement your swipe right functionality here
        }
    }
}
