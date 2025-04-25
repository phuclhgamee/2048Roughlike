using UnityEngine;

namespace Roughlike2048
{
    public class InputManager
    {
        private static Vector2 startPos;
        private static Vector2 endPos;
        private static float swipeThreshold = 50f;

        public static bool SwipeUp()
        {
            return DetectSwipe(out Vector2Int dir) && dir == Vector2Int.up;
        }

        public static bool SwipeDown()
        {
            return DetectSwipe(out Vector2Int dir) && dir == Vector2Int.down;
        }

        public static bool SwipeLeft()
        {
            return DetectSwipe(out Vector2Int dir) && dir == Vector2Int.left;
        }

        public static bool SwipeRight()
        {
            return DetectSwipe(out Vector2Int dir) && dir == Vector2Int.right;
        }

        private static bool DetectSwipe(out Vector2Int direction)
        {
            direction = Vector2Int.zero;
            Vector2 swipeDelta = Vector2.zero;

            // Xử lý cảm ứng
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                    return false;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    endPos = touch.position;
                    swipeDelta = endPos - startPos;
                }
                else
                {
                    return false;
                }
            }
            // Xử lý chuột – để hoạt động trên PC và giả lập Android
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startPos = Input.mousePosition;
                    return false;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    endPos = Input.mousePosition;
                    swipeDelta = endPos - startPos;
                }
                else
                {
                    return false;
                }
            }

            if (swipeDelta.magnitude < swipeThreshold)
                return false;

            if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
            {
                direction = swipeDelta.x > 0 ? Vector2Int.right : Vector2Int.left;
            }
            else
            {
                direction = swipeDelta.y > 0 ? Vector2Int.up : Vector2Int.down;
            }

            return true;
        }

    }
}