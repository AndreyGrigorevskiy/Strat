using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{

    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Build[,] grid;
    private Build flyingBuild;
    private Camera mainCamera;

    private void Awake()
    {
        grid = new Build[GridSize.x*2, GridSize.y*2];
        mainCamera = Camera.main;
    }

    public void StartPlacingBuild(Build buildPrefab)
    {
        if (flyingBuild != null)
        {
            Destroy(flyingBuild.gameObject);
        }

        flyingBuild = Instantiate(buildPrefab);
    }
    
    void Update()
    {
        if(flyingBuild != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool available = true;

                if (x < -GridSize.x + flyingBuild.Size.x || x > GridSize.x - flyingBuild.Size.x) available = false;
                if (y < -GridSize.y + flyingBuild.Size.y || y > GridSize.y - flyingBuild.Size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                flyingBuild.transform.position = new Vector3(x, 0, y);
                flyingBuild.SetTranparent(available);


                if(available && Input.GetMouseButtonDown(0))
                {
                    PlaceFlyingBuild(x, y);
                }

            }

        }
    }


    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuild.Size.x; x++)
        {
            for (int y = 0; y < flyingBuild.Size.y; y++)
            {

                if (grid[GridSize.x + placeX + x, GridSize.x + placeY + y] != null) return true;
                
            }
        }

        return false;
    }

    private void PlaceFlyingBuild(int placeX, int placeY)
    {

        for (int x = 0; x < flyingBuild.Size.x; x++)
        {
            for (int y = 0; y < flyingBuild.Size.y; y++)
            {
                grid[GridSize.x + placeX + x, GridSize.x + placeY + y] = flyingBuild;
            } 
        }

        flyingBuild.SetNormal();
        flyingBuild = null;
    }
}

