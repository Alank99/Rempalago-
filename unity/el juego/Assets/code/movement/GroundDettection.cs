using UnityEngine;

public class GroundDettection : MonoBehaviour
{
    public playerController daddy;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("ground")){
            daddy.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("ground")){
            daddy.grounded = false;
        }
    }
}
