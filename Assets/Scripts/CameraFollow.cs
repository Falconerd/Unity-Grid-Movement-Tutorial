using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target;

    [SerializeField]
    float buffer = 0.1f;

    [SerializeField]
    float speed = 2.0f;

    Vector3 offset;
    float offsetDistance;

    void Start() {
        offset = target.position + transform.position;
        offsetDistance = Vector3.Distance(target.position, transform.position);
    }

    void Update() {
        Vector3 targetPosition = target.position + offset;
        Vector3 heading = targetPosition - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (distance > buffer)
            transform.position += direction * Time.deltaTime * speed;
        else
            transform.position = targetPosition;
    }
}

