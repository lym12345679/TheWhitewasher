using MizukiTool.AStar;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    [HideInInspector]
    public PointMod PointM
    {
        get
        {
            return SOManager.colorToPointModSO.GetPointMod(ColorMod);
        }
    }
    public ColorEnum ColorMod;
    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;
    public bool isPlayerDead = false;
    public float StayResetTime = 2f;
    public float StayResetTimeCounter = 2f;
    // Update is called once per frame
    void Update()
    {
        Jump();
        //CheckPlayerDead();
    }
    void FixedUpdate()
    {
        CheckIsGround();
        Move();
        TryReset();
    }
    void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer(PointM.ToString());
    }
    void OnValidate()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = SOManager.colorSO.GetColor(ColorMod);
    }
    private void Move()
    {
        CheckIsWall();
        if (Input.GetKey(KeyCode.A) && !isLeftWall)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) && !isRightWall)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
        }
    }

    private void CheckIsGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, .5f, 1 << this.gameObject.layer);
        if (hits.Length > 1)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    private void CheckIsWall()
    {
        RaycastHit2D[] hits1 = Physics2D.RaycastAll(transform.position, Vector2.right, .7f, 1 << this.gameObject.layer);
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(transform.position, Vector2.left, .7f, 1 << this.gameObject.layer);
        //Debug.Log(hits1.Length);
        if (hits2.Length > 1)
        {
            isLeftWall = true;
        }
        else
        {
            isLeftWall = false;
        }
        if (hits1.Length > 1)
        {
            isRightWall = true;
        }
        else
        {
            isRightWall = false;
        }
    }
    public void TryReset()
    {
        if (StayResetTimeCounter > 0 && Input.GetKey(KeyCode.R))
        {
            StayResetTimeCounter -= Time.fixedDeltaTime;
        }
        else
        {
            StayResetTimeCounter = StayResetTime;
        }

        if (StayResetTimeCounter <= 0)
        {
            LevelSceneManager.Instance.Reset();
        }
    }
    public void SetColorMod(ColorEnum colorEnum)
    {
        ColorMod = colorEnum;
        this.gameObject.layer = LayerMask.NameToLayer(PointM.ToString());
        this.gameObject.GetComponent<SpriteRenderer>().color = SOManager.colorSO.GetColor(ColorMod);
    }
    public void CheckPlayerDead()
    {
        if (isPlayerDead)
        {
            return;
        }
        if (transform.position.y < -10)
        {
            isPlayerDead = true;
            Debug.Log("Player Dead");
            LevelSceneManager.Instance.Reset();
        }
        Point point = AstarManager.Instance.map.GetPointOnMap(transform.position);
        if (point == null)
        {
            return;
        }
        if (point.Mod == this.PointM)
        {
            isPlayerDead = true;
            Debug.Log("Player Dead");
            LevelSceneManager.Instance.Reset();
        }
    }
}
