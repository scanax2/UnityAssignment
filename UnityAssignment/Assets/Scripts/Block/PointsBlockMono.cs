using System;
using System.Collections;
using UnityEngine;

public class PointsBlockMono : MonoBehaviour
{
    public Action<int> OnGroundCollision;

    [SerializeField] private ParticleSystem particles;
    [SerializeField] private int points;


    public int Points { get => points; }
    public bool IsDead { get; set; }


    public IEnumerator Die()
    {
        gameObject.SetActive(false);

        if (particles != null)
        {
            particles.transform.parent = null;
            particles.Play();
            particles.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(5f);
            particles.transform.parent = gameObject.transform;
        }
        else
        {
            yield return null;
        }
    }

    public void OnCollision()
    {
        if (IsDead || !gameObject.activeSelf)
        {
            return;
        }
        IsDead = true;

        OnGroundCollision?.Invoke(points);
        StartCoroutine(Die());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            OnCollision();
        }
    }
}
