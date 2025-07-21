using UnityEngine;

public class BackgroundScroll_X : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
     public Renderer meshRenderer;
    public float speed = 0.01f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2( speed * Time.deltaTime,0);
    }
}
