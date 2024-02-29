using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private Transform _barTransform;

    private const float MAX_TIME = 80f;
    private float _time;
    private float _multiplier = 1f;
    private float _timerForMultiplier = 0f;

    public static TimerBar Instance;
    
    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        _time = MAX_TIME / 2;
    }

    private void SetTime(float timeToSet)
    {
        _time = timeToSet > MAX_TIME ? MAX_TIME : timeToSet;
        UpdateBarSize();
    }

    public void IncreaseTime()
    {
        _time = _time + 4f > MAX_TIME ? MAX_TIME : _time + 4f;
        UpdateBarSize();
    }

    private void Update() {
        UpdateMultiplier();
        _time -= 5f * Time.deltaTime * _multiplier;
        UpdateBarSize();
    }

    private void UpdateBarSize()
    {
        _barTransform.localScale = new Vector3(_time/80f*5f,1f,1f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsTimeOut()
    {
        if (_time < 0)
        {
            return true;
        }
        return false;
    }

    public void Restart()
    {
        _multiplier = 1f;
        SetTime(MAX_TIME / 2);
        _timerForMultiplier = 0f;
    }

    private void UpdateMultiplier()
    {
        _timerForMultiplier += Time.deltaTime;
        _multiplier = 1 + _timerForMultiplier / 20f > 4 ? 4 : 1 + _timerForMultiplier / 20f;
    }
}
