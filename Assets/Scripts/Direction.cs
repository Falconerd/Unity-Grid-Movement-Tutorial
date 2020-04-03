using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationDirection {
    forward,
    left,
    back,
    right
}

public static class Direction
{
    public static Vector3 ToVector(RotationDirection direction) {
        switch (direction) {
        case RotationDirection.forward:
            return Vector3.forward;
        case RotationDirection.left:
            return Vector3.left;
        case RotationDirection.back:
            return Vector3.back;
        case RotationDirection.right:
            return Vector3.right;
        default:
            return Vector3.zero;
        }
    }
}
























