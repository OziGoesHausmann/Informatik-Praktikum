using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slingshot : MonoBehaviour
{
    public Transform target;
    public float force;

    public Rigidbody2D bulletPrefab;

    public Sprite currentBullet;

    public float shootRate = 1f;
    float nextShot = 0f;

    private bool hasFired = false;

    public GameObject preserveResultScreen;
    public TMP_Text durabilityResult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turn();

        //if (Time.time >= nextShot)
        if (!hasFired)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
                //nextShot = Time.time + 1f / shootRate;
            }
        }
    }

    void Turn()
    {
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Fire()
    {
        hasFired = true;
        Rigidbody2D bulletInstance = Instantiate(bulletPrefab, target.position, transform.rotation);
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        bulletInstance.AddForce(dir.normalized * force);
        bulletInstance.GetComponent<SpriteRenderer>().sprite = currentBullet;

        StartCoroutine(CheckShooting());
    }

    IEnumerator CheckShooting()
    {
        yield return new WaitForSeconds(3f);

        StartCoroutine(PreserveResult(false));
    }

    public void Hit()
    {
        StopAllCoroutines();

        StartCoroutine(PreserveResult(true));
    }

    public IEnumerator PreserveResult(bool perfectTry)
    {
        if (perfectTry)
        {
            durabilityResult.text = "+3";
        }
        else
        {
            durabilityResult.text = "-1";
        }

        preserveResultScreen.SetActive(true);

        yield return new WaitForSeconds(3f);

        preserveResultScreen.SetActive(false);

        hasFired = false;
    }
}
