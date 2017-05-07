using UnityEngine;
using System.Collections;

public class MenuSitioX : MonoBehaviour {
	public GUISkin botonGaleria,botonRA,botonMapas;
	public Texture2D imagen;
	public Texture2D cabecera;
	public int idiomaElegido;
	public static int sitioElegido;
	public static string descSitio;
	public static string tituloSitio;
	public string[] sitio;

	void Start () {
		idiomaElegido = Inicio1.idiomaElegido;
		sitioElegido = ListadoSitios.sitioElegido;
		BaseDatos db = new BaseDatos();
		db.AbrirBD("turismo.db");
		sitio = db.obtieneDatosSitio(sitioElegido);
		db.CerrarBD();
		tituloSitio = sitio [1];
		descSitio = sitio[7+idiomaElegido];
		Debug.Log (descSitio);
	}
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("ListadoSitios");
	}
	void OnGUI(){
		if (botonGaleria)
			GUI.skin = botonGaleria;
		//variables para la interfaz
		float altoPantalla = Screen.height;
		float anchoPantalla = Screen.width;
		int tamanioLetra = Screen.width/23;
		//Botones
		float inicioBotones = altoPantalla / 2;
		float diferenciaAlto = altoPantalla / 12;
		float tamanioBoton = altoPantalla/5;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, diferenciaAlto), cabecera, ScaleMode.StretchToFill);
		GUI.skin.label.fontSize = Screen.width/12;
		GUI.Label (new Rect (anchoPantalla/2-Screen.width/2,0,Screen.width, Screen.height/2-diferenciaAlto),tituloSitio);
		GUI.DrawTexture (new Rect (0, diferenciaAlto, Screen.width, Screen.height), imagen,ScaleMode.StretchToFill);

		if (GUI.Button (new Rect (anchoPantalla/2-tamanioBoton/2, (altoPantalla+diferenciaAlto)/7,tamanioBoton, tamanioBoton) ,"")) {
			//Ir a galeria
			Application.LoadLevel ("GaleriaSitioX");
		}
		GUI.skin = botonRA;
		if (GUI.Button (new Rect (anchoPantalla/2-tamanioBoton/2, (altoPantalla+diferenciaAlto)/2-tamanioBoton/2, tamanioBoton, tamanioBoton),"")) {
			//Ir a RA
			Application.LoadLevel (sitio[2]);
		}
		GUI.skin = botonMapas;
		if (GUI.Button (new Rect (anchoPantalla/2-tamanioBoton/2, (altoPantalla+diferenciaAlto)/1.48f, tamanioBoton, tamanioBoton),"")) {
			//Ir a Google maps
			Application.OpenURL (sitio[4]);
		}
	}
}
