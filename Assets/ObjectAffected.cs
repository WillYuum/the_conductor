using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectAffected : MonoBehaviour
{
    private Vector3 originalScale;
    private Conductor conductor;

    [SerializeField] public List<Transform> objectsToScale;
    [SerializeField] public float scaleMultiplier = 3f;
    [SerializeField] public float scaleDownDuration = 0.5f;

    void Start()
    {
        // Store the original scale of the objects
        if (objectsToScale.Count > 0)
        {
            originalScale = objectsToScale[0].localScale;
        }

        conductor = FindObjectOfType<Conductor>();
        if (conductor != null)
        {
            print("Subscribing to OnBeat event");
            conductor.OnBeat += OnBeatHandler;
        }
        else
        {
            Debug.LogError("Conductor is null");
        }
    }


    private void OnBeatHandler()
    {
        ScaleObjects();
    }

    private void ScaleObjects()
    {
        foreach (Transform obj in objectsToScale)
        {
            obj.DOKill();

            obj.localScale = originalScale * scaleMultiplier;
            obj.DOScale(originalScale, scaleDownDuration);
        }
    }
}
