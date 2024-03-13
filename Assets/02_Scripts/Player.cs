using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rigid;
    private SpriteRenderer sprite;
    [SerializeField]
    float moveSpeed;

    public Animator animator;
    public Vector3 inputVec;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.z = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
    }
    private void FixedUpdate()
    {
        Vector3 nextVec = inputVec.normalized * moveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }
    private void LateUpdate()
    {
        if (inputVec.x > 0)
            sprite.flipX = true;
        else if (inputVec.x < 0)
            sprite.flipX = false;
    }
    private void Interact()
    {
        Debug.Log("Interact");
    }

}
