using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour, ITakeDamage, IPlayerRespawnListener {

    public float Speed;
    public float FireRate = 1;
    public Projectile Projectile;
    public GameObject DestroyedEffect;
    public int PointsToGivePlayer;

    private CharacterController2D _controller; // tar hand om interaktionne nmed plattformarna
    private Vector2 _direction; // när han träffar en vägg flippar han och vänder sig om 
    private Vector2 _startPosition; // hålla koll på vart han träffades
    private float _canFireIn;

    public void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _direction = new Vector2(-1,0);
        _startPosition = transform.position;
    }

    public void Update()
    {
        _controller.SetHorizontalForce(_direction.x * Speed);
        //colliderar med höger eller vänster?
        if(_direction.x < 0 && _controller.State.IsCollidingLeft || (_direction.x>0 && _controller.State.IsCollidingRight))
        {
            _direction = -_direction;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        //om han kan skjuta
        if ((_canFireIn -= Time.deltaTime) > 0)
            return;
        //har spelaren möjlighet att bli träffad?
        var raycast = Physics2D.Raycast(transform.position, _direction, 10, 1 << LayerMask.NameToLayer("Player"));
        if (!raycast)
            return;

        var projectile = (Projectile)Instantiate(Projectile, transform.position, transform.rotation);
        projectile.Initialize(gameObject, _direction, _controller.Velocity);
        _canFireIn = FireRate;

        transform.Translate(Vector3.forward * Time.deltaTime);
        transform.Translate(Vector3.up * Time.deltaTime, Space.World);

    }
   
    public void TakeDamage(int damage, GameObject instigator)
    {
        if(PointsToGivePlayer != 0)
        {
            var projectile = instigator.GetComponent<Projectile>();
            if (projectile != null && projectile.Owner.GetComponent<Player>() != null)
            {
                GameManager.Instance.AddPoints(PointsToGivePlayer);
                FloatingText.Show(string.Format("+{0}", PointsToGivePlayer), "PointStarText", new FromWorldPointTextPositioner(Camera.main, transform.position, 1.5f, 50));

            }
        }

        Instantiate(DestroyedEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    // när enemy respawnar
    public void OnPlayerRespawnInThisCheckpoint(Checkpoint checkpoint, Player player)
    {
        _direction = new Vector2(-1, 0);
        transform.localScale= new Vector3(1, 1, 1);
        gameObject.SetActive(true); 
    }

   
}
