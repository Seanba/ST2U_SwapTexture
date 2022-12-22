using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    public float m_SeasonLength = 5.0f;

    private bool m_UseSummerTexture;
    private float m_Timer;

    private void OnEnable()
    {
        m_UseSummerTexture = true;
        UpdateSeason();
    }

    private void OnDisable()
    {
        m_UseSummerTexture = true;
        UpdateSeason();
    }

    private void Update()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_SeasonLength)
        {
            m_UseSummerTexture = !m_UseSummerTexture;
            m_Timer = 0;
            UpdateSeason();
        }
    }

    private void UpdateSeason()
    {
        if (m_UseSummerTexture)
        {
            Shader.SetGlobalFloat("_UseSummerTexture", 1.0f);
        }
        else
        {
            Shader.SetGlobalFloat("_UseSummerTexture", 0.0f);
        }
    }
}
