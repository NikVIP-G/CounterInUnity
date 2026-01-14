using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private float _smoothIncreaseDuration = 0.5f;
    [SerializeField] private TextMeshProUGUI _counterText;

    private Coroutine _coroutine;
    private float _displayedValue = 0f;

    private void Start()
    {
        _displayedValue = _counter.CurrentValue;
        _counterText.text = _displayedValue.ToString("");
    }

    private void OnEnable()
    {
        _counter.Changed += Change;
    }

    private void OnDisable()
    {
        _counter.Changed -= Change;
        Stop();
    }

    private void Change(float value)
    {
        _coroutine = StartCoroutine(ChangedValue(value));
    }

    private IEnumerator ChangedValue(float target)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _smoothIncreaseDuration)
        {
            elapsedTime += Time.deltaTime;
            _counterText.text = _displayedValue.ToString("");

            yield return null;
        }

        _displayedValue = target;
        _counterText.text = _displayedValue.ToString("");
        _coroutine = null;
    }

    private void Stop()
    {
        if (_counter != null)
            StopCoroutine(_coroutine);
    }
}
