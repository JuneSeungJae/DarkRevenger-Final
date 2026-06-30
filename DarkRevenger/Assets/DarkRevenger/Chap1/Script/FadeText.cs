using UnityEngine;
using TMPro;
using System.Collections;

public class FadeText : MonoBehaviour
{
    public TMPro.TextMeshProUGUI textComponent; // TextMeshPro 컴포넌트를 에디터에서 할당

    public float fadeInDuration = 1f; // 페이드 인 지속 시간
    public float fadeOutDuration = 1f; // 페이드 아웃 지속 시간
    public float displayDuration = 2f; // 텍스트가 완전히 보이는 상태로 유지되는 시간

    void Start()
    {
        if (textComponent != null)
        {
            StartCoroutine(FadeTextInAndOut());
        }
        else
        {
            Debug.LogError("텍스트 컴포넌트가 할당되지 않았습니다.");
        }
    }

    IEnumerator FadeTextInAndOut()
    {
        yield return StartCoroutine(FadeInText());
        yield return new WaitForSeconds(displayDuration);
        yield return StartCoroutine(FadeOutText());
    }

    IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);

        while (elapsedTime < fadeInDuration)
        {
            textComponent.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.color = targetColor;
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color originalColor = textComponent.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (elapsedTime < fadeOutDuration)
        {
            textComponent.color = Color.Lerp(originalColor, targetColor, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textComponent.color = targetColor;
    }
}