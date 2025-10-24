using System;
using TMPro;
using UnityEngine;

public class CandiesText : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private int _totalCandies = 0;
    private int _collectedCandies = 0;

    private void Awake()
    {
        _textMeshPro = GetComponent<TMP_Text>();
    }

    public void SetTotalCandies(int totalCandies)
    {
        _totalCandies = totalCandies; 
        UpdateText();
    }

    public void UpdateCandiesCollected(int collectedCandies)
    {
        _collectedCandies = collectedCandies;
        UpdateText();
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _textMeshPro?.SetText($"{_collectedCandies}/{_totalCandies}");
    }
}
