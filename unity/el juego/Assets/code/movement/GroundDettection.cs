using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundDettection : MonoBehaviour
{
    public playerController daddy;

    public List<Collider2D> groundContacts;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("ground")){
            groundContacts.Add(other);
            if (groundContacts.Count() == 1)
                daddy.TouchGrass();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("ground")){
            groundContacts.Remove(other);
            if (!groundContacts.Any())
                daddy.StopTouchGrass();
        }
    }
}
