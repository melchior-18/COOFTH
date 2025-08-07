using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class barre_vie : MonoBehaviour
{
    [SerializeField] private Image barreDeVieImage;
    [SerializeField] private float vieMax = 100f;
    private float vieActuelle;

    public bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vieActuelle = vieMax;
        MettreAJourBarre();
        
    }

    public void PrendreDegats(float degats)
    {
        if (isDead != true)
        {
            vieActuelle = Mathf.Clamp(vieActuelle - degats, 0, vieMax);
            MettreAJourBarre();

            if (vieActuelle <= 0)
            {
                isDead = true;
                SceneManager.LoadScene("Game_over");
            }
                
        }
    }

    private void MettreAJourBarre()
        {
            barreDeVieImage.fillAmount = vieActuelle / vieMax;
        }
}
