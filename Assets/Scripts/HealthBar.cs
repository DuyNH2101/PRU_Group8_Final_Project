using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform bar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSize(float size)
    {
        if(size < 0f) return;
        bar.localScale = new Vector2 (size, 1f);
    }
}
