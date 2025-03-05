using System.Collections;
using Pathfinding;
using UnityEngine;

public class Minions : MonoBehaviour
{
    Path path;
    Coroutine move;
    GameManager gameManager;
    public Seeker seeker;
    private Animator animatiorMinions;
    // public GameObject item;
    Vector3 direction;
    Vector3 stopPosition;
    float walkTime;
    float walkCounter;
    float waitTime;
    float waitCounter;
    int WalkDirection;
    public bool isWalking;
    public bool checkDead = false;
    [SerializeField] float nextWpDis;
    [SerializeField] float moveSpeed;
    void Start()
    {
        //animatiorMinions = GetComponent<>();
        walkTime = Random.Range(3, 6);
        waitTime = Random.Range(5, 7);

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
        InvokeRepeating("caculatePath", 0f, 0.5f);
    }
    void Update()
    {
        if (isWalking)
        {

            // animator.SetBool("isRunning", true);

            walkCounter -= Time.deltaTime;

            switch (WalkDirection)
            {
                case 0:
                    transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 1:
                    transform.localRotation = Quaternion.Euler(0f, 90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 2:
                    transform.localRotation = Quaternion.Euler(0f, -90, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
                case 3:
                    transform.localRotation = Quaternion.Euler(0f, 180, 0f);
                    transform.position += transform.forward * moveSpeed * Time.deltaTime;
                    break;
            }

            if (walkCounter <= 0)
            {
                stopPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isWalking = false;
                //stop movement
                transform.position = stopPosition;
                // animator.SetBool("isRunning", false);
                //reset the waitCounter
                waitCounter = waitTime;
            }


        }
        else
        {

            waitCounter -= Time.deltaTime;

            if (waitCounter <= 0)
            {
                ChooseDirection();
            }
        }

    }
    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);

        isWalking = true;
        walkCounter = walkTime;
    }
    public void DameEnemy(int damage)
    {
        gameManager.enemyConfig.hpEnemy -= damage;
        if (gameManager.enemyConfig.hpEnemy <= 0)
        {
            checkDead = true;
            gameManager.enemyConfig.hpEnemy = 0;
            // Instantiate(item, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    void caculatePath()
    {
        Vector3 target = findTarget();
        if (seeker.IsDone())
        {
            seeker.StartPath(transform.position, target, OnpathCallBack);
        }
        else
        {
            seeker.StartPath(transform.position, transform.position, OnpathCallBack);
        }
    }

    //location player
    Vector3 findTarget()
    {
        Vector3 playPos = FindObjectOfType<PlayerStatus>().transform.position;
        return playPos;
    }
    void OnpathCallBack(Path p)
    {
        if (p.error)
        {
            return;
        }
        path = p;
        moveTager();
    }
    void moveTager()
    {
        if (move != null)
        {
            StopCoroutine(move);
        }
        move = StartCoroutine(moveTagerCoroutine());
    }
    // di chuyen
    IEnumerator moveTagerCoroutine()
    {
        int currentWp = 0;
        while (currentWp < path.vectorPath.Count)
        {
            path.vectorPath[currentWp] = new Vector3(path.vectorPath[currentWp].x, 0, path.vectorPath[currentWp].z);
            direction = (path.vectorPath[currentWp] - transform.position).normalized;
            Vector3 force = direction * moveSpeed * Time.deltaTime;
            transform.position += force;
            float dis = Vector3.Distance(transform.position, path.vectorPath[currentWp]);
            if (dis < nextWpDis)
            {
                currentWp++;
            }
            // if (force.x != 0)
            // {
            //     if (force.x < 0)
            //     {
            //         enemySR.transform.localRotation = Quaternion.Euler(0, 180, 0);
            //     }
            //     else
            //     {
            //         enemySR.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //     }
            // }
            yield return null;
        }
    }
    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         FindObjectOfType<GameManager>().player = other.GetComponent<Player>();
    //         InvokeRepeating("Damage", 0, 0.3f);
    //     }
    // }
    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         FindObjectOfType<GameManager>().player = null;
    //         CancelInvoke("Damage");
    //     }
    // }
    // void Damage()
    // {
    //     int damage = Random.Range(minDamage, maxDamage);
    //     FindObjectOfType<GameManager>().TakeDamagePlayer(damage);
    // }
}
