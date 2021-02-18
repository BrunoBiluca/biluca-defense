using UnityEngine;

public class AnglesHelper {

    public static Vector3 ConvertDirectionVector(Vector3 direction) {
        return new Vector3(
            0,
            0,
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
        );
    }

}
