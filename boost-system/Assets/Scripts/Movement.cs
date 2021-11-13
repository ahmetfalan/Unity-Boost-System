using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private PlayerAttributes playerAttributes;

    private Rigidbody2D rb;
    public GameObject speedImage;
    public Text speedText;
    public GameObject jumpImage;
    public Text jumpText;
    IBuffable speedBuff;
    IBuffable jumpBuff;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        speedBuff = new SpeedBuff(playerAttributes, false, 5, 10, speedImage);
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
            speedBuff.Active();
        }
        if (collision.tag == "Jump")
        {
            Destroy(collision.gameObject);
            jumpBuff.Active();
        }
    }
}
