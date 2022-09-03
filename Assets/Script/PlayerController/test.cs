using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
    PlayerControl playerControls;
    public InputAction stealth;
    public InputAction shoot;
    public InputAction chargeShoot;


    NewPlayer newPlayer;
    
    [SerializeField] GameObject player;
  


    [Header("Stealth")]
    [SerializeField] private bool canStealth; // 判断是否在潜行区域内
    public LayerMask StealthArea;
    public Transform StealthCheck;
    private SpriteRenderer rend;
    public float fadeOutTime = 0.5f;
    private bool toStealth = false;


    [Header("Shoot")]
    public GameObject BulletTiles1;
    public Transform Firepoint;

    
    private bool canShoot;


    [Header("ChargeShoot")]
    public GameObject ChargedBullet;
    // Start is called before the first frame update


    void Awake()
    {
        playerControls = new PlayerControl();
        newPlayer = player.GetComponent<NewPlayer>();
        

    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        canShoot = true;
    }


    void Update()
    {
        canStealth = Physics2D.OverlapCircle(StealthCheck.position, .05f, StealthArea);
        
        if (!canStealth)
        {
            toStealth = false;
        }

        if (canStealth )
        {
            if (toStealth)
            {
                Stealthing();
                canShoot=false;
            }
            else
            {
                OutStealth();
                canShoot = true;
            }
        }
        else
        {
            OutStealth();
            canShoot = true;
        }

    }


    // Update is called once per frame
    public void Stealth(InputAction.CallbackContext context)
    {
        toStealth = !toStealth;
        

        // TODO
    }

    void Stealthing()
    {

        StartCoroutine(FadeOut(GetComponent<SpriteRenderer>()));
        newPlayer.Speed = 2f;
        Physics2D.IgnoreLayerCollision(11, 13, true); // 11 means the layer number of player, 13 means that of enemy
    }
        
    void OutStealth()
    {
            StartCoroutine(BacktoPlayer(GetComponent<SpriteRenderer>()));
            newPlayer.Speed = 5f;
            Physics2D.IgnoreLayerCollision(11, 13, false);
        
    }

    IEnumerator FadeOut(SpriteRenderer _sprite) //change the transparency
    {
        Color tmpColor = _sprite.color;
        while (tmpColor.a > 0.3f)
        {
            tmpColor.a -= 1f * Time.deltaTime / fadeOutTime;

            _sprite.color = tmpColor;
            if (tmpColor.a <= 0.3f)
            {
                tmpColor.a = 0.3f;
            }
            yield return null;
        }
        _sprite.color = tmpColor;

    }

    IEnumerator BacktoPlayer(SpriteRenderer _sprite) //change the transparency from 0.3 to 1
    {
        Color tmpColor = _sprite.color;
        tmpColor.a = 1f;
        yield return null;

        _sprite.color = tmpColor;

    }


    public void Shoot(InputAction.CallbackContext value)
    {
        if (canShoot)
        {
            var bumper = (GameObject)Instantiate(BulletTiles1, Firepoint.position, Firepoint.rotation);
            Destroy(bumper, 5f);
        }

    }

        
         
            
    public void ChargeShoot(InputAction.CallbackContext value)
    {
        if (canShoot)
        {
            var bumper = (GameObject)Instantiate(ChargedBullet, Firepoint.position, Firepoint.rotation);
            Destroy(bumper, 5f);
        }
 
        
    }







    public void OnEnable()
    {
        stealth = playerControls.GamePlay.Stealth;
        stealth.Enable();
        stealth.performed += Stealth;

        shoot = playerControls.GamePlay.Shoot;
        shoot.Enable();
        shoot.performed += Shoot;

        chargeShoot = playerControls.GamePlay.ChargeShoot;
        chargeShoot.Enable();
        chargeShoot.performed += ChargeShoot;
    }

    public void OnDisable()
    {
        stealth.Disable();
        shoot.Disable();
        chargeShoot.Disable();
    }
}
