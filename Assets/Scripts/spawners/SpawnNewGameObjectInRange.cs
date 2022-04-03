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
            StartCoroutine(spawmEnamyPerTimesquare());
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

    private IEnumerator spawmEnamyPerTimesquare()
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


        if(Random.value < 0.25f)
        {
            randomZ = Random.Range(RangeIner, RangeOuter);
            randomX = Random.Range(-RangeOuter, RangeOuter);
        }
        else if (Random.value < 0.5f)
        {
            randomZ = Random.Range(-RangeOuter, -RangeIner);
            randomX = Random.Range(-RangeOuter, RangeOuter);
        }
        else if (Random.value < 0.75f)
        {
            randomZ = Random.Range(-RangeOuter, RangeOuter);
            randomX = Random.Range(-RangeOuter, -RangeIner);
        }
        else
        {
            randomZ = Random.Range(-RangeOuter, RangeOuter);
            randomX = Random.Range(RangeIner, RangeOuter);
        }

        Vector3 newPosition;
        newPosition.x = transform.position.x + randomX;
        newPosition.z = transform.position.z + randomZ;
        newPosition.y = transform.position.y;

        return newPosition;
    }

}
