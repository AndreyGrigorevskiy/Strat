using System.Collections;
using UnityEngine;

public enum TagsToChoose
{
    bild,
    enamy,
    friendly
}

public class EntityEvents : MonoBehaviour
{
    public float damage;
    public float maxHealth;
    public float health;
    public float attacSpeedPerSecond;
    public float moveSpeed;
    public bool  canMove;
    public float timeBetweenFind;
    public TagsToChoose[] currentTags;
    public GameObject hpBarPrefab;
    public Transform canvasTransform;
    public Transform hpBarAnchor;

    private string[] currentEntityTags;
    private GameObject[][] targets;
    private Vector3 target;
    private float waitActtacTime;
    private HPBar _hpBar;


    private void Awake()
    {
        health = maxHealth;

        var bar = Instantiate(hpBarPrefab, canvasTransform);

        _hpBar = bar.GetComponent<HPBar>();
        _hpBar.Initialize(hpBarAnchor, this);
    }

    private void Start()
    {
        currentEntityTags = new string[currentTags.Length];

        for(int i = 0; i<currentTags.Length; i++)
        {
            switch (currentTags[i])
            {
                case TagsToChoose.bild: currentEntityTags[i] = "build"; break;
                case TagsToChoose.enamy: currentEntityTags[i] = "Enamy"; break;
                case TagsToChoose.friendly: currentEntityTags[i] = "friendly"; break;   
            }
        }

        targets = new GameObject[currentEntityTags.Length][];

        waitActtacTime = 60.0f / attacSpeedPerSecond;

        StartCoroutine(findPerTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(attacSpeedPerSecond != 0)
        {
            StartCoroutine(attacPerTime(other));
        }
    }

    private IEnumerator attacPerTime(Collider other)
    {
        while(true)
        {
            var distance = Vector3.Distance(transform.position, other.transform.position);

            if (distance > 0.2f | other.gameObject == null)
            {
                StopCoroutine(attacPerTime(other));
            }

            for (int i = 0; i < currentTags.Length; i++)
            {
                if (other.tag == currentEntityTags[i])
                {
                    other.SendMessage("getHit", damage);
                }
            }
            yield return new WaitForSeconds(waitActtacTime);
        }
        
    }

    public void getHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(_hpBar);
            Destroy(gameObject);
        }
    }
  
    void FixedUpdate()
    {
        if(canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed);
        }
    }

    private IEnumerator findPerTime()
    {
        while (true)
        {
            for (int i = 0; i < targets.GetLength(0); i++)
            {
                targets[i] = GameObject.FindGameObjectsWithTag(currentEntityTags[i]);
            }

            yield return new WaitForSeconds(timeBetweenFind);

            var foundGameObject = FindClosesTarget();
            if(foundGameObject == null)
            {
                StopCoroutine(findPerTime());
                yield return 0;
            }
            target = foundGameObject.transform.position;
            target.y = transform.position.y;
        }
    }

    private GameObject FindClosesTarget()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        GameObject currentGameObject = default;
        foreach (GameObject[] currentTargetMass in targets)
        {
            foreach (GameObject currentTarget in currentTargetMass)
            {
                if(currentTarget == null)
                {
                    continue;
                }

                var currentDistance = Vector3.Distance(transform.position, currentTarget.transform.position);
                if (currentDistance < distance)
                {
                    currentGameObject = currentTarget;
                    distance = currentDistance;
                }
            }
            if(currentGameObject != default)
            {
                break;
            }
        }        

        return currentGameObject;
    }
}
