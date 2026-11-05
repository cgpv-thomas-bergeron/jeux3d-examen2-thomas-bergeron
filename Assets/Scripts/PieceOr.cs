using UnityEngine;

public class PieceOr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boule boule))
        {
            boule.ajouterPiece();
            Destroy(gameObject);
        }
    }
}
