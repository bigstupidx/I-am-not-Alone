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
    CheckInWeaponAndCraft checkInWeaponAndCraft;
    MainMenu mainmenu;
    string filepath;




    public void OpenDB (string p)
    {

        // check if file exists in Application.persistentDataPath


        //#if UNITY_ANDROID
        //  filepath = Application.persistentDataPath + "/" + p;
        //#elif UNITY_EDITOR
        //        filepath = Application.dataPath + "/" + p;
        //#endif
        filepath = Application.dataPath + "/" + p;








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


        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();



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


                        checkInWeaponAndCraft.WeaponBought.Add(new ParamsDbBoughtWeaponAndCraftItem(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2)));


                    }
                    dbconnection.Close();
                    reader.Close();
                }


            }

        }



    }
    public void GetCraftItemBought ()
    {


        checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();



        using (IDbConnection dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            using (IDbCommand dcm = dbconnection.CreateCommand())
            {


                string sqlQuery = String.Format("SELECT NameCraft , LevelItem  FROM PlayerCraftItem");

                dcm.CommandText = sqlQuery;
                using (IDataReader reader = dcm.ExecuteReader())
                {
                    while (reader.Read())
                    {


                        checkInWeaponAndCraft.CraftItemBought.Add(new ParamsDbBoughtWeaponAndCraftItem(reader.GetString(0), reader.GetInt32(1), 0));


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

    public string GetLanguage (string idLanguage)
    {

        using (IDbConnection dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            using (IDbCommand dcm = dbconnection.CreateCommand())
            {


                string sqlQuery = String.Format("SELECT  Language  FROM PlayerParams");

                dcm.CommandText = sqlQuery;
                using (IDataReader reader = dcm.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        idLanguage = reader.GetString(0);

                    }
                    dbconnection.Close();
                    reader.Close();
                    return idLanguage;
                }


            }

        }


    }
    public void UpdateLanguage (String idLanguage)
    {

        using (IDbConnection dbconnetcion = new SqliteConnection(connection))
        {

            dbconnetcion.Open();
            using (IDbCommand dcm = dbconnetcion.CreateCommand())
            {

                string sqlQuery = String.Format("UPDATE  PlayerParams  SET  Language = \"{0}\"", idLanguage);



                dcm.CommandText = sqlQuery;

                dcm.ExecuteScalar();
                dbconnetcion.Close();

            }

        }


    }
    public void GetMoney ()
    {

        using (IDbConnection dbconnection = new SqliteConnection(connection))
        {
            dbconnection.Open();
            using (IDbCommand dcm = dbconnection.CreateCommand())
            {


                string sqlQuery = String.Format("SELECT  Money  FROM PlayerParams");

                dcm.CommandText = sqlQuery;
                using (IDataReader reader = dcm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (mainmenu = GetComponent<MainMenu>())
                        {
                            mainmenu = GetComponent<MainMenu>();
                            mainmenu.myMoney.text = reader.GetString(0);

                        }
                        else if (checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>())
                        {
                            checkInWeaponAndCraft = GetComponent<CheckInWeaponAndCraft>();
                            checkInWeaponAndCraft.MyMoney.text = reader.GetString(0);
                        }




                    }
                    dbconnection.Close();
                    reader.Close();
                }


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
    public void InsertDBWeapon (string WeaponName, int level, int category)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO PlayerItemParams VALUES(\"{0}\",\"{1}\",\"{2}\")", WeaponName, level, category);

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
    public void InsertDBCraft (string NameCraft, int level)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("INSERT INTO PlayerCraftItem VALUES(\"{0}\",\"{1}\")", NameCraft, level);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();



            }


        }

    }
    public void UpdateDBCraft (string NameCraft, int level)
    {




        using (IDbConnection dbConnection = new SqliteConnection(connection))
        {

            dbConnection.Open();
            using (IDbCommand dbCmd = dbConnection.CreateCommand())
            {
                string sqlQuery = String.Format("UPDATE PlayerCraftItem SET   LevelItem =\"{1}\"  WHERE NameCraft =\"{0}\"", NameCraft, level);

                dbCmd.CommandText = sqlQuery;
                dbCmd.ExecuteScalar();
                dbConnection.Close();



            }


        }

    }
}

