using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    [SerializeField] private PlayerBullet bullet;
    [SerializeField] private Text playerHpText;

    [SerializeField] private float playerHP;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float shotSpeed;

    public float HP
    {
        get { return playerHP; }
        set
        {
            if(playerHP > 0) playerHP = value;
            playerHpText.text = $"HP : {playerHP}";

            if(playerHP == 0) PlayerDie();
        }
    }

    private bool isPlayerDie = false;
    private float shotWaitTime = 0;
    private float currentTime = 0;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void FixedUpdate()
    {
        PlayerMovement(playerInput.moveInput);
    }

    private void Update()
    {   
        if (playerInput.Shot && shotWaitTime <= currentTime)
        {
            ShotBullet();
            currentTime = Time.time;
            shotWaitTime = Time.time + shotSpeed;
        }
        currentTime += Time.deltaTime;
    }

    private void PlayerMovement(Vector2 moveInput)
    {
        var moveDir = moveInput.normalized * moveSpeed;
        transform.Translate(moveDir * Time.deltaTime);
    }
    private void ShotBullet()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
        var mouseDir = mousePos - transform.position;

        var bulletDir = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, bulletDir));
        
    }
    private void PlayerDie()
    {
        isPlayerDie = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet")) HP--;
    }
}
