using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Transform _pointsTMP;

    public static UIController Instance;

    public void UpdatePoints(int pointsToChange)
    {
        _pointsTMP.GetComponent<TextMeshProUGUI>().text = pointsToChange.ToString();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _pointsTMP.GetComponent<TextMeshProUGUI>().text = "0";
    }
}
