﻿using UnityEngine;

namespace Assets.Foundation.TransformUtils {
    public static class RotationUtils {

        public static Vector3 GetZRotation(Vector3 direction) {
            return new Vector3(
                0,
                0,
                Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
            );
        }

    }
}