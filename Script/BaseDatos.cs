using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Data;
using System.Text;
using Mono.Data.SqliteClient;
public class BaseDatos : MonoBehaviour {
	private string connection;
	private IDbConnection dbcon;
	private IDbCommand dbcmd;
	private IDataReader reader;
	private StringBuilder builder;
	// Use this for initialization
	void Start () {
		
	}
	public void AbrirBD(string p)
	{
		connection = "URI=file:" + Application.persistentDataPath + "/" + p;
		Debug.Log(connection);
		dbcon = new SqliteConnection(connection);
		dbcon.Open();
	}
	
	public void CerrarBD(){
		reader.Close(); 
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbcon.Close();
		dbcon = null;
	}
	
	public IDataReader BasicQuery(string query){ 
		dbcmd = dbcon.CreateCommand(); 
		dbcmd.CommandText = query; 
		reader = dbcmd.ExecuteReader(); 
		return reader; 
	}
	public bool CrearTabla(string name,string[] col, string[] colType){ // Create a table, name, column array, column type array
		string query;
		query  = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0];
		for(var i=1; i< col.Length; i++){
			query += ", " + col[i] + " " + colType[i];
		}
		query += ")";
		try{
			dbcmd = dbcon.CreateCommand(); 
			dbcmd.CommandText = query; 
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			Debug.Log(e);
			return false;
		}
		return true;
	}
	public int insertarEnTablaX(string nombreTabla, string[] columnas, string[] valores){ 
		string query;
		query = "INSERT INTO " + nombreTabla + "(" + columnas[0];
		for(int i=1; i< columnas.Length; i++){
			query += ", " + columnas[i];
		}
		query += ") VALUES (" + valores[0];
		for(int i=1; i< columnas.Length; i++){
			query += ", " + valores[i];
		}
		query += ")";
		Debug.Log(query);
		try
		{
			dbcmd = dbcon.CreateCommand();
			dbcmd.CommandText = query;
			reader = dbcmd.ExecuteReader();
		}
		catch(Exception e){
			
			Debug.Log(e);
			return 0;
		}
		return 1;
	}
	public string[] listaSitios(){
		string query = "SELECT nombre_sitio FROM sitio";
		string[] sitios = new string[11];
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader ();
		int contador = 0;
		while(reader.Read ()){
			sitios[contador] = reader.GetString(0).ToUpper().Replace("Ã¡", "À").Replace ("Ã©","É").Replace ("Ã³","Ó").Replace ("Ã±","Ñ");
			contador++;
		}
		return sitios;
	}
	public string[] obtieneDatosSitio(int idSitio){
		string query = "SELECT * FROM sitio WHERE id_sitio = "+idSitio;	
		string[] datosSitio = new string[9];
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader();
		int contador = 0;
		Debug.Log (reader.FieldCount);
		reader.Read ();
		while(contador < reader.FieldCount){
			datosSitio[contador] = reader.GetString(contador).ToString().Replace ("Ã¡", "á").Replace ("Ã©","é").Replace ("Ã­","í").Replace ("Ã³","ó").Replace ("Ãº","ú").Replace ("Ã±","ñ");
			contador++;
		}
		return datosSitio;
	}
	public string[,] obtieneLocalizaciones(){
		string query = "SELECT * FROM sitio";
		string[,] localizaciones = new string[11,3];
		dbcmd = dbcon.CreateCommand();
		dbcmd.CommandText = query;
		reader = dbcmd.ExecuteReader ();
		int contador = 0;
		while(reader.Read ()){
			localizaciones[contador,0] = reader.GetString(0);
			localizaciones[contador,1] = reader.GetString(5);
			localizaciones[contador,2] = reader.GetString(6);
			contador++;
		}
		return localizaciones;
	}





}
