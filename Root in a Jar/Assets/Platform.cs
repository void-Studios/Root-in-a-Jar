using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private bool isVisible = true;
    private bool isDestroyed;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Collider2D[] overlappingColliders = Physics2D.OverlapCircleAll(transform.position, 0.01f);
        foreach (Collider2D collider in overlappingColliders)
        {
            if (collider.CompareTag("Platform"))
            {
                if (Vector3.Distance(gameObject.transform.position, collider.transform.position)>=0.1f)
                {
                    Destroy(gameObject);
                    isDestroyed = true;
                    break;
                }
                
            }
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnBecameInvisible()
    {
        if (!isDestroyed)
        {
            isVisible = false;

            if (!isVisible)
            {
                StartCoroutine(DestroyAfterDelay());
            }
        }
        
    }

    private void OnBecameVisible()
    {
        isVisible = true;
        StopAllCoroutines();
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (!isVisible)
        {
            Destroy(gameObject);
        }
    }
}
