using UnityEngine;
using System.Collections;
using System.IO;

public class Inicio1 : MonoBehaviour {
	public GUISkin SkinIrDetalles;
	public GUISkin SkinIrGps;
	public GUISkin SkinCabecera;
	//public GUISkin BotonesIdiomas;

	public Texture2D imagen;
	public Texture2D cabecera;
	public Texture2D botonCompartir;
	public Texture2D botonIdiomas;
	public Texture2D botonEspaniol;
	public Texture2D botonIngles;
	
	private string imageName = "imagenPublicitariaApp";
	bool expandeMenu = false;
	// 0 para español y 1 para ingles
	public static int idiomaElegido;
	void Start () {
		idiomaElegido = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.Quit();
	}
	void OnGUI(){
				if (SkinIrDetalles)
						GUI.skin = SkinCabecera;
				//variables para la interfaz

				float anchoPantalla = Screen.width;
				int tamanioLetra = Screen.width / 23;
				float diferenciaAlto = Screen.height / 12.0f;
				float altoPantalla = Screen.height - diferenciaAlto;
				//Botones
				float tamanioBoton = altoPantalla / 3.5f;
				float inicioBotonesX = anchoPantalla / 2 - tamanioBoton / 2;
				float inicioBotonesY = diferenciaAlto + altoPantalla / 7;
				//Cabecera
				GUI.DrawTexture (new Rect (0, 0, anchoPantalla, diferenciaAlto), cabecera, ScaleMode.StretchToFill);
				GUI.skin.label.fontSize = Screen.width / 12;
				GUI.Label (new Rect (anchoPantalla / 2 - Screen.width / 2, 0, Screen.width, Screen.height / 2 - diferenciaAlto), " Potosi Aumentado");
				//fondo
				GUI.DrawTexture (new Rect (0, diferenciaAlto, anchoPantalla, altoPantalla), imagen, ScaleMode.StretchToFill);
				float tamanioBotonCabecera = diferenciaAlto / 1.2f;
				//Boton Compartir
				if (GUI.Button (new Rect (Screen.width - diferenciaAlto, diferenciaAlto / 2 - (diferenciaAlto / 1.2f) / 2, tamanioBotonCabecera, tamanioBotonCabecera), botonCompartir)) {
						StartCoroutine (ShareScreenshot ());
				}
				if (GUI.Button (new Rect (diferenciaAlto/7f, diferenciaAlto / 2 - (diferenciaAlto / 1.2f) / 2 , tamanioBotonCabecera, tamanioBotonCabecera), botonIdiomas)) {
					expandeMenu = !expandeMenu;
				}		
				
				float altoMenu = Screen.height / 7;
				if(expandeMenu){
					GUI.skin.button.fontSize = Screen.width / 18;
					GUI.BeginGroup(new Rect(0,diferenciaAlto,Screen.width/3.5f,altoMenu));
					if(GUI.Button(new Rect(0,0,Screen.width/3.5f,altoMenu/2),botonEspaniol)){
						expandeMenu = false;
						idiomaElegido = 0;
					}
					if(GUI.Button(new Rect(0,altoMenu/2,Screen.width/3.5f,altoMenu/2),botonIngles)){
						expandeMenu = false;
						idiomaElegido = 1;
					}
					
					GUI.EndGroup();
				}		
				GUI.skin = SkinIrDetalles;

				if (GUI.Button (new Rect (inicioBotonesX, inicioBotonesY, tamanioBoton, tamanioBoton), "")) {
						Application.LoadLevel ("ListadoSitios");
				}
				SkinIrDetalles.label.fontSize = 45;

				GUI.skin = SkinIrGps;
				if (GUI.Button (new Rect (inicioBotonesX, inicioBotonesY + diferenciaAlto * 1.5f + tamanioBoton, tamanioBoton, tamanioBoton), "")) {
						Application.LoadLevel ("BarraCargaGps");
				}
				
				//toolbarInt = GUI.Toolbar (new Rect (tamanioBoton/4, Screen.height-tamanioBoton/4, tamanioBoton/2, tamanioBoton/4), toolbarInt, imgIdiomas);
				
	
	
	
	}

	
	public IEnumerator ShareScreenshot()
	{
		// esperar hasta la renderizacion de la textura
		yield return new WaitForEndOfFrame();
		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		// creacion de textura
		//Texture2D screenTexture = new Texture2D(Screen.width, Screen.height,TextureFormat.RGB24,true);
		
		// cargando la textura
		//screenTexture.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		//screenTexture.Apply();
		//imagenCompartir.ReadPixels(new Rect(0f, 0f, Screen.width, Screen.height),0,0);
		//imagenCompartir.Apply();

		//----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
		Texture2D screenTexture = new Texture2D(1080, 1080,TextureFormat.RGB24,true);
		screenTexture.Apply();
		
		byte[] dataToSave = Resources.Load<TextAsset>(imageName).bytes;
		//byte[] guardarDatos = imagenCompartir.EncodeToPNG();
		
		string destino = Path.Combine(Application.persistentDataPath,System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg");
		
		File.WriteAllBytes(destino, dataToSave);
		
		if(!Application.isEditor)
		{
			AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
			AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
			intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
			AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
			AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse","file://" + destino);
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
			intentObject.Call<AndroidJavaObject> ("setType", "text/plain");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TITLE"), "TITULO");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "Ya probaste la nueva aplicación de Realidad Aumentada de la ciudad de Potosí? Que esperas! Descargala ahora desde http://www.google.com");
			intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "Recomendacion");
			intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
			AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "ELIGE LA APLICACION CON LA CUAL COMPARTIR");
			currentActivity.Call("startActivity", jChooser);
		}
		guiTexture.enabled = true;
	}

}
