using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChildRoom
{
    public class MoveObjects3D : MonoBehaviour
    {
        private Vector3 offset;
        private float z_position;


        private void Start()
        {
            Input.multiTouchEnabled = false;
        }

        private void OnMouseDown()
        {
            z_position = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            offset = gameObject.transform.position - GetMouseWorldPosition();
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = z_position;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
    }
}


