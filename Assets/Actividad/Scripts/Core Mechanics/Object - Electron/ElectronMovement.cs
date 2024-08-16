using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronMovement : MonoBehaviour
{
    private IAvatar localAvatar;

    [Header("Chance Ranges for Movement Parameters")]
    [SerializeField] private float minRadius;
    [SerializeField] private float maxRadius;
    [SerializeField] private float minRadialSpeed;
    [SerializeField] private float maxRadialSpeed;
    [SerializeField] private float minInclinationAngle;
    [SerializeField] private float maxInclinationAngle;
    [SerializeField] private float minSizeMultiplier;
    [SerializeField] private float maxSizeMultiplier;

    [Header("Movement Parameters")]
    [SerializeField] private float radius;
    [SerializeField] private float radialSpeed;
    [SerializeField] private float inclinationAngle;
    [SerializeField] private float sizeMultiplier;

    [Header("Components")]
    [SerializeField] private GameObject electronObject;

    [Header("States")]
    [SerializeField] private float angle;

    private void Start()
    {
        localAvatar = SpatialBridge.actorService.localActor.avatar;

        SetParameters();
        SetSize();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void SetParameters()
    {
        radius = Random.Range(minRadius, maxRadius);
        radialSpeed = Random.Range(minRadialSpeed, maxRadialSpeed);
        inclinationAngle = Random.Range(minInclinationAngle, maxInclinationAngle);
        sizeMultiplier = Random.Range(minSizeMultiplier, maxSizeMultiplier);

        angle = Random.Range(0f, 360f);
    }

    private void SetSize()
    {
        electronObject.transform.localScale *= sizeMultiplier;
    }

    private void UpdatePosition()
    {
        angle += radialSpeed * Time.deltaTime;

        if (angle >= 360f) angle -= 360f;

        Vector2 positionInCircle = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
        float elevation = 1f + radius * Mathf.Cos(angle * Mathf.Deg2Rad) * Mathf.Tan(inclinationAngle * Mathf.Deg2Rad);

        transform.position = new Vector3(positionInCircle.x, elevation, positionInCircle.y) + localAvatar.position;
    }
}
