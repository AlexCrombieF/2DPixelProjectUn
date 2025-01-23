using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParllexEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Transform followTarget;

    Vector2 startingPosition;

    float starting2;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    void Start()
    {
        startingPosition = transform.position;
        starting2 = transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, starting2);
    }
}
