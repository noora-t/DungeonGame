using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChange : MonoBehaviour
{
    [SerializeField] ItemCollection _itemCollection;
    [SerializeField] ItemType _coin;

    GameObject _blackScreenCanvas;
    GameObject _levelCompleteCanvas;
    TMP_Text _coinsAmount;

    //void Start()
    //{
    //    StartCoroutine(LightenScreen(1f));
    //}

    //IEnumerator LightenScreen(float duration)
    //{
    //    Image blackScreen = _blackScreenCanvas.GetComponentInChildren<Image>();
    //    float time = 0;
    //    var tempColor = blackScreen.color;
    //    var targetColor = blackScreen.color;
    //    targetColor.a = 0;
    //    float t;

    //    while (time <= duration)
    //    {
    //        t = time / duration;
    //        t = t * t * (3f - 2f * t);
    //        tempColor = Color.Lerp(blackScreen.color, targetColor, t);
    //        blackScreen.color = tempColor;
    //        time += Time.unscaledDeltaTime;
    //        yield return null;
    //    }

    //    tempColor.a = 0f;
    //    blackScreen.color = tempColor;
    //    _blackScreenCanvas.gameObject.SetActive(false);
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().DisableMovement();
            _blackScreenCanvas = GameObject.Find("Black Screen");
            _levelCompleteCanvas = GameObject.Find("Level Complete");

            foreach (Transform child in _blackScreenCanvas.transform)
            {
                child.gameObject.SetActive(true);
            }

            StartCoroutine(DarkenScreen(20f));
            Invoke("Stats", 3f);        
        }     
    }

    IEnumerator DarkenScreen(float duration)
    {
        Image blackScreen = _blackScreenCanvas.GetComponentInChildren<Image>();
        float time = 0;
        var tempColor = blackScreen.color;
        var targetColor = blackScreen.color;
        targetColor.a = 1;
        float t;

        while (time < duration)
        {
            t = time / duration;
            t = t * t * (3f - 2f * t);
            tempColor = Color.Lerp(blackScreen.color, targetColor, t);
            blackScreen.color = tempColor;
            time += Time.deltaTime;
            yield return null;
        }

        tempColor.a = 1f;
        blackScreen.color = tempColor; 
    }

    void Stats()
    {
        foreach (Transform child in _blackScreenCanvas.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in _levelCompleteCanvas.transform)
        {
            child.gameObject.SetActive(true);
        }
        _coinsAmount = GameObject.Find("Level Coin Amount").GetComponent<TMP_Text>();
        _coinsAmount.SetText(_itemCollection.CountOf(_coin).ToString());
    }
}