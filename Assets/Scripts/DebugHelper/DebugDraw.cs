using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDraw {

    public static void DrawRectangle(Vector3 position, Vector3 size, Color color) {
        // Top line
        Debug.DrawLine(
            new Vector3(position.x - size.x / 2, position.y - size.y / 2, position.z),
            new Vector3(position.x + size.x / 2, position.y - size.y / 2, position.z),
            color
        );
        // BottomLine
        Debug.DrawLine(
            new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z),
            new Vector3(position.x + size.x / 2, position.y + size.y / 2, position.z),
            color
        );
        // Left Line
        Debug.DrawLine(
            new Vector3(position.x - size.x / 2, position.y - size.y / 2, position.z),
            new Vector3(position.x - size.x / 2, position.y + size.y / 2, position.z),
            color
        );
        // Right Line
        Debug.DrawLine(
            new Vector3(position.x + size.x / 2, position.y - size.y / 2, position.z),
            new Vector3(position.x + size.x / 2, position.y + size.y / 2, position.z),
            color
        );
    }

}
