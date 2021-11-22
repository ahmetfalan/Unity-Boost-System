# Simply Boost System (Buff/Debuff with Cooldown)


You can use this system in your games as buff or debuff.



> I'm not very good at explaining like this things, sorry for mistakes.



IBuffable.cs interface looks like this and also you can use it instead of debuff:

```c#
public interface IBuffable
{
    float Value { get; set; }                 // how much will it decrease or increase
    float CurrentCooldown { get; set; }       // counter
    float MaxCoolDown { get; set; }           // durability
    bool IsActive { get; set; }               // is it active now
    GameObject ImageInPanel { get; set; }     // when pick up the buff or debuff ui timer is active and you can change the image of timer
    void Active();                            // activate buff or debuff
    void DeActive();                          // deactivate buff or debuff
    PlayerAttributes Player { get; set; }     // which player
}
```

SpeedBuff.cs looks like this:
```c#
public class SpeedBuff :IBuffable
{
    public float Value { get; set; }
    public bool IsActive { get; set; }
    public float CurrentCooldown { get; set ; }
    public float MaxCoolDown { get; set; }
    public GameObject ImageInPanel { get; set; }
    public PlayerAttributes Player { get; set; }
    public SpeedBuff(PlayerAttributes Player, bool IsActive, float Value, float MaxCoolDown, GameObject ImageInPanel) // we will call this function in movement.cs
    {
        this.Player = Player;
        this.Value = Value;
        this.MaxCoolDown = MaxCoolDown;
        this.ImageInPanel = ImageInPanel;
        this.IsActive = IsActive;
    }
    public void Active()
    {
        if (!IsActive) // if buff isn't active 
        {
            Player.Speed += Value; // and add player speed
            IsActive = true; // activate the buff or debuff
            CooldownManager.Instance.StartCooldown(this); // start cooldown
        }
        else
        {
            CurrentCooldown += MaxCoolDown; //  add time on it if currently there is a buff or debuff
        }
    }
    public void DeActive()
    {
        Player.Speed -= Value; and remove player speed
        IsActive = false; activate the buff or debuff
    }
}
```

Movement.cs looks like this:
```c#
[SerializeField]
    private PlayerAttributes playerAttributes;

    private Rigidbody2D rb;
    public GameObject speedImage;
    public Text speedText;
    public GameObject jumpImage;
    public Text jumpText;
    IBuffable speedBuff; //we get instance IBuffable, we'll call this later type of SpeedBuff
    IBuffable jumpBuff;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        speedBuff = new SpeedBuff(playerAttributes/*player*/, false/*is active while game start*/, 5/*increase value*/, 10/*durability(second)*/, speedImage/*timer ui image*/); // new speed buff
        jumpBuff = new JumpBuff(playerAttributes, false, 10, 10, jumpImage);
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * playerAttributes.Speed, rb.velocity.y);
        if (Input.GetKey("w"))
        {
            rb.velocity = new Vector2(rb.velocity.x, playerAttributes.Jump);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Speed")
        {
            Destroy(collision.gameObject);
            speedBuff.Active(); //activate the speed buff
        }
        if (collision.tag == "Jump")
        {
            Destroy(collision.gameObject);
            jumpBuff.Active();
        }
    }
```

CooldownManager.cs looks like this:
```c#
public class CooldownManager : MonoBehaviour
{
    public static CooldownManager Instance;
    
    public List<IBuffable> Buffables = new List<IBuffable>();
    
    public GameObject activeBuffs;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    public void StartCooldown(IBuffable buffable)
    {
        buffable.CurrentCooldown = buffable.MaxCoolDown; // current cooldown equal the druability
        Buffables.Add(buffable); // add list
        Instantiate(buffable.ImageInPanel, activeBuffs.transform); 
    }

    void Update()
    {
        if (Buffables.Count > 0)
        {
            for (int i = 0; i < Buffables.Count; i++)
            {
                Buffables[i].CurrentCooldown -= Time.deltaTime; // timer
                Buffables[i].ImageInPanel.SetActive(true); // activate buff ui image
                Buffables[i].ImageInPanel.GetComponentInChildren<Text>().text = Convert.ToInt32(Buffables[i].CurrentCooldown).ToString(); // timer text
                if (Buffables[i].CurrentCooldown <= 0) // if cooldown equal or less than zero
                {
                    Buffables[i].ImageInPanel.SetActive(false); // buff or debuff ui image deactive
                    Buffables[i].CurrentCooldown = 0;
                    Buffables[i].DeActive(); // buff or debuff deactive
                    Buffables.Remove(Buffables[i]); // remove the list
                }
            }
        }
    }
}
```




