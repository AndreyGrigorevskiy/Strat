using UnityEngine;

public class SpawnOnMouseClik : MonoBehaviour
{
    public GameObject entityToSpawn;
    public Camera playerCameraOnView;
    public int power;

    void Update()
    {
        
        if(Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var pos = ray.origin + ray.direction * 10.0f;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                if(hit.transform.gameObject.tag == "Enamy")
                {
                    hit.transform.gameObject.SendMessage("getHit", power);
                }
                else
                {
                    Vector3 newPosition;

                    newPosition.x = pos.x;
                    newPosition.y = 0.2f;
                    newPosition.z = pos.z;

                    Instantiate(entityToSpawn, newPosition, Quaternion.identity);
                }
            }
       
        }
        
    }

    void SetNewEntityToSpawn(GameObject newEntity)
    {
        entityToSpawn = newEntity;
    }
}
