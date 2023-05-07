using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("attack", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("attack", false);
        }
    }
}
