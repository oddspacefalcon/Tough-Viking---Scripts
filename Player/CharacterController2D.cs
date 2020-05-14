using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour
{

   // public static bool touchFireButton = false;

    private const float SkinWidth = .02f;
    private const int TotalHorizontalRays = 8; // antal arrayer som pekar ut från spelaren
    private const int TotalVerticalRays = 5;

    private static readonly float SlopeLimitTangant = Mathf.Tan(75f * Mathf.Deg2Rad);

    public LayerMask PlatformMask;
    public ControllerParameters2D DefaultParameters; // tillåter oss att edita parametrarna inne i inspectorn

    public ControllerState2D State { get; private set; }
    public Vector2 Velocity { get { return _velocity; } }

    // vi kan hoppa om man inte är på marken osv...
    public int JumpCount=0;
    public bool CanJump
    {
        get
        {
            // kan hoppa om jumprestricition är satt till canjumpenywhere
            if (Parameters.JumpRestrictions == ControllerParameters2D.JumpBehavior.CanJumpAnywhere)
                return _jumpIn <= 0;

            if (Parameters.JumpRestrictions == ControllerParameters2D.JumpBehavior.CanJumpOnGround)
                return State.IsGrounded;

            if (Parameters.JumpRestrictions == ControllerParameters2D.JumpBehavior.DoubleJump)
                return JumpCount <= 0; 

            return false;
        }
    }
    

    public bool HandleCollisions { get; set; }
    public ControllerParameters2D Parameters { get { return _overrideParameters ?? DefaultParameters; } }
    public GameObject StandingOn { get; private set; }
    public Vector3 PlatformVelocity { get; private set; }


    private Vector2 _velocity;
    private Transform _transform;
    private Vector3 _localScale;
    private BoxCollider2D _boxCollider;
    private ControllerParameters2D _overrideParameters;
    private float _jumpIn;
    private GameObject _lastStandingOn;

    private Vector3
        _activeGlobalPlatformPoint,
        _activeLocalPlatformPoint;

    private Vector3
        _raycastTopLeft,
        _raycastBottomRight,
        _raycastBottomLeft;

    private float
        _verticalDistanceBetweenRays,
        _horizontalDistanceBetweenRays;

    public void Awake()
    {

        HandleCollisions = true;
        State = new ControllerState2D();
        _transform = transform;
        _localScale = transform.localScale;
        _boxCollider = GetComponent<BoxCollider2D>();

        // ta reda på avståndet mellan arrayerna på objektet
        var colliderWidth = _boxCollider.size.x * Mathf.Abs(transform.localScale.x) - (2 * SkinWidth);
        _horizontalDistanceBetweenRays = colliderWidth / (TotalVerticalRays - 1);

        var colliderHeight = _boxCollider.size.y * Mathf.Abs(transform.localScale.y) - (2 * SkinWidth);
        _verticalDistanceBetweenRays = colliderHeight / (TotalHorizontalRays - 1);
    }

    //Swipe!
    public void AddForce(Vector2 direction)
    {
       
            float angle = Mathf.Atan(direction.y / direction.x); // vinkel från direction till x axeln
            Vector2 go = new Vector2(Parameters.SwipeMagnitude, 0); // grund hastighetsvektorn som ligger längs y axeln
            float angleneg = 2 * Mathf.PI + angle; // vinkeln om vinkeln skulle vara negativ (man swipar nedåt)

            //vinklarna i sin resp cos för de två fallen 
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            float sin2 = Mathf.Sin(angleneg);
            float cos2 = Mathf.Cos(angleneg);

            // x och y komponenten i vektorn go
            float gox = go.x;
            float goy = go.y;

            // roterar vektorn i de tre olika fallen mha formeln x2 = cos(angle)*x1 - sin(angle)*y1 samt  y2 = sin(angle)*x1 + cos(angle)*y1
            if (angle > 0)
            {
                go.x = (cos * gox) - (sin * goy);
                go.y = (sin * gox) + (cos * goy);

                _velocity = go;
            }

            if (angle < 0)
            {
                go.x = (cos2 * gox) - (sin2 * goy);
                go.y = (sin2 * gox) + (cos2 * goy);

                _velocity = go;
            }

            if (angle == 0)
            {
                _velocity = go;
            }

    }

    //Jump
    public void AddForce2(Vector2 force)
    {
        _velocity = force;
    }

    public void SetForce(Vector2 force)
    {
        _velocity += force;
    }

    public void SetHorizontalForce(float x)
    {
        _velocity.x = x;
    }
    public void SetVerticalForce(float y)
    {
        _velocity.y = y;
    }

    public void Jump()
    {
       
          // TODO: Moving platform support
         AddForce2(new Vector2(2, Parameters.JumpMagnitude));
        _jumpIn = Parameters.JumpFrequency; // jumpIn används för att bestämma om spelaren kan hoppa eller ej
         JumpCount = JumpCount + 1;  
    }
    public void addJumpCount()
    {
        JumpCount = JumpCount + 1;
    }

    public void LateUpdate()
    {
        _jumpIn -= Time.deltaTime; // här minskas _jumpIn 
        if (State.IsGrounded && JumpCount > 0)
            JumpCount = 0;

        _velocity.y += Parameters.Gravity * Time.deltaTime;
        Move(Velocity * Time.deltaTime);
    }

    private void Move(Vector2 deltaMovement)
    {
        var wasGrounded = State.IsCollidingBelow;
        State.Reset();

        if (HandleCollisions)
        {
            HandlePlatforms();
            CalculateRayOrigins();

            if (deltaMovement.y < 0 && wasGrounded)
                HandleVerticalSlope(ref deltaMovement);

            if (Mathf.Abs(deltaMovement.x) > .001f)
                MoveHorizontally(ref deltaMovement);

            MoveVertically(ref deltaMovement);

            CorrectHorizontalPlacement(ref deltaMovement, true);
            CorrectHorizontalPlacement(ref deltaMovement, false);
        }

        _transform.Translate(deltaMovement, Space.World);

        if (Time.deltaTime > 0)
            _velocity = deltaMovement / Time.deltaTime;

        _velocity.x = Mathf.Min(_velocity.x, Parameters.MaxVelocity.x);
        _velocity.y = Mathf.Min(_velocity.y, Parameters.MaxVelocity.y);

        if (State.IsMovingUpSlope)
            _velocity.y = 0;
        
        if (StandingOn != null)
        {
            _activeGlobalPlatformPoint = transform.position;
            _activeLocalPlatformPoint = StandingOn.transform.InverseTransformPoint(transform.position);

            // Jumping platform mm... vi kan nu göra scripts till objekt som vi står på gör att vi kan speeda up, bli långsammare etc...
            if (_lastStandingOn != StandingOn)
            {
                if (_lastStandingOn != null)
                    _lastStandingOn.SendMessage("ControllerExit2D", this, SendMessageOptions.DontRequireReceiver);

                StandingOn.SendMessage("ControllerEnter2D", this, SendMessageOptions.DontRequireReceiver);
                _lastStandingOn = StandingOn;
            }
            else if (StandingOn != null)
                StandingOn.SendMessage("ControllerStay2D", this, SendMessageOptions.DontRequireReceiver);
        }
        else if (_lastStandingOn != null)
        {
            _lastStandingOn.SendMessage("ControllerExit2D", this, SendMessageOptions.DontRequireReceiver);
            _lastStandingOn = null;
        }
        
    }

    // såa tt vi inte glider rundor på plattformen
    // såhär kan man beräkna hastigheten på vilket objekt som helst som mamn står på.
    private void HandlePlatforms()
    {
        //om vi stod på något senaste framen så beräknas ny platform point geon vår relativa position
        if (StandingOn != null)
        {
            var newGlobalPlatformPoint = StandingOn.transform.TransformPoint(_activeLocalPlatformPoint);
            var moveDistance = newGlobalPlatformPoint - _activeGlobalPlatformPoint;

            if (moveDistance != Vector3.zero)
                transform.Translate(moveDistance, Space.World);

            //klassisk velocity formula för att få reda på vår nya hastighet
            PlatformVelocity = (newGlobalPlatformPoint - _activeGlobalPlatformPoint) / Time.deltaTime;
        }
        else
            PlatformVelocity = Vector3.zero;

        StandingOn = null;
    }

    
    //Tar hand om när vi hoppar in på plattformar från sidan genom att skapa nya horisontella vektorer 
    private void CorrectHorizontalPlacement(ref Vector2 deltaMovement, bool isRight)
    {
        var halfWidth = (_boxCollider.size.x * _localScale.x) / 2f;
        var rayOrigin = isRight ? _raycastBottomRight : _raycastBottomLeft;

        if (isRight)
            rayOrigin.x -= (halfWidth - SkinWidth);
        else
            rayOrigin.x += (halfWidth - SkinWidth);

        var rayDirection = isRight ? Vector2.right : -Vector2.right;
        var offset = 0f;

        for (var i = 1; i < TotalHorizontalRays - 1; i++) // går från andra till second last array
        {
            var rayVector = new Vector2(deltaMovement.x + rayOrigin.x, deltaMovement.y + rayOrigin.y + (i * _verticalDistanceBetweenRays));
            		//Debug.DrawRay(rayVector, rayDirection * halfWidth, isRight ? Color.cyan : Color.magenta);

            var raycastHit = Physics2D.Raycast(rayVector, rayDirection, halfWidth, PlatformMask);
            if (!raycastHit)
                continue;

            offset = isRight ? ((raycastHit.point.x - _transform.position.x) - halfWidth) : (halfWidth - (_transform.position.x - raycastHit.point.x));
        }

        deltaMovement.x += offset;
    }



    private void CalculateRayOrigins()
    {
        var size = new Vector2(_boxCollider.size.x * Mathf.Abs(_localScale.x), _boxCollider.size.y * Mathf.Abs(_localScale.y)) / 2;
        var center = new Vector2(_boxCollider.offset.x * _localScale.x, _boxCollider.offset.y * _localScale.y);

        _raycastTopLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y + size.y - SkinWidth);
        _raycastBottomRight = _transform.position + new Vector3(center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
        _raycastBottomLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);
    }

    private void MoveHorizontally(ref Vector2 deltaMovement)
    {
        var isGoingRight = deltaMovement.x > 0;
        var rayDistance = Mathf.Abs(deltaMovement.x) + SkinWidth;
        var rayDirection = isGoingRight ? Vector2.right : -Vector2.right;
        var rayOrigin = isGoingRight ? _raycastBottomRight : _raycastBottomLeft;

        for (var i = 0; i < TotalHorizontalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * _verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask);
            if (!rayCastHit)
                continue;

            if (i == 0 && HandleHorizontalSlope(ref deltaMovement, Vector2.Angle(rayCastHit.normal, Vector2.up), isGoingRight))
                break;

            deltaMovement.x = rayCastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(deltaMovement.x);

            if (isGoingRight)
            {
                deltaMovement.x -= SkinWidth;
                State.IsCollidingRight = true;
            }
            else
            {
                deltaMovement.x += SkinWidth;
                State.IsCollidingLeft = true;
            }

            if (rayDistance < SkinWidth + .0001f)
                break;
        }
    }



    // fungerar liknande som move horizontally fast med annan axel samt håller reda på om vi befinner oss  på en plattform
    private void MoveVertically(ref Vector2 deltaMovement)
    {
        var isGoingUp = deltaMovement.y > 0;
        var rayDistance = Mathf.Abs(deltaMovement.y) + SkinWidth;
        var rayDirection = isGoingUp ? Vector2.up : -Vector2.up;
        var rayOrigin = isGoingUp ? _raycastTopLeft : _raycastBottomLeft;

        rayOrigin.x += deltaMovement.x; // skjuter våra arrayer som ett offset i den riktning vi vill

        var standingOnDistance = float.MaxValue;
        for (var i = 0; i < TotalVerticalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x + (i * _horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.red); // för att se att våra arrays faktiskt dyker upp

            var raycastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, PlatformMask); // vill bara kunna kollidera med saker som är platformmask dvs inte med tex liv
            if (!raycastHit)
                continue;

            // hålla reda på vad vi står på så om vi står på tex en platform måste vi kunna åka med den
            if (!isGoingUp)
            {
                var verticalDistanceToHit = _transform.position.y - raycastHit.point.y;
                if (verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    StandingOn = raycastHit.collider.gameObject;
                }
            }

            deltaMovement.y = raycastHit.point.y - rayVector.y;
            rayDistance = Mathf.Abs(deltaMovement.y);

            // om vi träffar ett tak
            if (isGoingUp)
            {
                deltaMovement.y -= SkinWidth;
                State.IsCollidingAbove = true;
            }
            // om vi träffar marken (är på markan)
            else
            {
                deltaMovement.y += SkinWidth;
                State.IsCollidingBelow = true;
            }
            // om vi är påväg ned
            if (!isGoingUp && deltaMovement.y > .0001f)
                State.IsMovingUpSlope = true;
            // om avståndet mellan raysen (mellan spelarn och objektet) bryt
            if (rayDistance < SkinWidth + .0001f)
                break;
        }
    }

    private void HandleVerticalSlope(ref Vector2 deltaMovement)
    {
        var center = (_raycastBottomLeft.x + _raycastBottomRight.x) / 2; // ge oss center bland vertikala arrays
        var direction = -Vector2.up;

        var slopeDistance = SlopeLimitTangant * (_raycastBottomRight.x - center); 
        var slopeRayVector = new Vector2(center, _raycastBottomLeft.y);

        Debug.DrawRay(slopeRayVector, direction * slopeDistance, Color.yellow);

        var raycastHit = Physics2D.Raycast(slopeRayVector, direction, slopeDistance, PlatformMask);
        if (!raycastHit)
            return;

        // ReSharper disable CompareOfFloatsByEqualityOperator

        var isMovingDownSlope = Mathf.Sign(raycastHit.normal.x) == Mathf.Sign(deltaMovement.x);// Sign returnerar 1 om värdet positivt, egativt om  negativt och 0 om 0,
        if (!isMovingDownSlope)
            return;

        var angle = Vector2.Angle(raycastHit.normal, Vector2.up);
        if (Mathf.Abs(angle) < .0001f) // om vi ej är på sen slope
            return;

        State.isMovingDownSlope = true;
        State.SlopeAngle = angle;
        deltaMovement.y = raycastHit.point.y - slopeRayVector.y;
    }

    private bool HandleHorizontalSlope(ref Vector2 deltaMovement, float angle, bool isGoingRight)
    {
        if (Mathf.RoundToInt(angle) == 90) // vill inte kunna röra oss upp för rak vägg
            return false;

        if (angle > Parameters.SlopeLimit) // För brant backe kan vi ej ta oss upp för
        {
            deltaMovement.x = 0;
            return true;
        }

        if (deltaMovement.y > .07f)
            return true;

        deltaMovement.x += isGoingRight ? -SkinWidth : SkinWidth;
        deltaMovement.y = Mathf.Abs(Mathf.Tan(angle * Mathf.Deg2Rad) * deltaMovement.x);
        State.IsMovingUpSlope = true;
        State.IsCollidingBelow = true;
        return true;

    }
    
        // OnTriggerEnter och exit tar hand om vi åker in i tex vatten (sätter andra fysik parametrar)
        public void OnTriggerEnter2D(Collider2D other)
        {
            var parameters = other.gameObject.GetComponent<ControllerPhysicsVolume2D>();
            if (parameters == null)
                return;

           _overrideParameters = parameters.Parameters;
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            var parameters = other.gameObject.GetComponent<ControllerPhysicsVolume2D>();
            if (parameters == null)
                return;

            _overrideParameters = null;
        }
        
    
         
}
