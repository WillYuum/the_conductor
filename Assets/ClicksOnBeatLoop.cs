using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ClicksOnBeatLoop : MonoBehaviour
{
    private Conductor _conductor;

    [SerializeField] private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        _conductor = GameObject.Find("Conductor").GetComponent<Conductor>();
    }


    void Update()
    {
        //Check if any mouse button is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            CheckIfClickedOnBeat();
        }
    }


    private void CheckIfClickedOnBeat()
    {
        if (_conductor.CurrentlyNearBeat())
        {
            GameObject text = new("Text");
            text.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Text textMesh = text.AddComponent<Text>();
            textMesh.text = "Click on beat!";
            textMesh.fontSize = 25;
            textMesh.color = Color.black;
            textMesh.font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;

            float newPos = text.transform.position.y + 100;
            text.transform.DOMoveY(newPos, 1).OnComplete(() => Destroy(text));

            text.transform.parent = canvas.transform;
        }
    }
}
