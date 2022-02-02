using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] destinationPoints;
    [SerializeField] private Sprite[] moveSprite;
    [SerializeField] private Sprite[] dirtyMoveSprite;
    [SerializeField] private float speed;
    
    private Sprite[] currentSprite;
    private float currentSpeed;
    public static GameObject objectExploded;

    private SpriteRenderer spriteRenderer;

    private int destinationPoint;
    private float distancePlayer = 3;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentSprite = new Sprite[moveSprite.Length];
        currentSprite = moveSprite;
        currentSpeed = speed;

        transform.position = destinationPoints[0].position;
        destinationPoint = 1;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Instance.PlayerDie();
        }
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        transform.position = Vector2.MoveTowards(transform.position, destinationPoints[destinationPoint].position, currentSpeed * Time.deltaTime);

        destinationPoint = transform.position == destinationPoints[destinationPoint].position ? Mathf.Abs(destinationPoint - 1) : destinationPoint;
        spriteRenderer.sprite = currentSprite[destinationPoint];

        if (objectExploded == gameObject)
        {
            StartCoroutine(Dirty());
        }
    }

    public void EnemyBecomeDirty()
    {
        StartCoroutine(Dirty());
    }

    private IEnumerator Dirty()
    {
        currentSprite = dirtyMoveSprite;
        currentSpeed = speed / 2;

        yield return new WaitForSeconds(5f);

        currentSprite = moveSprite;
        currentSpeed = speed;
    }
}
