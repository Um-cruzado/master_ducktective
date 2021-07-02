using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Projectile;
    bool control = true;
    Animator Anima;
    public AudioClip[] audios;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire") && control == true)
        {
            control = false;
          //  GetComponent<AudioSource>().clip = audios[0];
            GameObject ShootyMcShot = Instantiate(Projectile, transform.position,Quaternion.identity);
            ShootyMcShot.transform.position = transform.position;
            ShootyMcShot.transform.rotation = transform.parent.rotation;//Quaternion.Euler(ShootyMcShot.transform.rotation.x, ShootyMcShot.transform.rotation.y, transform.rotation.y);
            StartCoroutine(TempoBala());
        }
    }
    IEnumerator TempoBala()
    {
        yield return new WaitForSeconds(2);
        control = true;
    }
}
