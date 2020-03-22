using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform target = null;

    [SerializeField]
    float innerBuffer = 0.05f;

    [SerializeField]
    float outerBuffer = 1.5f;

    [SerializeField]
    float speed = 2.0f;

    Vector3 offset;
    bool moving;

    void Start() {
        offset = target.position + transform.position;
    }

    void Update() {
        Vector3 targetPosition = target.position + offset;
        Vector3 heading = targetPosition - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        if (distance > outerBuffer)
            moving = true;

        if (moving) {
            if (distance > innerBuffer) {
                transform.position += direction * Time.deltaTime * speed;
            } else {
                transform.position = targetPosition;
                moving = false;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position + offset, innerBuffer);
        Gizmos.DrawWireSphere(target.position + offset, outerBuffer);

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 0.05f);
    }
}

