using UnityEngine;
using System.Collections;

public class ZoomOnIsland : MonoBehaviour
{
    public AnimationClip[] zoomAnimations;
    public AnimationClip[] dezoomAnimations;
    public new Animation animation;
    public bool isZoomedOnIsland1 = false;
    public bool isZoomedOnIsland2 = false;
    public bool isZoomedOnIsland3 = false;
    public GameObject planete1;
    public GameObject planete2;
    public GameObject planete3;
    public GameObject titre;

    void Start()
    {
        // Désactiver le bouton au démarrage
        planete1.SetActive(false);
        planete2.SetActive(false);
        planete3.SetActive(false);
    }

    void Update()
    {
        // Détecte les clics de souris
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast pour détecter les objets cliqués
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Vérifie si l'objet cliqué a le tag "Island1"
                if (hit.collider.CompareTag("island1"))
                {
                    if (isZoomedOnIsland1 == false)
                    {
                        // Lance l'animation de zoom de la première île
                        titre.SetActive(false);
                        PlayZoomAnimation(0);
                        isZoomedOnIsland1 = true;
                        planete1.SetActive(true);
                    }
                    else
                    {
                        // Lance l'animation de zoom de la première île
                        planete1.SetActive(false);
                        PlayDezoomAnimation(0);
                        isZoomedOnIsland1 = false;
                        titre.SetActive(true);
                    }
                }
                else if (hit.collider.CompareTag("island2"))
                {
                    if(isZoomedOnIsland2 == false)
                    {
                        // Lance l'animation de zoom de la deuxième île
                        titre.SetActive(false);
                        PlayZoomAnimation(1);
                        isZoomedOnIsland2 = true;
                        planete2.SetActive(true);
                    }
                    else
                    {
                        // Lance l'animation de zoom de la deuxième île
                        planete2.SetActive(false);
                        PlayDezoomAnimation(1);
                        isZoomedOnIsland2 = false;
                        titre.SetActive(true);
                    }

                }
                else if (hit.collider.CompareTag("island3"))
                {
                    if(isZoomedOnIsland3 == false)
                    {
                        // Lance l'animation de zoom de la troisième île
                        titre.SetActive(false);
                        PlayZoomAnimation(2);
                        isZoomedOnIsland3 = true;
                        planete3.SetActive(true);
                    }
                    else
                    {
                        // Lance l'animation de zoom de la troisième île
                        planete3.SetActive(false);
                        PlayDezoomAnimation(2);
                        isZoomedOnIsland3 = false;
                        titre.SetActive(true);
                    }
                }
            }
        }
    }

    void PlayZoomAnimation(int index)
    {
        // Vérifie si l'index est valide et s'il existe une animation à cet index
        if (index >= 0 && index < zoomAnimations.Length && zoomAnimations[index] != null)
        {
            // Récupère le composant d'animation de l'objet actuellement cliqué

            // Si le composant d'animation n'est pas attaché à cet objet, on l'ajoute
            //if (animation == null)
            // animation = gameObject.AddComponent<Animation>();

            // Lance l'animation clip correspondant à l'index
            animation.AddClip(zoomAnimations[index], zoomAnimations[index].name);
            animation.clip = zoomAnimations[index];
            animation.Play();
        }
        else
        {
            Debug.LogWarning("Aucune animation n'est disponible pour l'index spécifié.");
        }
    }

    void PlayDezoomAnimation(int index)
    {
        // Vérifie si l'index est valide et s'il existe une animation à cet index
        if (index >= 0 && index < dezoomAnimations.Length && dezoomAnimations[index] != null)
        {
            // Récupère le composant d'animation de l'objet actuellement cliqué

            // Si le composant d'animation n'est pas attaché à cet objet, on l'ajoute
            //if (animation == null)
            // animation = gameObject.AddComponent<Animation>();

            // Lance l'animation clip correspondant à l'index
            animation.AddClip(dezoomAnimations[index], dezoomAnimations[index].name);
            animation.clip = dezoomAnimations[index];
            animation.Play();
        }
        else
        {
            Debug.LogWarning("Aucune animation n'est disponible pour l'index spécifié.");
        }
    }


}
