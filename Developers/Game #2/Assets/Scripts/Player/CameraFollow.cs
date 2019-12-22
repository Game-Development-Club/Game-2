using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;
    
    [Range(0f, 1f)]
    public float smoothing = .125f;

    private void FixedUpdate()
    {
        offset = new Vector3(0f, 0f, transform.position.z);

        Vector3 pos = target.position + offset;
        Vector3 smoothPos = Vector3.Lerp(transform.position, pos, smoothing);
        transform.position = smoothPos;
    }
}
