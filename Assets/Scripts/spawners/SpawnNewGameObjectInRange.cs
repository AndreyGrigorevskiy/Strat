using System.Collections;
using UnityEngine;


public enum FormToSpawn
{
    circle,
    square
}

public class SpawnNewGameObjectInRange : MonoBehaviour
{
    public GameObject EntityToSpawn;
    public float RangeOuter;
    public float RangeIner;
    
    public float timeBetweenSpawn;
    public bool keepSpawning;
    public FormToSpawn currentForm;

    private void Start()
    {

        if(currentForm == FormToSpawn.circle)
        {
            StartCoroutine(spawmEnamyPerTimeCirlce());
        }
        else
        {
            StartCoroutine(spawmEnamyPerTimeSquare());
        }

    }

    private IEnumerator spawmEnamyPerTimeCirlce()
    {
        while(keepSpawning)
        {
            
            yield return new WaitForSeconds(timeBetweenSpawn);

            var agle = Random.Range(-180.0f, 180.0f);
            var inRangePosition = Random.Range(RangeIner, RangeOuter);
            var ZOnCircle = Mathf.Sin(agle);
            var XOnCircle = Mathf.Cos(agle);

            Vector3 newPosition1;
            newPosition1.x = transform.position.x + (XOnCircle * inRangePosition);
            newPosition1.z = transform.position.z + (ZOnCircle * inRangePosition);
            newPosition1.y = transform.position.y;

            Instantiate(EntityToSpawn, newPosition1, Quaternion.identity);

        }
    }

    private IEnumerator spawmEnamyPerTimeSquare()
    {
        while (keepSpawning)
        {

            yield return new WaitForSeconds(timeBetweenSpawn);
          

            Instantiate(EntityToSpawn, getRandomPositionOnSqare(), Quaternion.identity);

        }
    }

    private Vector3 getRandomPositionOnSqare()
    {

        float randomZ;
        float randomX;

        float rand1;
        float rand2;

        rand1 = Random.Range(-RangeOuter, RangeOuter);
        rand2 = Random.Range(RangeIner, RangeOuter);

        if (Random.value < 0.5f)
        {
            rand2 = -rand2;
        }
        if (Random.value < 0.5f)
        {
            randomZ = -rand1;
            randomX = rand2;
        }
        else
        {
            randomZ = rand2;
            randomX = -rand1;
        }

        Vector3 newPosition;
        newPosition.x = randomX;
        newPosition.z = randomZ;
        newPosition.y = transform.position.y;

        return newPosition;
    }

}
