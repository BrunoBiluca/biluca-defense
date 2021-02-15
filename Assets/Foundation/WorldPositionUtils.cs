using UnityEngine;

public class WorldPositionUtils {

    private static Camera mainCamera;

    public static Vector3 GetMousePosition() {
        if(mainCamera == null) mainCamera = Camera.main;

        var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }

    public static Vector3 GetRandomPosition(float range) {
        return new Vector3(
            Random.Range(-range, range),
            Random.Range(-range, range)
        );
    }
}
