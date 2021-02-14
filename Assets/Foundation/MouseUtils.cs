using UnityEngine;

public class MouseUtils {

    private static Camera mainCamera;

    public static Vector3 GetWorldPosition() {
        if(mainCamera == null) mainCamera = Camera.main;

        var worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0f;
        return worldPosition;
    }
}
