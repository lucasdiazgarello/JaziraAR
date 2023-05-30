using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TryGetTouchPoisition : MonoBehaviour
{
    private bool _isTouching;

    public bool Try(out Vector2 touchPosition)
    {
        if (Application.isEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    touchPosition = default;
                    return false;
                }
                
                touchPosition = Input.mousePosition;
                return true;
            }
            touchPosition = default;
            return false;
        }

        if (Input.touchCount == 1 && !_isTouching && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _isTouching = true;
            if (Input.touches.Select(touch => touch.fingerId).Any(id => EventSystem.current.IsPointerOverGameObject(id)))
            {
                touchPosition = default;
                return false;
            }
            
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        
        touchPosition = default;
        return false;
    }

    private void Update()
    {
        if (Input.touchCount > 0) return;
        _isTouching = false;
    }
}