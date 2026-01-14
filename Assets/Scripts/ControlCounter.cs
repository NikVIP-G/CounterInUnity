using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlCounter : MonoBehaviour
{
    private int _valueButtonForPress = 0;

    public event Action IsPressed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_valueButtonForPress))
            IsPressed?.Invoke();
    }
}
