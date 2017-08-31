using UnityEngine;
using System;
using System.IO;

using System.Data;

using Mono.Data.SqliteClient;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Threading;

public class DbGame : MonoBehaviour
{

    [Space(15)]
    private string connection;
    private IDbConnection dbcon;
    private IDbCommand dbcmd;
    private IDataReader reader;
    CheckInScene checkIn;
    CheckInWeapon checkInWeapon;
    string filepath;




    public void OpenDB (string p)
    {

        // check if file exists in Application.persistentDataPath





        filepath = Application.dataPath + "/" + p;


        //   filepath = Application.persistentDataPath + "/" + p;


        //  }
        //    filepath = Application.persistentDataPath + "/" + p;


        if (!File.Exists(filepath))
        {

            // if it doesn't ->
            // open StreamingAssets directory and load the db -> 
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + p);
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }

        //open db connection
        connection = "URI=file:" + filepath;

        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }


    public void GetSceneBought ()
    {
        checkIn = GetComponent<CheckInScene>();
        using (IDbConnection dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            using (IDbCommand dcm = dbconnection.CreateCommand())
            {


                string sqlQuery = String.Format("SELECT SceneName FROM SceneParams");

                dcm.CommandText = sqlQuery;
                using (IDataReader reader = dcm.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        checkIn.sceneBought.Add(reader.GetString(0));

                    }
                    dbconnection.Close();
                    reader.Close();
                }


            }

        }



    }
    public void GetWeaponBought ()
    {
        checkInWeapon = GetComponent<CheckInWeapon>();
        using (IDbConnection dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            using (IDbCommand dcm = dbconnection.CreateCommand())
            {


                string sqlQuery = String.Format("SELECT NameWeapon , LevelWeapon, Category  FROM PlayerItemParams");

                dcm.CommandText = sqlQuery;
                using (IDataReader reader = dcm.ExecuteReader())
                {
                    while (reader.Read())
                    {
             
                        checkInWeapon.WeaponBought.Add(new WeaponParams(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));
                    
                    }
                    dbconnection.Close();
                    reader.Close();
                }


            }

        }



    }

    public void InsertDBSceneName (string sceneName)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO SceneParams VALUES(\"{0}\")", sceneName);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();



            }


        }

    }
    public void UpdateMoney (string money)
    {
        using (IDbConnection dbconnetcion = new SqliteConnection(connection))
        {

            dbconnetcion.Open();
            using (IDbCommand dcm = dbconnetcion.CreateCommand())
            {

                string sqlQuery = String.Format("UPDATE  PlayerParams  SET  Money = \"{0}\"", money);



                dcm.CommandText = sqlQuery;

                dcm.ExecuteScalar();
                dbconnetcion.Close();

            }

        }



    }
    public void InsertDBWeapon (string WeaponName, int level)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO PlayerItemParams VALUES(\"{0}\",\"{1}\")", WeaponName, level);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();



            }


        }

    }
    public void UpdateDBWeapon (string WeaponName, int level)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("UPDATE PlayerItemParams SET   LevelWeapon =\"{1}\"  WHERE NameWeapon =\"{0}\"", WeaponName, level);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();



            }


        }

    }

}

