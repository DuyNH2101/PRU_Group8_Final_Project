using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Renderer meshRenderer;
    public float speed = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
