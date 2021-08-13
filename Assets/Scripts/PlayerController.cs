using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    [SerializeField] private int mLife;
    private Animator _mAnimator;
    private Rigidbody _mRigidbody;
    private Vector3 _mDirection;
    public LayerMask FloorMask;
    public GameController GameController;
    public InterfaceController InterfaceController;
    [SerializeField] private AudioClip damageSound;

    public int MLife
    {
        get { return mLife; }
        set { mLife = value; }
    }

    private void Start()
    {
        _mAnimator = GetComponent<Animator>();
        _mRigidbody = GetComponent<Rigidbody>();
        GameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        InterfaceController = GameObject.FindWithTag("Canvas").GetComponent<InterfaceController>();
    }

    private void Update()
    {
        var axisX = Input.GetAxis("Horizontal");
        var axisZ = Input.GetAxis("Vertical");

        _mDirection = new Vector3(axisX, 0, axisZ);
        
        _mAnimator.SetBool("isMove", _mDirection != Vector3.zero);
    }

    private void FixedUpdate()
    {
        _mRigidbody.MovePosition
        (_mRigidbody.position +
         (_mDirection * (Time.deltaTime * mSpeed)));

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit impact;

        if (!Physics.Raycast(raio, out impact, 50, FloorMask)) return;
        Vector3 playerAimPosition = impact.point - transform.position;

        playerAimPosition.y = transform.position.y;

        Quaternion newRotation = Quaternion.LookRotation(playerAimPosition);

        _mRigidbody.MoveRotation(newRotation);
    }

    public void TakeDamage(int damageEnemy)
    {
        mLife -= damageEnemy;
        InterfaceController.UpdateSliderLifePlayer();
        AudioController.Instance.PlayOneShot(damageSound);
        
        if (mLife > 0) return;
        Time.timeScale = 0;
        GameController.SetPanelGameOver(true);
    }
    
    
}