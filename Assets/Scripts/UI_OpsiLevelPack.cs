using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_OpsiLevelPack : MonoBehaviour
{
    public static event System.Action<LevelPackKuis> EventSaatKlik;

    [SerializeField]
    private TextMeshProUGUI _packName = null;

    [SerializeField]
    private LevelPackKuis _levelPack = null;

    [SerializeField]
    private Button _tombol = null;

    

    private void Start()
    {
        if (_levelPack != null)
        {
            SetLevelPack(_levelPack);
        }

        _tombol.onClick.AddListener(SaatKlik);
    }

    public void SetLevelPack(LevelPackKuis levelPack)
    {
        _packName.text = levelPack.name;
        _levelPack = levelPack;
    }

    private void OnDestroy()
    {
        _tombol.onClick.RemoveListener(SaatKlik);
    }

    private void SaatKlik()
    {
        EventSaatKlik?.Invoke(_levelPack);
        Debug.Log("KLIK!!");
    }
}
