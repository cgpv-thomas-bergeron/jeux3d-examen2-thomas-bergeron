using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(DetecteurBoule))]
public class Surface : MonoBehaviour
{
    /// <summary>
    /// Référence au détecteur de boule attaché à cette surface.
    /// </summary>
    public DetecteurBoule Detecteur { get; private set; }

    [SerializeField, Tooltip("Positions de génération des objets sur la surface.")]
    private Vector3[] positionGeneration;

    private Animator animator;

    private void Awake()
    {
        Detecteur = GetComponent<DetecteurBoule>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GestionnairePause.Instance.pauseAnimation += MettreEnPause;
        if (ControleurJeu.Instance.pause)
        {
            animator.SetTrigger("Bouger");
        }
        else
        {
            animator.SetTrigger("Pause");
        }
    }

    private void OnDestroy()
    {
        GestionnairePause.Instance.pauseAnimation -= MettreEnPause;
    }

    private void Update()
    {

    }

    /// <summary>
    /// Ajoute un objet à la surface à une position aléatoire parmi les positions de génération définies.
    /// </summary>
    /// <param name="prefab">Le prefab de l'objet à ajouter à la surface.</param>
    public void AjouterObjet(GameObject prefab)
    {
        if (positionGeneration.Length == 0)
        {
            Debug.LogWarning("Aucune position de génération définie pour la surface.");
            return;
        }

        // Sélectionne une position aléatoire parmi les positions disponibles
        Vector3 positionChoisie = positionGeneration[Random.Range(0, positionGeneration.Length)];
        // Instancie l'prefab à la position choisie
        GameObject objetGenere = Instantiate(prefab, transform);
        objetGenere.transform.localPosition = positionChoisie; // Utilise localPosition pour positionner l'objet par rapport à la surface
    }

    private void OnDrawGizmos()
    {
        foreach(Vector3 position in positionGeneration)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position + position, 0.2f);
        }
    }

    private void MettreEnPause(float tempsPause)
    {
        Debug.Log("test");

        ControleurJeu.Instance.pause = false;
        animator.SetTrigger("Pause");

        StartCoroutine(TempsPause(tempsPause));
    }

    private IEnumerator TempsPause(float duree)
    {
        float tempsEcoule = 0;

        while (tempsEcoule < duree)
        {
            tempsEcoule += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        Debug.Log("test");
        ControleurJeu.Instance.pause = true;
        animator.SetTrigger("Bouger");
    }
}
