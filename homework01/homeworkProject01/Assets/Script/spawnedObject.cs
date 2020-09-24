using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedObject : MonoBehaviour
{
    int hittedTimes = 5;
    Vector3 originalScale;
    public ParticleSystem particleBurst;
    public ParticleSystem particleScore;
    float lastCollision;
    public float maxSpeed = 50;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        originalScale = transform.localScale;
        lastCollision = Time.time;
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hittedTimes <= 0)
        {
            var particleInstance = Instantiate(particleBurst, transform.position, Quaternion.Euler(0, 0, 0));
            particleInstance.Play();
            foreach (var sN in FindObjectsOfType<ShowNumber>()){
                if (sN.objects.Contains(gameObject))
                {
                    sN.objects.Remove(gameObject);
                }
            }
            Destroy(gameObject);
        }
        if (rigidbody.velocity.magnitude > maxSpeed) rigidbody.velocity = rigidbody.velocity / rigidbody.velocity.magnitude * maxSpeed;
        float scale = Mathf.Sqrt(rigidbody.velocity.magnitude);
        scale = Mathf.Clamp(scale - 4, 0, 3)/7;
        transform.localScale = originalScale*(1-scale);
        GetComponent<BoxCollider>().size = Vector3.one / (1 - scale);
    }
    private void OnCollisionEnter(Collision collision)
    {
        var obj = collision.gameObject;
        if (!obj.tag.Equals("PlayerPat")) return;
        if (Time.time - lastCollision < 0.1) return;
        lastCollision = Time.time;
        var scorer=FindObjectOfType<Score>();
        float force;
        if (obj.GetComponentInParent<WhichPlayer>().player == PlayerNO.PlayerA)
        {
            force = obj.GetComponentInParent<Rigidbody>().angularVelocity.magnitude;
            hittedTimes--;
            GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(hittedTimes / 15f, 1 - hittedTimes * 0.1f, 1);
            if (hittedTimes == 0) scorer.scoreA += 5;
            if ((int)Mathf.Sqrt(force) >= 2)
            {
                scorer.scoreA ++;
                //transform.localScale *= 1 / Mathf.Clamp(force / 2, 1, 10);
                var particleInstance = Instantiate(particleScore, transform.position, Quaternion.Euler(0, 0, 0));
                particleInstance.Play();
            }
        }
        else
        {
            force = obj.GetComponentInParent<Rigidbody>().angularVelocity.magnitude;
            hittedTimes--;
            if (hittedTimes == 0) scorer.scoreB += 5;
            GetComponent<MeshRenderer>().material.color = Color.HSVToRGB(hittedTimes / 15f, 1 - hittedTimes * 0.1f, 1);
            if ((int)Mathf.Sqrt(force) >= 2)
            {
                scorer.scoreB ++;
                //transform.localScale *= 1 / Mathf.Clamp(force / 2, 1, 10);
                var particleInstance = Instantiate(particleScore, transform.position, Quaternion.Euler(0, 0, 0));
                particleInstance.Play();
            }
        }
        //Debug.Log("scoreA: " + scorer.scoreA + "   scoreB:" + scorer.scoreB);

    }
}
