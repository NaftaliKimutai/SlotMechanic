using UnityEngine;
using UnityEditor;
public class TranslationHelper : Editor
{
	[MenuItem("Translation/_Assign")]
	static void Assign()
	{
		FindFirstObjectByType<LanguageMan>().AssignCodes();
	}
	[MenuItem("Translation/English")]
	static void SetEnglish()
	{
		if (!Application.isPlaying)
			return;
		if (FindFirstObjectByType<Extra_LanguageMan>())
		{
			FindFirstObjectByType<Extra_LanguageMan>().ActiveLanguage = Extra_TheLanguage.English;
			FindFirstObjectByType<Extra_LanguageMan>().RefreshAll();
		}
		FindFirstObjectByType<LanguageMan>().ActiveLanguage = Extra_TheLanguage.English;
		FindFirstObjectByType<LanguageMan>().RefreshAll();
	}
	[MenuItem("Translation/Chinese")]
	static void SetChinese()
	{
		if (!Application.isPlaying)
			return;
		if (FindFirstObjectByType<Extra_LanguageMan>())
		{
			FindFirstObjectByType<Extra_LanguageMan>().ActiveLanguage = Extra_TheLanguage.Chinese;
			FindFirstObjectByType<Extra_LanguageMan>().RefreshAll();
		}

		FindFirstObjectByType<LanguageMan>().ActiveLanguage = Extra_TheLanguage.Chinese;
		FindFirstObjectByType<LanguageMan>().RefreshAll();
	}
	[MenuItem("Translation/Japan")]
	static void SetJapan()
	{
		if (!Application.isPlaying)
			return;
		if (FindFirstObjectByType<Extra_LanguageMan>())
		{
			FindFirstObjectByType<Extra_LanguageMan>().ActiveLanguage = Extra_TheLanguage.Japan;
			FindFirstObjectByType<Extra_LanguageMan>().RefreshAll();
		}
		FindFirstObjectByType<LanguageMan>().ActiveLanguage = Extra_TheLanguage.Japan;
		FindFirstObjectByType<LanguageMan>().RefreshAll();
	}
	[MenuItem("Translation/Spanish")]
	static void SetSpanish()
	{
		if (!Application.isPlaying)
			return;
		if (FindFirstObjectByType<Extra_LanguageMan>())
		{
			FindFirstObjectByType<Extra_LanguageMan>().ActiveLanguage = Extra_TheLanguage.Spanish;
			FindFirstObjectByType<Extra_LanguageMan>().RefreshAll();
		}
		FindFirstObjectByType<LanguageMan>().ActiveLanguage = Extra_TheLanguage.Spanish;
		FindFirstObjectByType<LanguageMan>().RefreshAll();
	}

}
