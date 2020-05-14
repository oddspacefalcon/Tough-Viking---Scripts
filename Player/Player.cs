using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, ITakeDamage
{
    //array för att kunna sätta vapen och ammo, viktigt att sätta dom i ordningen som de visas i shoppen samt sizen ska motsvara antalet guns i shoppen!
    public GameObject[] AvailableGuns;
    public Projectile[] AvailableAmmo;
    private int ChosenGun;
    private int ChosenAmmo;
    public Transform ChosenGunLocation;

    private SwipeMove _swipeMove;

    private bool _isFacingRight;
    private CharacterController2D _controller;
    private float _normalizedHorizontalSpeed=0; // target speed
    public float MaxSpeed=8;
    public float SpeedAcceleratioOnGround = 10f;
    public float SpeedAccelerationInAir = .5f;

    //health variabler
    public int MaxHealth = 100;
    public int Health { get; private set; }
    // public GameObject OuchEffect; // detta objekt/prefab kommer sättas när spelaren tar damage

    //kunna skjuta
    public Projectile Projectile;
    public float FireRate; // cooldown after fire
    private float _canFireIn;
    public Transform ProjectileFireLocation;
    public GameObject FireProjectileEffect;
    //

    /*sound
    public AudioClip PlayerHitSound;
    public AudioClip PlayerShootSound;
    public AudioClip PlayerHealthSound;
    */
    //Animation 
    public Animator Animator;
   

    public bool IsDead { get; private set; }
    public static bool spawnPause = false;

    private int counter_to_ad=0; // hur många gånger till ad..

    public void Awake()
    {
        _controller = GetComponent<CharacterController2D>();
        _isFacingRight = transform.localScale.x > 0;

        // sätta häsan
        Health = MaxHealth;

        FireRate = PlayerPrefs.GetFloat("FireRate");
    }

    public void Update() //kallas varje frame
    {
       _canFireIn -= Time.deltaTime; // var update sätts can fire mindre tillsd en når 0 då kan spelaren skjuta igen.

        if (!IsDead)
            HandleInput();

        var movemenFactor = _controller.State.IsGrounded ? SpeedAcceleratioOnGround : SpeedAccelerationInAir;

        // varje frame kommer först hantera input som sätter normaliserad horisontell hastighet till 1, -1 eller 0. Därefter sätts x kompontenten för
        //spelarens hastighet som en funktion mellan max hastighet och nuvarand hastigheten. här är det Time.deltaTime som avgör hållet och hastighet
        if (IsDead)
            _controller.SetHorizontalForce(0);
        else
            _controller.SetHorizontalForce(Mathf.Lerp(_controller.Velocity.x, _normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movemenFactor));

        //Animation 
        Animator.SetBool("IsGrounded", _controller.State.IsGrounded);
        Animator.SetBool("IsDead", IsDead);
        //Animator.SetFloat("Speed", Mathf.Abs(_controller.Velocity.x) / MaxSpeed);
        
    }

    //när man slutför en level
    public void FinishLevel()
    {     
        enabled = false;
        _controller.enabled = false;
       // GetComponent<Collider2D>().enabled = false;
    }
    

    public void Kill()
    {
        
        _controller.HandleCollisions = false;
        GetComponent<Collider2D>().enabled = false;
        IsDead = true;

        _controller.SetForce(new Vector2(0, 4));

        Health = 0; // när vi dör sätts healthbaren till 0
        spawnPause = true;
        StartCoroutine(KillAndWait());

        //coins
        int tempCoin = PlayerPrefs.GetInt("TotalCoin");
        PlayerPrefs.SetInt("tempCoin", tempCoin);
        int tempCoin2 = PlayerPrefs.GetInt("tempCoin");

        PlayerPrefs.SetInt("TotalCoin", (tempCoin2 + GoldCoin.Coins));
        

    }

    public IEnumerator KillAndWait()
    {
        counter_to_ad++;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        //visar ad var tredje load
        //Counter obj = GameObject.Find("SceneCounter").GetComponent<Counter>();
        Music obj = GameObject.Find("Music Manager").GetComponent<Music>();
        int count = obj.NumberOfDeath();
        if (count % 3 == 0)
        {
            // StartRewardButton.instance.ShowInterstitial();
            StartRewardButton adObj = GameObject.Find("AdManager").GetComponent<StartRewardButton>();
            adObj.ShowInterstitial();
            //UnityAdsScript.instance.ShowVideoAd(); // visa video ad
        }
        
    }

    public void RespawnAt(Transform spawnPoint)
    {
        
        // Spawn chosen gun
        //(GameObject)Instantiate(AvailableGuns[0], ProjectileFireLocation.position, ProjectileFireLocation.rotation);
        var newObj = GameObject.Instantiate(AvailableGuns[PlayerPrefs.GetInt("ChosenGun")], ChosenGunLocation.position, Quaternion.identity);
        newObj.transform.parent = GameObject.Find("left hand").transform;

        // flippa så att man står mot höger 
        if (!_isFacingRight)
            Flip();

        IsDead = false;
        GetComponent<Collider2D>().enabled = true;
        _controller.HandleCollisions = true;

        transform.position = spawnPoint.position;
        Health = 100;
        GameManager.Instance.ResetPoints(0);
        GoldCoin.Coins = 0;

    }

    //ta skada metod

    public void TakeDamage(int damage, GameObject instagator)
    {
        /*sound when hit
        if (PlayerHitSound != null)
            AudioSource.PlayClipAtPoint(PlayerHitSound, transform.position);
        */

    //FLOATING USAGE TEXT NEDAN
    // skapar först texten vi vill ska visas, nästa syle namn (kontrollera "presentationen" för hur texten ska visas.s slutligen specifierar vi textpositioneraren (gå upp under 1.5 sek och 50 pixlar/sek)
    // "PlayerTakenDamageText" är namnet viv måste sätta på vår customStyle i vårt GameSkin i resources


    FloatingText.Show(string.Format("-{0}", damage), "PlayerTakenDamageText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60));

        //Instantiate(OuchEffect, transform.position, transform.rotation);
        Health -= damage;

        if (Health <= 0)
            LevelManager.Instance.KillPlayer();
        
    }
   


    /*give health
    public void GiveHealth(int health, GameObject instagator)
    {
        if (PlayerHealthSound != null)
            AudioSource.PlayClipAtPoint(PlayerHealthSound, transform.position); //health sound

        FloatingText.Show(string.Format("+{0}", health), "PlayergotHealthText", new FromWorldPointTextPositioner(Camera.main, transform.position, 2f, 60));
        
        Health = Mathf.Min(Health + health, MaxHealth);
    }
    */

    private void HandleInput()
    {
        /*
        if (Input.GetKey(KeyCode.D))
        {
            _normalizedHorizontalSpeed = 1;
            if (!_isFacingRight)
                Flip();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _normalizedHorizontalSpeed = -1;
            if (_isFacingRight)
                Flip();
        }
        else
        {
            _normalizedHorizontalSpeed = 0;
        }
        */
        _normalizedHorizontalSpeed = 1;




         /*if (_controller.CanJump && Input.GetMouseButtonDown(0))
         {
             _controller.Jump();
             //_controller.addJumpCount();
         }*/

        /*if (Input.GetKey(KeyCode.A))
        {
            FireProjectile();
        }*/
          




    }

    // sätta upp vapnet med tanke på vad som valts i shoppen
    public void SetupGun(int gun, int ammo)
    {
        ChosenGun = gun;
        ChosenAmmo = ammo;

    }

    //a) bestäm om vi kan skjuta. b) direction. c) instantiate. d) initalize. e) reset canFire to fire rate
    public void FireProjectile()
    {

        if (_canFireIn > 0)
                return;

        if (FireProjectileEffect != null)
            {
                var effect = (GameObject)Instantiate(FireProjectileEffect, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
                effect.transform.parent = transform;

            }
        var direction = _isFacingRight ? Vector2.right : -Vector2.right;

        // var projectile = (Projectile)Instantiate(Projectile, ProjectileFireLocation.position, ProjectileFireLocation.rotation);
        var projectile = (Projectile)Instantiate(AvailableAmmo[PlayerPrefs.GetInt("ChosenGun")], ProjectileFireLocation.position, ProjectileFireLocation.rotation);
        projectile.Initialize(gameObject, direction, _controller.Velocity);

            // projectile.transform.localScale = new Vector3(_isFacingRight ? 1 : -1, 1, 1);

            _canFireIn = FireRate;


        /*
        if(PlayerShootSound != null)
            AudioSource.PlayClipAtPoint(PlayerShootSound, transform.position);

        //Animator
        Animator.SetTrigger("Fire");
        */

    }
    


    private void Flip()
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isFacingRight = transform.localScale.x > 0;
        }
    
}
