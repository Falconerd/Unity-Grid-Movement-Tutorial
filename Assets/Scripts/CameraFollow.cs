using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target = null;

    [SerializeField]
    float buffer = 0.05f;

    [SerializeField]
    float outerBuffer = 1.5f;

    [SerializeField]
    float speed = 2.0f;

    Vector3 offset;
    float offsetDistance;
    bool moving;

    void Start() {
        offset = target.position + transform.position;
        offsetDistance = Vector3.Distance(target.position, transform.position);
    }

    void Update() {
        Vector3 targetPosition = target.position + offset;
        Vector3 heading = targetPosition - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (distance > outerBuffer)
            moving = true;

        if (moving) {
            if (distance > buffer) {
                transform.position += direction * Time.deltaTime * speed;
            } else {
                transform.position = targetPosition;
                moving = false;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(target.position + offset, buffer);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position + offset, outerBuffer);
    }
}

