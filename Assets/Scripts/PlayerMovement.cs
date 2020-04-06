using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 0.25f;
    [SerializeField]
    float rayLength = 1.4f;
    [SerializeField]
    float rayOffsetX = 0.5f;
    [SerializeField]
    float rayOffsetY = 0.5f;
    [SerializeField]
    float rayOffsetZ = 0.5f;

    Vector3 targetPosition;
    Vector3 startPosition;
    bool moving;

    Vector3 xOffset;
    Vector3 yOffset;
    Vector3 zOffset;
    Vector3 zAxisOriginA;
    Vector3 zAxisOriginB;
    Vector3 xAxisOriginA;
    Vector3 xAxisOriginB;

    [SerializeField]
    Transform cameraRotator = null;

    void Update() {
        if (moving) {
            if (Vector3.Distance(startPosition, transform.position) > 1f) {

                float x = Mathf.Round(targetPosition.x);
                float y = Mathf.Round(targetPosition.y);
                float z = Mathf.Round(targetPosition.z);

                transform.position = new Vector3(x, y, z);

                moving = false;
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        } else {
            // After moving, we can check if we should go down a level
            if (!Physics.Raycast(transform.position - Vector3.down * 0.1f, Vector3.down, 1f)) {
                transform.position += Vector3.down;

                Debug.DrawLine(
                        transform.position - Vector3.down * 0.1f,
                        transform.position - Vector3.down * 0.1f + Vector3.down * 1f,
                        Color.yellow,
                        Time.deltaTime);
            }
        }

        // Set the ray positions every frame

        yOffset = transform.position + Vector3.up * rayOffsetY;
        zOffset = Vector3.forward * rayOffsetZ;
        xOffset = Vector3.right * rayOffsetX;

        zAxisOriginA = yOffset + xOffset;
        zAxisOriginB = yOffset - xOffset;

        xAxisOriginA = yOffset + zOffset;
        xAxisOriginB = yOffset - zOffset;

        // Draw Debug Rays
        
        Debug.DrawLine(
                zAxisOriginA,
                zAxisOriginA + Vector3.forward * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                zAxisOriginB,
                zAxisOriginB + Vector3.forward * rayLength,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                zAxisOriginA,
                zAxisOriginA + Vector3.back * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                zAxisOriginB,
                zAxisOriginB + Vector3.back * rayLength,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                xAxisOriginA,
                xAxisOriginA + Vector3.left * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xAxisOriginB,
                xAxisOriginB + Vector3.left * rayLength,
                Color.red,
                Time.deltaTime);

        Debug.DrawLine(
                xAxisOriginA,
                xAxisOriginA + Vector3.right * rayLength,
                Color.red,
                Time.deltaTime);
        Debug.DrawLine(
                xAxisOriginB,
                xAxisOriginB + Vector3.right * rayLength,
                Color.red,
                Time.deltaTime);

        // Handle player input
        if (Input.GetKeyDown(KeyCode.W)) {
            if (CanMove(Vector3.forward)) {
                targetPosition = transform.position + cameraRotator.transform.forward;
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.S)) {
            if (CanMove(Vector3.back)) {
                targetPosition = transform.position - cameraRotator.transform.forward;
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.A)) {
            if (CanMove(Vector3.left)) {
                targetPosition = transform.position - cameraRotator.transform.right;
                startPosition = transform.position;
                moving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.D)) {
            if (CanMove(Vector3.right)) {
                targetPosition = transform.position + cameraRotator.transform.right;
                startPosition = transform.position;
                moving = true;
            }
        }
    }

    bool falling;
    float lastElevation;

    // Check if the player can move

    bool CanMove(Vector3 direction) {
        if (direction.z != 0) {
            if (Physics.Raycast(zAxisOriginA, direction, rayLength)) return false;
            if (Physics.Raycast(zAxisOriginB, direction, rayLength)) return false;
        }
        else if (direction.x != 0) {
            if (Physics.Raycast(xAxisOriginA, direction, rayLength)) return false;
            if (Physics.Raycast(xAxisOriginB, direction, rayLength)) return false;
        }
        return true;
    }
}
