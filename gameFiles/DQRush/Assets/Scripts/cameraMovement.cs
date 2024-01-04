using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{

    private Vector3 offset;
    [SerializeField] private Transform player;
    [SerializeField] private float smTime;

    private Vector3 currentVel = Vector3.zero;

    private void Awake()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVel, smTime);
    }

}
