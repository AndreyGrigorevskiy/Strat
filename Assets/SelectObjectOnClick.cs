using UnityEngine;

public class SelectObjectOnClick : MonoBehaviour
{
    public Canvas[] toolbarObjectsUi;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var pos = ray.origin + ray.direction * 10.0f;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                if (hit.transform.gameObject.tag == "Enamy")
                {
                    SetUi("enamyUi", hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "build")
                {
                    SetUi("buildUi", hit.transform.gameObject);
                }
                else if (hit.transform.gameObject.tag == "ground")
                {
                     SetUi("mainUi", hit.transform.gameObject);
                }
            }

        }

    }

    private void SetUi(string currentTag, GameObject currentGameObject)
    {
        foreach (Canvas objectUi in toolbarObjectsUi)
        {
            if (objectUi.gameObject.tag == currentTag)
            {
                objectUi.gameObject.SetActive(true);
                objectUi.GetComponent("objectContauner").SendMessage("SetGameObject", currentGameObject);
            }
            else
            {
                objectUi.gameObject.SetActive(false);
            }
        }
    }
}
