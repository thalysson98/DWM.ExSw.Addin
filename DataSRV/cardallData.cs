using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Text.RegularExpressions;

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

        public Dictionary<string, List<string>> EstruturaBanco { get; set; } = new Dictionary<string, List<string>>();

        #region Dados Banco
        string serverAddress = "192.168.2.234";
        string username = "ska";
        string password = "0$6n?3flB<8>,bJi<";
        string databaseName = "DADOSADV2";
        #endregion


        public void Main()
        {
            string connectionString = $"Data Source={serverAddress};Initial Catalog={databaseName};User ID={username};Password={password};";

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

        public Dictionary<string, List<string>> GetEstrutura(string codigo)
        {
            databaseName = "TESTE";
            string connectionString = $"Data Source={serverAddress};Initial Catalog={databaseName};User ID={username};Password={password};";
            Dictionary<string, List<string>> Estrutura_ = new Dictionary<string, List<string>>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader reader = null;

            try
            {
                string query = @"
                        WITH CTE AS (
                            SELECT *,
                                ROW_NUMBER() OVER (PARTITION BY G1_COD, G1_COMP, G1_QTDUNIT,G1_LARGURA,G1_COMPR ORDER BY G1_INI DESC) AS rn
                            FROM dbo.SG1010
                            WHERE G1_COD = @codigo
                        )
                        SELECT *
                        FROM CTE
                        WHERE rn = 1
                        AND G1_INI = (
                            SELECT MAX(G1_INI)
                            FROM dbo.SG1010
                            WHERE G1_COD = @codigo
                        )";
                int j = 0;
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@codigo", codigo);
                connection.Open();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string VarCodigo = reader["G1_COD"].ToString();
                    string componente = reader["G1_COMP"].ToString();
                    string qtd = reader["G1_QTDUNIT"].ToString();
                    string larg = reader["G1_LARGURA"].ToString();
                    string comp = reader["G1_COMPR"].ToString();
                    string peso = reader["G1_QUANT"].ToString();
                    string data = reader["G1_INI"].ToString();

                    List<string> values = new List<string>() { VarCodigo, componente, qtd, larg, comp, peso, data };

                    for (int i = 0; i < values.Count; i++)
                    {
                        if(values[i] != "")
                        {
                            values[i] = values[i].TrimEnd();
                        }
                    }
                    if (values[1] != "")
                    {
                        values[1] = GetCodigoDesc(values[1]);
                    }
                    Estrutura_.Add(j.ToString(), values);
                    j++;
                }

                if (reader != null)
                    reader.Close();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return Estrutura_;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar banco de dados: " + ex.Message);
            }
            return null;
        }
        //public string VerificarCodigo(out int err, string vCod, string codigo)
        //{
        //    err = 0;
        //    string PadraoCodigo = @"^\d{3}\.\d{3}\.\d{4}$";
        //    string PadraoFicticio = @"^F\d{2}\.\d{3}\.\d{4}$";
        //    string Padraocomercial = @"^\d{6}\$";


        //    var resultado = ExtrairDados(vCod);
        //    Console.WriteLine("Código: " + resultado.Cod);
        //    Console.WriteLine("Revisão: " + resultado.Rev);
        //    Console.WriteLine("Descrição: " + resultado.Desc);


        //    if (codigo != "")//Existe a propriedade codigo
        //    {

        //    else if (!Regex.IsMatch(TextString, PadraoCodigo))//texto codigo padrao
        //        {
        //            if (Regex.IsMatch(TextString, PadraoFicticio))//texto ficticio
        //            {
        //                err = 0;
        //            }
        //            else if (Regex.IsMatch(TextString, Padraocomercial))//texto comercial
        //            {
        //                err = 0;
        //            }
        //            else
        //            {
        //                err = 2;
        //            }

        //        }

        //        return codigo;
        //    }
        //public (string Cod, string Rev, string Desc) ExtrairDados(string linha)
        //{
        //    string vCod = "";
        //    string vRev = "";
        //    string vDesc = "";

        //    // Localiza o primeiro espaço (separando código da descrição)
        //    int indiceEspaco = linha.IndexOf(' ');

        //    if (indiceEspaco > 0)
        //    {
        //        string parteInicial = linha.Substring(0, indiceEspaco); // Ex: 000.000.0000A
        //        vDesc = linha.Substring(indiceEspaco + 1).Trim();        // Resto é a descrição

        //        // Último caractere da parte inicial é a revisão
        //        if (parteInicial.Length > 0)
        //        {
        //            vRev = parteInicial.Substring(parteInicial.Length - 1);             // Ex: A
        //            vCod = parteInicial.Substring(0, parteInicial.Length - 1);         // Ex: 000.000.0000
        //        }
        //    }

        //    return (vCod, vRev, vDesc);
        //}
        public string ValidarRev(string VarCod, string vRev)
        {
            string connectionString = $"Data Source={serverAddress};Initial Catalog={databaseName};User ID={username};Password={password};";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            List<string> RevBD = new List<string>();
            try
            {
                string val = "";
                string query = $@"SELECT * 
                                    FROM dbo.SB1010 
                                    WHERE LEN(B1_COD) > 1
                                    AND LEFT(B1_COD, LEN(B1_COD) - 1) = @codBase;";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@codBase", VarCod);

                connection.Open();
                reader = command.ExecuteReader();
                string rev = "";
                while (reader.Read())
                {
                    #region DATA VALUES
                    string codigo = reader["B1_COD"].ToString();
                    string descricao = reader["B1_DESC"].ToString();
                    #endregion
                    #region REMOVE ESPAÇOS
                    List<string> values = new List<string>() { codigo, descricao };

                    for (int i = 0; i < values.Count; i++)
                    {
                        if (values[i] != "")
                        {
                            values[i] = values[i].TrimEnd();
                        }
                    }
                    #endregion
                    RevBD.Add(values[0].Substring(values[0].Length - 1));
                }

                if (reader != null)
                    reader.Close();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return RevBD.Max();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar banco de dados: " + ex.Message);
            }

            return "";
        }
        public string GetCodigoDesc(string VarCod)
        {
            string connectionString = $"Data Source={serverAddress};Initial Catalog={databaseName};User ID={username};Password={password};";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataReader reader = null;

            try
            {
                string val = "";
                string query = $"SELECT * FROM dbo.produtos WHERE B1_COD = '{VarCod}';";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@codBase", VarCod);

                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    #region DATA VALUES
                    string codigo = reader["B1_COD"].ToString();
                    string descricao = reader["B1_DESC"].ToString();
                    #endregion

                    #region REMOVE ESPAÇOS
                    List<string> values = new List<string>() { codigo, descricao};

                    for (int i = 0; i < values.Count; i++)
                    {
                        if (values[i] != "")
                        {
                            values[i] = values[i].TrimEnd();
                        }
                    }
                    #endregion
                    val = $"{values[0]} {values[1]}";

                }
                if (reader != null)
                    reader.Close();

                if (connection.State == ConnectionState.Open)
                    connection.Close();
                return val;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar banco de dados: " + ex.Message);
            }
            return "";
        }

    }
}
