using MizukiTool.AStar;
using MizukiTool.Audio;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float Speed;
    public float JumpForce;
    private PlayerAnimator playerAnimator;
    [HideInInspector]
    public PointMod PointM
    {
        get
        {
            return SOManager.colorSO.GetPointMod(ColorMod);
        }
    }
    public ColorEnum ColorMod;
    #region Animator相关
    private bool _isGroud;
    public bool IsGround
    {
        get
        {
            return _isGroud;
        }
        set
        {
            _isGroud = value;
            playerAnimator.IsGround = value;
        }
    }
    private bool _isWalking;
    public bool IsWalking
    {
        get
        {
            return _isWalking;
        }
        set
        {
            _isWalking = value;
            playerAnimator.IsWalking = value;
        }
    }
    #endregion
    public bool IsLeftWall;
    public bool IsRightWall;
    public bool IsPlayerDead = false;
    public bool IsPlayerWin = false;
    public float StayResetTime = 2f;
    public float StayResetTimeCounter = 2f;
    public int OriginalDirection = 1;
    public AudioEnum PlayerDeadAudio;
    // Update is called once per frame
    private void Awake()
    {
        Instance = this;
        playerAnimator = GetComponent<PlayerAnimator>();
    }
    void Update()
    {
        Jump();
        CheckPlayerDead();
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
        if (KeyboardSet.IsPressing(KeyEnum.Left) && !IsLeftWall)
        {
            transform.Translate(Vector3.left * Speed * Time.deltaTime);
            IsWalking = true;
            transform.localScale = new Vector3(-OriginalDirection, 1, 1);
        }
        else if (KeyboardSet.IsPressing(KeyEnum.Right) && !IsRightWall)
        {
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
            IsWalking = true;
            transform.localScale = new Vector3(OriginalDirection, 1, 1);
        }
        else
        {
            IsWalking = false;
        }

    }
    private void Jump()
    {

        if ((KeyboardSet.IsKeyDown(KeyEnum.Jump) || KeyboardSet.IsKeyDown(KeyEnum.Up)) && IsGround)
        {
            playerAnimator.Jump();
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpForce);
            AudioUtil.Play(AudioEnum.SE_Player_Jump, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
        }
    }

    private void CheckIsGround()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 1.1f, 1 << this.gameObject.layer);
        if (hits.Length > 1)
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }
    }
    private void CheckIsWall()
    {
        RaycastHit2D[] hits1 = Physics2D.RaycastAll(transform.position, Vector2.right, .5f, 1 << this.gameObject.layer);
        RaycastHit2D[] hits2 = Physics2D.RaycastAll(transform.position, Vector2.left, .5f, 1 << this.gameObject.layer);
        //Debug.Log(hits1.Length);
        if (hits2.Length > 1)
        {
            IsLeftWall = true;
        }
        else
        {
            IsLeftWall = false;
        }
        if (hits1.Length > 1)
        {
            IsRightWall = true;
        }
        else
        {
            IsRightWall = false;
        }
    }
    public void TryReset()
    {
        if (StayResetTimeCounter > 0 && KeyboardSet.IsPressing(KeyEnum.Reset))
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
        AstarManagerSon.Instance.CheckAllPointNeighbour();
    }
    public void CheckPlayerDead()
    {
        if (IsPlayerDead || IsPlayerWin)
        {
            return;
        }
        if (transform.position.y < -4)
        {
            IsPlayerDead = true;
            Debug.Log("Player Dead");
            AudioUtil.Play(PlayerDeadAudio, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
            LevelSceneManager.Instance.Reset();
        }
        Point point = AstarManagerSon.Instance.map.GetPointOnMap(transform.position);
        if (point == null)
        {
            return;
        }
        /*if (point.Mod == this.PointM)*/
        if (IsLeftWall && IsRightWall)
        {
            IsPlayerDead = true;
            Debug.Log("Player Dead");
            AudioUtil.Play(PlayerDeadAudio, AudioMixerGroupEnum.Effect, AudioPlayMod.Normal);
            LevelSceneManager.Instance.Reset();
        }
    }
}
