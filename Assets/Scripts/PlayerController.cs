using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField]private float speed = 10.0f;
    private float xInput; // left and right input
    private float zInput; // up and down input
    public Rigidbody playerRb;
    private float jumpForce= 400; //this is the initial jump force wich needs to be high to get you off the ground
    private int jumpCount = 1; //when initially jumping your raycast keeps you at 1 so a double jump is 1 and a triple jump would be 2 so on and so forth
    public float hoverForce = 2; //strenght of force keeping you in place
    public GameObject weaponHitBox;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");
        if (xInput < 0)
        {
            transform.rotation =  Quaternion.Euler(0,90,0);
            transform.Translate(Vector3.right * Time.deltaTime * speed * xInput, Space.World);
        }
        if (xInput > 0)
        {
            transform.rotation =  Quaternion.Euler(0,-90,0);
            transform.Translate(Vector3.right * Time.deltaTime * speed * xInput, Space.World);
        }
        if (zInput < 0)
        {
            transform.rotation =  Quaternion.Euler(0,0,0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * zInput, Space.World);
        }
        if (zInput > 0)
        {
            transform.rotation =  Quaternion.Euler(0,180,0);
            transform.Translate(Vector3.forward * Time.deltaTime * speed * zInput, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed =15;
        }
        else
        {
            speed = 10;
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            Vector3 playerPosition = transform.position;
            Vector3 weaponOffset = transform.forward * -1f + transform.right * 0f + transform.up * 0f;
            Instantiate(weaponHitBox, playerPosition + weaponOffset, transform.rotation);
        }

        Ray ray = new Ray(transform.position, transform.up * -1.2f);
        Debug.DrawRay(transform.position, transform.up * -1.2f,Color.red, 0f);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1.2f))
        {
            playerRb.AddForce(transform.up * hoverForce * -5f, ForceMode.Force);
            //Debug.Log("the ray is colliding with" + hitInfo.collider.gameObject.name);
            jumpCount = 1;
  
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Force );
            jumpForce = 200;
            jumpCount--;
            StartCoroutine(DoubleJump());
        }

    }

    IEnumerator DoubleJump()
    {
        yield return new WaitForSeconds(1);
        jumpForce = 400;
    }
}
