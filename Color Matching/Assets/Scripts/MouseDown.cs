using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseDown : MonoBehaviour
{
    float tweenTime;

    void OnMouseDown()
    {
        if (gameObject.name == "startbutton" || gameObject.name == "endbutton")
        {
            Destroy(gameObject);
            LoadDots.gameState = "Load";
            LoadDots.startLoaded = false;
            LoadDots.endLoaded = false;
        }
        else
        {
            //VisualFeedback(gameObject);
            Tween(gameObject);
            Debug.Log("Loaded");
            LoadDots.ClickedOn(gameObject);
        }

    }
    

    void VisualFeedback(GameObject obj)
    {
        Color objColor = obj.GetComponent<Renderer>().material.color;
        var red = objColor.r * 1.5f;
        var blue = objColor.b * 1.5f;
        var green = objColor.g * 1.5f;

        Color darkenedColor = new Color(
            red,
            green,
            blue,
            1
        );

        obj.GetComponent<Renderer>().material.color = darkenedColor;
    }

    void Tween(GameObject obj)
    {
        LeanTween.cancel(obj);
        transform.localScale = Vector3.one;
        LeanTween.scale(obj, Vector3.one * 1.5f, 2).setEasePunch();
    }

    void Update() {

    }
}
