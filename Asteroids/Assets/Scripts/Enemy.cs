using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    private float rayLength;
    private Vector3 direction;
    [SerializeField] UnityEvent OnDeath;
    private LayerMask playerMask;
    [SerializeField] private LayerMask[] repel;
    [SerializeField] private LayerMask[] pushAway;

    Player player;
    enum MoveToTarget {
        random,
        player
    }
    [SerializeField] MoveToTarget moveTo;
    private void Start()
    {
        rayLength = GetComponent<PolygonCollider2D>().bounds.extents.x * 2f;
        Debug.DrawRay(transform.position,Vector2.up * rayLength);
        player = FindObjectOfType<Player>();
        playerMask.value = player.gameObject.layer;

        if (moveTo == MoveToTarget.random)
            FindRandomDirection();
    }

    private void Update()
    {
        FireRayCast(); // casting rays to find and avoid other enemyes/rocks
        MoveToTheTarget(moveTo);
    }

    private void FireRayCast()
    {
        float angle = 0f;
        for (int i = 0; i < 8; i++)
        {
            Vector3 target2 = new Vector2(rayLength * Mathf.Cos(angle * Mathf.Deg2Rad), rayLength * Mathf.Sin(angle * Mathf.Deg2Rad));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target2, rayLength);
            angle += 45;
            Debug.DrawRay(transform.position,target2.normalized * rayLength);
            if (hit)
            {
                foreach (LayerMask layer in repel) // check if we repel this object
                {
                    if (Mathf.Pow(2,hit.transform.gameObject.layer) == layer)
                    {
                        Vector3 vector3 = hit.transform.position - transform.position;
                        hit.transform.Translate(vector3 * Time.deltaTime * 3f);
                    }
                }
                foreach (LayerMask layer in pushAway) // check if we push ourself away from the object
                {
                    if (Mathf.Pow(2,hit.transform.gameObject.layer) == layer)
                    {
                        Vector3 vector3 = transform.position - hit.transform.position;
                        transform.Translate(vector3 * Time.deltaTime * 3f);
                    }
                }
            }
        }
    }

    private void MoveToTheTarget( MoveToTarget moveTo )
    {
        if (moveTo == MoveToTarget.player)
        {
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,Time.deltaTime * speed);
        }

        if (moveTo == MoveToTarget.random)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }
    }

    private void FindRandomDirection()
    {
        Vector3 center = Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f));
        direction = center - transform.position;
        float randomAngle = Random.Range(-15,15);
        float newX = Mathf.Cos(randomAngle * Mathf.Deg2Rad) * direction.x - Mathf.Sin(randomAngle * Mathf.Deg2Rad) * direction.y;
        float newY = Mathf.Sin(randomAngle * Mathf.Deg2Rad) * direction.x + Mathf.Cos(randomAngle * Mathf.Deg2Rad) * direction.y;
        direction = new Vector3(newX,newY);
        direction = direction.normalized;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}
