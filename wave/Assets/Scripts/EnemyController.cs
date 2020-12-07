using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int maxImpacts;
    private int currentImpacts = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        currentImpacts++;
        if (other.gameObject.CompareTag("Projectile") && currentImpacts >= maxImpacts)
        {
            Destroy(this.gameObject);
        }
    }
}
