using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DWM.ExSw.Addin.DataSRV
{
    public class cardallData
    {
        public List<string> codigo_DATA { get; set; } = new List<string>();
        public List<string> comercial_DATA { get; set; } = new List<string>();
        public List<string> descricao_DATA { get; set; } = new List<string>();
        public List<string> um_DATA { get; set; } = new List<string>();
        public List<string> tipo_DATA { get; set; } = new List<string>();
        public List<string> peso_DATA { get; set; } = new List<string>();


        public void Main()
        {
            #region CONSTANTES
            string serverAddress = "192.168.2.234";
            string username = "ska";
            string password = "0$6n?3flB<8>,bJi<";
            string databaseName = "DADOSADV2";
            string connectionString = $"Data Source={serverAddress};Initial Catalog={databaseName};User ID={username};Password={password};";
            #endregion

            // Objeto de conexão com o servidor
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            try
            {
                string query = "SELECT * FROM dbo.produtos;";
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    #region DATA VALUES
                    string codigo = reader["B1_COD"].ToString();
                    string descricao = reader["B1_DESC"].ToString();
                    string um = reader["B1_UM"].ToString();
                    string peso = reader["B1_PESOCAD"].ToString();
                    string tipo = reader["B1_TIPO"].ToString();
                    string subgrupo = reader["B1_SUBGRUP"].ToString();
                    string grupo = reader["B1_GRUPO"].ToString();
                    string desc = reader["ZZ5_DESCRI"].ToString();
                    #endregion

                    #region REMOVE ESPAÇOS
                    while (codigo.EndsWith(" "))
                    {
                        codigo = codigo.TrimEnd();
                    }

                    while (descricao.EndsWith(" "))
                    {
                        descricao = descricao.TrimEnd();
                    }

                    while (tipo.EndsWith(" "))
                    {
                        tipo = tipo.TrimEnd();
                    }

                    while (subgrupo.EndsWith(" "))
                    {
                        subgrupo = subgrupo.TrimEnd();
                    }

                    while (grupo.EndsWith(" "))
                    {
                        grupo = grupo.TrimEnd();
                    }

                    while (desc.EndsWith(" "))
                    {
                        desc = desc.TrimEnd();
                    }
                    while (um.EndsWith(" "))
                    {
                        um = um.TrimEnd();
                    }
                    #endregion

                    string linha;
                    if (codigo.Length == 6)
                    {
                        linha = $"{codigo};{um};;1;{descricao}";
                        comercial_DATA.Add(codigo);
                        um_DATA.Add(um);
                        descricao_DATA.Add(descricao);
                    }
                    //else
                    //{
                    //    codigo_DATA.Add(codigo);
                    //    descricao_DATA.Add(descricao);
                    //    peso_DATA.Add(peso);
                    //}
                }
                if (reader != null)
                    reader.Close();

                if (connection.State == ConnectionState.Open)
                    connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar banco de dados: " + ex.Message);
            }

        }

    }
}
