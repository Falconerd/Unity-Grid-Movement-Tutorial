using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject cameraRotator = null;

    [SerializeField]
    RotationDirection targetDirection = RotationDirection.forward;

    [SerializeField]
    RotationDirection exitDirection = RotationDirection.forward;

    CameraRotation cameraRotation = null;

    void Start() {
        cameraRotation = cameraRotator.GetComponent<CameraRotation>();
        GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            cameraRotation.RotateTo(Direction.ToVector(targetDirection));
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            cameraRotation.RotateTo(Direction.ToVector(exitDirection));
        }
    }
}


























