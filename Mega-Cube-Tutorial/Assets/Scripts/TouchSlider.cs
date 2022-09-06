using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
    public UnityAction OnPointerDownEvent;
    public UnityAction<float> OnPointerDragEvent;
    public UnityAction OnPointerUpEvent;

    private Slider uiSlider;

    private void Awake()
    {
        uiSlider = GetComponent<Slider>();
        uiSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnPointerDownEvent != null) 
            OnPointerDownEvent.Invoke();  //tek sat�rl�k if'lerde curly bracket'e gerek yokmu�.

        if (OnPointerDragEvent != null)
            OnPointerDragEvent.Invoke(uiSlider.value); // tek sat�rl�k if
    }
    private void OnSliderValueChanged(float value)
    {
        if (OnPointerDragEvent != null) 
            OnPointerDragEvent.Invoke(value); // tek sat�rl�k if
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (OnPointerUpEvent != null)
        {
            OnPointerUpEvent.Invoke();
        }

        // reset slider value;
        uiSlider.value = 0f;
    }

    private void OnDestroy()
    {
        // remove listener to avoid memory leaks.
        uiSlider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }
}
