using UnityEngine;
using System.Collections;

public class TouchIndicator : MonoBehaviour
{
    public float lifespan = .1f;
    public bool used { get; set; }

    void Start()
    {
        this.used = false;
    }

    // Update is called once per frame
    void Update()
    {
        lifespan -= Time.deltaTime;

        if (this.used || lifespan <= 0)
            Destroy(this.gameObject);
    }
}
