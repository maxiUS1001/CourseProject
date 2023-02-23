using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCameraToObj : MonoBehaviour, IPointerClickHandler
{
    public Camera Camera;
    public GameObject targetObject;
    float speed = 0.009f;
    bool move;
    float offset = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        offset = 0;
        move = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (move)
        {
            if (offset <= 1)
            {
                offset += speed;
                Camera.transform.position = Vector3.Lerp(Camera.transform.position, targetObject.transform.position, offset);
                Camera.transform.rotation = Quaternion.Lerp(Camera.transform.rotation, targetObject.transform.rotation, offset);
            }
            else
            {
                move = false;
            }
        }
    }
}
