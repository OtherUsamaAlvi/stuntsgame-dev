using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    private bool dragging;
    private Vector2 offset;
    private Vector3 defaultPosition;
    private void Start()
    {
        defaultPosition = transform.localPosition;
    }
    public void Update()
    {
        if (dragging)
        {
            transform.localPosition = new Vector2((transform.localPosition.x*(Input.mousePosition.x/4000)), 0) - offset;
        }
        if(transform.localPosition.x< -3093)
        {
            transform.localPosition = new Vector3(-3093, defaultPosition.y, defaultPosition.z);
        }
        if(transform.localPosition.x> -46)
        {
            transform.localPosition = new Vector3(-46, defaultPosition.y, defaultPosition.z);
        }
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosition.y, defaultPosition.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        StartCoroutine(waitAlittle(eventData));
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    IEnumerator waitAlittle(PointerEventData eventData)
    {
        yield return new WaitForSeconds(0.2f);
        dragging = true;
        offset = eventData.position - new Vector2(transform.position.x, 0);
    }

}
