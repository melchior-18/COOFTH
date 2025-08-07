using UnityEngine;
using UnityEngine.UI;
public class barre_vie : MonoBehaviour
{
    [SerializeField] private Image barreDeVieImage;
    [SerializeField] private float vieMax = 100f;
    private float vieActuelle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vieActuelle = vieMax;
        MettreAJourBarre();
        
    }

    public void PrendreDegats(float degats)
    {
        vieActuelle = Mathf.Clamp(vieActuelle - degats, 0, vieMax);
        MettreAJourBarre();
    }

    private void MettreAJourBarre()
        {
            barreDeVieImage.fillAmount = vieActuelle / vieMax;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
