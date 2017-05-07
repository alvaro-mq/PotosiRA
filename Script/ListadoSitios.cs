using UnityEngine;
using System.Collections;

public class ListadoSitios : MonoBehaviour {
	public GUISkin Interfaz;
	public GUISkin letraCabecera;
	public Texture2D imagen;
	public Texture2D cabecera;
	public static int sitioElegido;
	public string[] sitios;
	// Use this for initialization
	void Start () {
		BaseDatos db = new BaseDatos();
		db.AbrirBD("turismo.db");
		sitios = db.listaSitios();
		db.CerrarBD();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("inicio");
	}
	void OnGUI(){

			GUI.skin = Interfaz;
		//variables para la interfaz
		float altoPantalla = Screen.height;
		float anchoPantalla = Screen.width;
		int tamanioLetra = Screen.width/23;
		//Botones
		float inicioBotones = altoPantalla / 2;
		float diferenciaAlto = altoPantalla / 12;

		GUI.DrawTexture (new Rect (0, 0, Screen.width, diferenciaAlto), cabecera, ScaleMode.StretchToFill);

		GUI.DrawTexture (new Rect (0, diferenciaAlto, Screen.width, Screen.height/2-diferenciaAlto), imagen,ScaleMode.StretchToFill);

		Interfaz.button.fontSize = tamanioLetra;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla, diferenciaAlto), sitios[0])) {
			sitioElegido = 1;
			Application.LoadLevel ("MenuSitioX");
		}
		inicioBotones += diferenciaAlto;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[1])) {
			sitioElegido = 2;
			Application.LoadLevel ("MenuSitioX");
		}
		if (GUI.Button (new Rect (anchoPantalla/2, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[2])) {
			sitioElegido = 3;
			Application.LoadLevel ("MenuSitioX");
		}
		inicioBotones += diferenciaAlto;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla/2, diferenciaAlto),sitios[3])) {
			sitioElegido = 4;
			Application.LoadLevel ("MenuSitioX");
		}
		if (GUI.Button (new Rect (anchoPantalla/2, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[4])) {
			sitioElegido = 5;
			Application.LoadLevel ("MenuSitioX");
		}
		inicioBotones += diferenciaAlto;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla/2, diferenciaAlto),sitios[5])) {
			sitioElegido = 6;
			Application.LoadLevel ("MenuSitioX");
		}
		if (GUI.Button (new Rect (anchoPantalla/2, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[6])) {
			sitioElegido = 7;
			Application.LoadLevel ("MenuSitioX");
		}
		inicioBotones += diferenciaAlto;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[7])) {
			sitioElegido = 8;
			Application.LoadLevel ("MenuSitioX");
		}
		if (GUI.Button (new Rect (anchoPantalla/2, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[8])) {
			sitioElegido = 9;
			Application.LoadLevel ("MenuSitioX");
		}
		inicioBotones += diferenciaAlto;
		if (GUI.Button (new Rect (0, inicioBotones, anchoPantalla/2, diferenciaAlto), sitios[9])) {
			sitioElegido = 10;
			Application.LoadLevel ("MenuSitioX");
		}
		if (GUI.Button (new Rect (anchoPantalla/2, inicioBotones, anchoPantalla/2, diferenciaAlto),sitios[10])) {
			sitioElegido = 11;
			Application.LoadLevel ("MenuSitioX");
		}
		GUI.skin = letraCabecera;
		GUI.skin.label.fontSize = Screen.width / 10;
		GUI.Label (new Rect (anchoPantalla/2-Screen.width/2,0,Screen.width, Screen.height/2-diferenciaAlto), "Sitios");
	}
}
