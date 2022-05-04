using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    private Color defaultColor;

    private void Awake()
    {
        defaultColor = MainRenderer.material.color;
    }

    public void SetTranparent(bool availabe)
    {
        if (availabe)
        {
            MainRenderer.material.color = Color.green;
        }
        else
        {
            MainRenderer.material.color = Color.red;
        }
    }

    public void SetNormal()
    {
        MainRenderer.material.color = defaultColor;
    }

    private void OnDrawGizmos()
    {
       for (int x = 0; x<Size.x;x++)
       {
            for(int y = 0; y<Size.y; y++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y),new Vector3(1, .1f, 1));
            }
       }
    }
}
