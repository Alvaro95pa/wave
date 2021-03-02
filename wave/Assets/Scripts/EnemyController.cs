using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Components
    private Rigidbody rb;
    private Transform player;
    private PlayerController playerCtrl;

    // Health vars
    [SerializeField]
    private int maxImpacts = 5;
    private int currentImpacts = 0;

    // Movement vars
    [SerializeField]
    private float movementSpeed = 0.0f;
    [SerializeField]
    private float minDistance = 0.0f;

    // Damage vars
    [SerializeField]
    private int damagePower = 1;
    [SerializeField]
    private float attackRatio = 0.5f;
    private float nextAttack = 0.5f;
    private float attackTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerCtrl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        chase();
    }

    private void OnTriggerExit(Collider other)
    {
        currentImpacts++;
        if (other.gameObject.CompareTag("Projectile") && currentImpacts >= maxImpacts)
        {
            Destroy(this.gameObject);
        }
    }

    private void chase()
    {
        transform.LookAt(player);
        if (Vector3.Distance(transform.position, player.position) >= minDistance)
        {
            rb.MovePosition(rb.position + (transform.forward * movementSpeed * Time.deltaTime));
        }
        else
        {
            attack();
        }
    }

    private void attack()
    {
        attackTime += Time.deltaTime;

        if (attackTime > nextAttack)
        {
            nextAttack = attackTime + attackRatio;
            playerCtrl.setHealth(playerCtrl.getHealth() - damagePower);

            nextAttack -= attackTime;
            attackTime = 0.0f;
        }
    }
}
