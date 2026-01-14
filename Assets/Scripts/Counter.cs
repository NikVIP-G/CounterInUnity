using System;
using System.Collections;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private ControlCounter _button;
    [SerializeField] private float _changedRate = 0.5f;
    [SerializeField] private float _valueChanged = 1.0f;

    private float _currentValue = 0.0f;
    private Coroutine _coroutine;
    private bool _isActive;

    public event Action<float> Changed;
    public float CurrentValue => _currentValue;

    private void OnEnable()
    {
        _button.IsPressed += OnChange;
    }

    private void OnDisable()
    {
        _button.IsPressed -= OnChange;
        Stop();
    }

    private IEnumerator CountingRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_changedRate);

        while (enabled)
        {
            yield return wait;
            _currentValue += _valueChanged;
            Changed?.Invoke(_currentValue);
        }
    }

    private void OnChange()
    {
        if (_isActive == false)
        {
            Activate();
            _coroutine = StartCoroutine(CountingRoutine());
        }
        else
        {
            Deactivate();
            Stop();
        }
    }

    private void Stop()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void Activate()
    {
        _isActive = true;
    }

    private void Deactivate()
    {
        _isActive = false;
    }
}
