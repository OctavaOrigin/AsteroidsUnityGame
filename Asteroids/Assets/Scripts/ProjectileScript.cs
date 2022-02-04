using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float speed;
    public Vector3 vector;
    [SerializeField] float lifeTime;

    private void Start()
    {
        StartCoroutine(KillYourSelf());
    }

    IEnumerator KillYourSelf()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.position += vector * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        
        Destroy(gameObject);
    }

}
