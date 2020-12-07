using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MAX_AMMO = 50;
    
    // Components
    private Rigidbody rb;
    private Collider rbCollider;
    [SerializeField]
    private Transform cam;

    // Movement and rotation vars
    [SerializeField]
    private float speed;
    private Vector3 movement;
    private Vector3 direction;

    // Jump vars
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private LayerMask layerMask;

    // Combat vars
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private float fireRatio = 0.5f;
    private float nextFire = 0.5f;
    private float myTime = 0.0f;
    [SerializeField]
    private int ammo;
    private bool isReloading = false;
    [SerializeField]
    private float reloadVelocity;
    private float nextAmmo;
    private float reloadTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rbCollider = GetComponent<BoxCollider>();

        // Initialize reload vars
        nextAmmo = reloadVelocity;
    }

    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")).normalized;

        direction = Quaternion.Euler(0.0f, cam.eulerAngles.y, 0.0f) * movement;

        // Shooting
        shoot();

        // Reloading
        reload();
    }

    void FixedUpdate()
    {
        playerMovement();

        isGrounded();

        playerJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo") && !isReloading)
        {
            isReloading = true;
        }
    }

    private void playerMovement()
    {
        rb.MoveRotation(Quaternion.Euler(0.0f, cam.eulerAngles.y, 0.0f));
        if (movement.magnitude >= 0.01f && !Input.GetButton("Sprint"))
        {
            rb.MovePosition(rb.position + (direction.normalized * speed * Time.deltaTime));
        }
        else if (movement.magnitude >= 0.01f && Input.GetButton("Sprint"))
        {
            rb.MovePosition(rb.position + (direction.normalized * (2.0f * speed) * Time.deltaTime));
        }
    }

    private void playerJump()
    {
        if (Input.GetButton("Jump") && isGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(rb.position, rbCollider.bounds.extents.y / 2.0f, Vector3.down, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            return true;
        }
        return false;
    }

    private void shoot()
    {
        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire && ammo > 0 && !isReloading)
        {
            nextFire = myTime + fireRatio;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            nextFire -= myTime;
            myTime = 0.0f;
            ammo -= 1;
        }
    }

    private void reload()
    {
        reloadTime += Time.deltaTime;

        if(isReloading && ammo < MAX_AMMO && reloadTime > nextAmmo)
        {
            nextAmmo = reloadTime + reloadVelocity;
            ammo += 1;

            nextAmmo -= reloadTime;
            reloadTime = 0.0f;
        }
        else if(ammo == MAX_AMMO)
        {
            isReloading = false;
        }
    }
}
