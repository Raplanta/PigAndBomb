using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang : MonoBehaviour
{
    public Enemy enemyScript;

    void Start()
    {
        StartCoroutine(Explosion());
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Instance.PlayerDie();
        }

        if (other.gameObject.tag == "Enemy")
        {
            Enemy.objectExploded = other.gameObject;
        }
    }

    private IEnumerator Explosion()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
