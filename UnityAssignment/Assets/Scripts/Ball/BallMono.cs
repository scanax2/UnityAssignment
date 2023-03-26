using System;
using System.Collections;
using UnityEngine;

public class BallMono : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;


    public Action OnGroundCollision;
    public bool IsFirstTouch { get; set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            OnGroundCollision?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsFirstTouch)
        {
            return;
        }

        var block = collision.transform.GetComponent<PointsBlockMono>();

        if (block)
        {
            IsFirstTouch = false;
            block.OnCollision();
            rigidbody.velocity = rigidbody.velocity / 3f;
            StartCoroutine(Die());
        }
    }
}
