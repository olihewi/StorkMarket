using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraPosition))]
public class CameraPositionEditor : Editor
{
    public CameraPosition camPos
    {
        get { return (CameraPosition) target; }
    }
    public void OnSceneGUI()
    {
        foreach (var pos in camPos.cameraPositions)
        {
            var pos2 = pos.position;
            Vector3 offset = new Vector3(pos.cameraSize/16.0F*9.0F,pos.cameraSize, 0.0F);
            Vector3[] border = new[]
            {
                pos2 - offset,
                pos2 - new Vector3(offset.x, 0.0F, 0.0F) + new Vector3(0.0F, offset.y, 0.0F),
                pos2 + offset,
                pos2 + new Vector3(offset.x, 0.0F, 0.0F) - new Vector3(0.0F, offset.y, 0.0F),
            };
            Handles.DrawLines(new []
            {
                border[0], border[1],
                border[1], border[2],
                border[2], border[3],
                border[3], border[0]
            });
        }
            
    }
}
