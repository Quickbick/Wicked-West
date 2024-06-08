using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class hidingoverlay : MonoBehaviour
{
    public GameObject overlay;

    // Start is called before the first frame update
    void Start()
    {
        overlay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isHiding)
            overlay.SetActive(true);
        else
            overlay.SetActive(false);

    }
}
