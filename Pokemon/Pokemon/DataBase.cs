using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Pokemon
{
    class DataBase
    {
        static SqlConnection client = new SqlConnection();

        public DataBase()
        {
            string relativePath = "PokemonDB.mdf";
            string absolutePath = Application.StartupPath + "\\" + relativePath;
            string connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + "\"" + absolutePath + "\"" + ";Integrated Security=True;Connect Timeout=30";
            client.ConnectionString = connectionString;

            OpenIfNotOpen();
        }

        static private void OpenIfNotOpen()
        {
            if (client.State != ConnectionState.Open) client.Open();
        }

        static public List<List<string>> GetData(string query)
        {
            OpenIfNotOpen();

            List<List<string>> ret = new List<List<string>>();

            SqlCommand cmd = new SqlCommand(query, client);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ret.Add(new List<string>());
                for (int i = 1; i < sdr.FieldCount; ++i) ret[ret.Count - 1].Add(sdr[i] + "");
            }

            sdr.Close();
            sdr.Dispose();
            cmd.Dispose();

            return ret;
        }

        static public List<List<string>> GetAttack(int index, int level)
        {
            return GetData("SELECT * FROM PokeAttack WHERE PokeID = " + index + " AND Level = " + level);
        }

        static public List<List<string>> GetAttack(int index, int level, bool all)
        {
            return GetData("SELECT * FROM PokeAttack WHERE PokeID = " + index + " AND Level <= " + level);
        }

        static public List<string> GetAttack(int index)
        {
            List<List<string>> ret = GetData("SELECT * FROM AttackDex WHERE ID = " + index);
            return ret[0];
        }

        static public List<string> GetPokemon(int index)
        {
            List<List<string>> ret = GetData("SELECT * FROM PokeDex WHERE ID = " + index);
            return ret[0];
        }

        static public bool PokeCanLearnTM(int moveindex, int pokeindex)
        {
            List<List<string>> ret = GetData("SELECT * FROM TMDex WHERE AttackID = " + moveindex + " AND PokeID = " + pokeindex);
            if (ret.Count > 0) return true;
            return false;
        }
    
        #region Admin Panel
        
        static public List<List<string>> AdminGetData(string query)
        {
            OpenIfNotOpen();

            List<List<string>> ret = new List<List<string>>();

            SqlCommand cmd = new SqlCommand(query, client);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                ret.Add(new List<string>());
                for (int i = 0; i < sdr.FieldCount; ++i) ret[ret.Count - 1].Add(sdr[i] + "");
            }

            sdr.Close();
            sdr.Dispose();
            cmd.Dispose();

            return ret;
        }

        static public void AdminUpdate(string query)
        {
            OpenIfNotOpen();

            SqlCommand cmd = new SqlCommand(query, client);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }

        #endregion
    }
}
