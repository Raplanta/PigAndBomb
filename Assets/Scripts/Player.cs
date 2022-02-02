using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {
    None = 0,
    MoveRight = 1,
    MoveLeft = 2,
    MoveUp = 3,
    MoveDown = 4,
    Bomb = 5
}

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField] private float speed; 
    [SerializeField] private Sprite[] moveSprite;  
    [SerializeField] private GameObject bombPrefab;

    private static Player _instancePlayer;

    GameObject bombInstantiate;

    private int directionRight = 1;
    private int directionUp = 1;
    private int onMovingHorizontal = 0;
    private int onMovingVertical = 0;
    private bool onSetBomb = false;

    public static Player Instance
    {
        get 
        {
            return _instancePlayer;
        }

        private set 
        {
            _instancePlayer = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Move();
        SetBomb();
    }

    public void OnDoAction(int actionId) 
    {
        OnDoAction((ActionType)actionId);
    }

    public void OnDoAction(ActionType action) 
    {
        switch (action)
        {
            case ActionType.MoveRight:
                this.directionRight = 1;
                this.onMovingHorizontal = 1;
                this.onMovingVertical = 0;
                spriteRenderer.sprite = moveSprite[0];
                break;

            case ActionType.MoveLeft:
                this.directionRight = -1;
                this.onMovingHorizontal = 1;
                this.onMovingVertical = 0;
                spriteRenderer.sprite = moveSprite[1];
                break;

            case ActionType.MoveUp:
                this.directionUp = 1;
                this.onMovingHorizontal = 0;
                this.onMovingVertical = 1;
                spriteRenderer.sprite = moveSprite[2];
                break;

            case ActionType.MoveDown:
                this.directionUp = -1;
                this.onMovingHorizontal = 0;
                this.onMovingVertical = 1;
                spriteRenderer.sprite = moveSprite[3];
                break;

            case ActionType.Bomb:
                this.onSetBomb = true;
                break;

            case ActionType.None:
                this.onMovingHorizontal = 0;
                this.onMovingVertical = 0;
                break;
        }
    }

    private void Move()
    {
        rigidbody.velocity = new Vector2(this.onMovingHorizontal * this.directionRight * speed * Time.fixedDeltaTime, 
            this.onMovingVertical * this.directionUp * speed * Time.fixedDeltaTime);
    }

    private void SetBomb()
    {
        if (this.onSetBomb)
        {
            bombInstantiate = (GameObject)Instantiate(bombPrefab, transform.position, transform.rotation);
            this.onSetBomb = false;
        }
    }

    public void PlayerDie()
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade() 
    {
        float transparency = 0f;
        Color playerColor = spriteRenderer.material.color;

        while (transparency > 0f)
        {
            transparency -= Time.deltaTime * 0.04f; 
            playerColor.a = transparency;
            yield return 0;
        }

        Destroy(gameObject);
    }

}
