using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickSelect : MonoBehaviour, IPointerClickHandler
{
    InputField field;
    void Start()
    {
        field = GetComponent<InputField>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        field.Select();
    }
}