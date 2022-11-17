using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    Animation animation;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            animation = GetComponent<Animation>();
            animation.Play("Hit");
        }
    }
}
