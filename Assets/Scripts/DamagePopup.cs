using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 1f;
    }

    public static DamagePopup Create(Canvas parentCanvas, int damageAmount)
    {

        RectTransform rt = (RectTransform)parentCanvas.transform;
        float xSpawn = Random.Range(0, rt.rect.width * rt.localScale.x) + parentCanvas.transform.position.x - rt.rect.width*rt.localScale.x;

        Transform damagePopupTransform = Instantiate(GameAssets.instance.pfDamagePopup, new Vector3(xSpawn, parentCanvas.transform.position.y, parentCanvas.transform.position.z), Quaternion.identity);
        damagePopupTransform.SetParent(parentCanvas.transform);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    private void Update()
    {
        float moveYSpeed = 3f;

        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearSpeed = 6f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
