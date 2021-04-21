using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapArea : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.Instance.CollectbyTap(eventData.position, transform);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.IsCliCked();
        }
    }
}
