using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Load_SVG_From_Web : MonoBehaviour
{
	[Tooltip("Here you can put the URL to the SVG file you want to display")]
	public string SVG_URL = "https://raffin.dev/SVG_At_Runtime/bee.svg";

	IEnumerator Start()
	{
		//If the SVG URL contains a URL
		if(SVG_URL.Trim().Length > 0 && Check_StartingWord(SVG_URL.Trim(), "http"))
		{
			//We start downloading the SVG file
			using (UnityWebRequest www = UnityWebRequest.Get(SVG_URL))
			{
				//We wait for the download to finish
				yield return www.SendWebRequest();

				//If there IS an error
				if(www.error != null || www.isNetworkError)
				{
					Debug.Log("<color=orange>" + "Error while downloading the SVG file: " + "</color>" + www.error);
				}
				else //SVG Download OK
				{
					//We convert and assign the downloaded SVG content to the Sprite Renderer
					GetComponent<SpriteRenderer>().sprite = Load_SVG_as_Sprite(www.downloadHandler.text);
				}
			}
		}
		else //The SVG variable is empty or isn't HTTP
		{
			Debug.Log("<color=orange>" + "The SVG_URL variable is empty OR isn't HTTP" + "</color>");
		}
	}

	Sprite Load_SVG_as_Sprite(string SVG_URL)
	{
		//If the SVG file is NOT empty
		if(SVG_URL.Trim().Length > 0)
		{
			//We define the tessellation options
			var TessOptions = new VectorUtils.TessellationOptions()
			{
				StepDistance = 100.0f,
				MaxCordDeviation = 0.5f,
				MaxTanAngleDeviation = 0.1f,
				SamplingStepSize = 0.01f
			};
			//We import the vectorial data
			var SceneInfo = SVGParser.ImportSVG(new StringReader(SVG_URL));
			//We tesselate the geometry
			var TessGeo = VectorUtils.TessellateScene(SceneInfo.Scene, TessOptions);
			//We create the final Unity sprite
			var SVG_Sprite = VectorUtils.BuildSprite(TessGeo, 10.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true);
			return SVG_Sprite;
		}
		else //The SVG file is empty
		{
			Debug.Log("<color=orange>Load_SVG_as_Sprite(null): The SVG file is empty</color>");
			return null;
		}
	}

	public bool Check_StartingWord(string Content, string Word_To_Find)
	{
		//If the content is at least as long as the word to find
		if(Content.Length >= Word_To_Find.Length)
		{
			//If the word to find is at the beggining of the content
			if(Content.Substring(0, Word_To_Find.Length) == Word_To_Find)
			{
				return true; //== Word found at the beggining of the content
			}
			else
			{
				return false; //The word hasn't been found
			}
		}
		else
		{
			return false; //The word hasn't been found
		}
	}
}