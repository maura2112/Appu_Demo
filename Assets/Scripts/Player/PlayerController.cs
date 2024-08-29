using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    protected static PlayerController instance;
    public static PlayerController Instance { get => instance; }

    [SerializeField] protected CharacterController _characterController;
    
    [SerializeField] protected GameObject _cameraContainer;
    
    [SerializeField] protected float _speed = 6.0F;
    [SerializeField] protected float _jumpPower = 8.0F;
    [SerializeField] protected float _gravity = 20.0F;

    [SerializeField] protected bool _isDead = false;
    [SerializeField] protected bool _isOnPlatform = false;

    [SerializeField] protected Transform _respawnTransform;

    [SerializeField] protected Vector3 _moveDirection = Vector3.zero;


    /**************************************************/

    private void Awake()
    {
        PlayerController.instance = this;
    }
    private void Start()
    {
        RePosition();
    }

    void Update()
    {
        this.Movement();
    }
     void FixedUpdate()
    {
        this.Respawn();
        
    }

    public virtual void Movement()
    {
        if (this._characterController.isGrounded)
        {
            this._moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            this._moveDirection = this._cameraContainer.transform.TransformDirection(this._moveDirection);
            this._moveDirection *= this._speed;
            if (Input.GetButton("Jump"))
            {
                this._moveDirection.y = this._jumpPower;
            }
        }

        this._moveDirection.y -= this._gravity * Time.deltaTime;
        this._characterController.Move(this._moveDirection * Time.deltaTime);

        Ray ray = new Ray(this.transform.position, Vector3.down * 100);
        Debug.DrawRay(ray.origin, ray.direction);
    }

    public virtual void RePosition()
    {
        Ray ray = new Ray(this.transform.position, Vector3.down);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            GameObject platform = hit.collider.gameObject;
            Vector3 colliderPos = ((BoxCollider)(hit.collider)).center;
            Vector3 playerPos = platform.transform.InverseTransformPoint(this.transform.position);
            Vector3 newPos = new Vector3(playerPos.x - colliderPos.x, playerPos.y, playerPos.z - colliderPos.z);
            newPos = platform.transform.TransformPoint(newPos);

            this.transform.position = newPos;
        }
    }

    public virtual void Respawn()
    {
        if(!this._isDead) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this._isDead = false;
    }

    public virtual void Enable()
    {
        this.enabled = true;
        this._characterController.enabled = true;
    }

    public virtual void Disable()
    {
        this.enabled = false;
        this._characterController.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Plane")
        {
            Debug.Log("Touch Plane!");
            this._isDead = true;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Platform")
        {
            this._isOnPlatform = true;
            foreach(Platform p in PlatformManager.Instance.GetPlatforms())
            {
                if(hit.gameObject == p.gameObject)
                {
                    p.TurnOnEmission();
                }
            }
        }
    }   


}