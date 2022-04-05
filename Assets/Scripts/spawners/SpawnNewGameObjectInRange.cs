using System.Collections;
using UnityEngine;


public enum FormToSpawn
{
    circle,
    square,
    triangle
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
            StartCoroutine(spawmEnamyPerTime());
    }

    private IEnumerator spawmEnamyPerTime()
    {
        while (keepSpawning)
        {

            yield return new WaitForSeconds(timeBetweenSpawn);

            switch(currentForm)
            {
                case FormToSpawn.circle     :
                    Instantiate(EntityToSpawn, getRandomPositionOnCircle(), Quaternion.identity);
                    break;
                case FormToSpawn.square     :
                    Instantiate(EntityToSpawn, getRandomPositionOnSqare(), Quaternion.identity);
                    break;
                case FormToSpawn.triangle   :
                    Instantiate(EntityToSpawn, getRandomPositionOnTriangle(), Quaternion.identity);
                    break;
            }

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
        newPosition.x = transform.position.x + randomX;
        newPosition.z = transform.position.z + randomZ;
        newPosition.y = transform.position.y;

        return newPosition;
    }

    private Vector3 getRandomPositionOnCircle()
    {
        var agle = Random.Range(-180.0f, 180.0f);
        var inRangePosition = Random.Range(RangeOuter, RangeOuter);
        var ZOnCircle = Mathf.Sin(agle);
        var XOnCircle = Mathf.Cos(agle);

        Vector3 newPosition;
        newPosition.x = transform.position.x + (XOnCircle * inRangePosition);
        newPosition.z = transform.position.z + (ZOnCircle * inRangePosition);
        newPosition.y = transform.position.y;

        return newPosition;
    }
    private Vector3 getRandomPositionOnTriangle()
    {
        float randomZ;
        float randomX;

        randomX = Random.Range(-RangeOuter, RangeOuter);
        randomZ = Random.Range(RangeIner, RangeOuter);

        var angle = 90 * Random.Range(0, 3);
        float[,] A = new float[2, 2] { { Mathf.Cos(angle), Mathf.Sin(angle) }, { -Mathf.Sin(angle), Mathf.Cos(angle)} };
        float[] B = new float[2] { randomX, randomZ };
        float[] C = new float[2] { 0, 0};

        for (int i = 0; i<2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                C[i] += A[i, j] * B[j];
            }
        }

        randomX = C[0];
        randomZ = C[1];

        Vector3 newPosition;
        newPosition.x = transform.position.x + randomX;
        newPosition.z = transform.position.z + randomZ;
        newPosition.y = transform.position.y;

        return newPosition;
    }

}
