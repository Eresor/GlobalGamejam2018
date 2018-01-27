using UnityEngine;

public class ProgressBarObsolete : MonoBehaviour
{
    private float CurrentHealth = 0;

    private float LosePerSecond = 1;

    private float barWidth;

    private bool makeProgress = true;


    // Use this for initialization
    void Start()
    {
        this.LosePerSecond = 0;
        this.barWidth = this.GetComponent<RectTransform>().sizeDelta.x;
    }



    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        if (!makeProgress)
        {
            return;
        }


        if (timer >= 1)
        {
            this.AddHealth(this.LosePerSecond);
            timer = 0;
        }

        this.AdjustBar();

        timer += Time.deltaTime;

        if (CurrentHealth < 0)
        {
            this.AdjustBar(true);
        }

    }

    public void SubtrackHealth(float value)
    {
        this.CurrentHealth -= value;
    }

    public void AddHealth(float value)
    {
        this.CurrentHealth += value;
    }

    private void AdjustBar(bool zero = false)
    {
        var offsetX = (100 - CurrentHealth) * this.barWidth / 100;
        var currentPosition = this.GetComponent<RectTransform>().localPosition;

        this.GetComponent<RectTransform>().localPosition = zero ? new Vector3(-offsetX, 0, 0)
            : Vector3.Lerp(currentPosition, new Vector3(-offsetX, 0, 0), 1 * Time.deltaTime);
    }
}
