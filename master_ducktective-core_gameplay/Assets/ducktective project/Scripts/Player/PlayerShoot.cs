using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Projectile;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            GameObject ShootyMcShot = Instantiate(Projectile, transform.position, transform.rotation);
            ShootyMcShot.transform.position = transform.position;
        } 
    }
}
