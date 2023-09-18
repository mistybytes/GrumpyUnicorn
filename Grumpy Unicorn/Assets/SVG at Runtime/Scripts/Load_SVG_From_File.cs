using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using UnityEngine.UI;

public class Load_SVG_From_File : MonoBehaviour
{
	[Tooltip("Name of the SVG file with its extension (example bee.svg)")]
	public string SVG_File_Name;

	[Tooltip("Path of the folder containing the SVG file (example C:/Users/YourUserName/AppData/LocalLow/CompanyName/ProductName)")]
	public string SVG_Path;

	[Tooltip("Full path to the SVG file")]
	private string SVG_Full_Path;

	void Awake()
	{
		//If the path to the folder containing the SVG file is empty
		if(SVG_Path.Trim().Length == 0)
		{
			//We fill it
			SVG_Path = Application.persistentDataPath;
		}

		//If the SVG file name is empty
		if(SVG_File_Name.Trim().Length == 0)
		{
			//We fill it
			SVG_File_Name = "bee.svg";
		}

		//We fill the full path to the SVG file
		SVG_Full_Path = SVG_Path + "/" + SVG_File_Name;
	}

	void Start()
	{
		//If the SVG file exists
		if(File.Exists(SVG_Full_Path))
		{
			//We read it
			StreamReader SVG_File_Reader;
			SVG_File_Reader = File.OpenText(SVG_Full_Path);
			string SVG_File_Content = SVG_File_Reader.ReadToEnd();
			SVG_File_Reader.Close();
			//We assign the readed sprite to the Sprite Renderer
			GetComponent<SpriteRenderer>().sprite = Load_SVG_as_Sprite(SVG_File_Content);
		}
		else //The SVG file doesn't exists
		{
			Debug.Log("<color=orange>" + "Missing SVG file at " + Application.persistentDataPath + "/" + SVG_File_Name + "</color>\nTo test it, please put bee.svg file into " + Application.persistentDataPath);
		}
	}

	Sprite Load_SVG_as_Sprite(string SVG_File_Content)
	{
		//If the SVG file is NOT empty
		if(SVG_File_Content.Trim().Length > 0)
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
			var SceneInfo = SVGParser.ImportSVG(new StringReader(SVG_File_Content));
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
}