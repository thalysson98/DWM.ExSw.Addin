using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Vortex.Addin.PartData.Core
{

    public class SQLCommands
    {
        SqlConnection connection;
        public SqlConnection Connect()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "192.168.2.248\\ERASQL",
                UserID = "PDBLogin",
                Password = "eng.2003",
                InitialCatalog = "PDB_BANCO",
                IntegratedSecurity = true

            };
            var connectionString = builder.ConnectionString;

            try
            {
                connection = new SqlConnection(connectionString);
                return connection;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar ao banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public bool oncon()
        {
            using (connection = Connect())
            {
                if (connection != null) { return false; }
            }
            return true;
        }

        public void Disconnect(SqlConnection connection)
        {
            try
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao desconectar do banco de dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool InsertCategoria(string categoria, string tipo)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = "INSERT INTO CATEGORIAS (MATERIAL, TIPO) VALUES (@material, @tipo)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@material", categoria ?? "");
                        command.Parameters.AddWithValue("@tipo", tipo ?? "");
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Categoria cadastrada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao cadastrar categoria: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool DeleteItem(string tabela, string item, string columnTable)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return false;

                    string query = $"DELETE FROM {tabela} WHERE {columnTable} = @item";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@item", item);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            MessageBox.Show("Item excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Nenhum item encontrado para exclusão!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir item: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public List<string> GetValColumn(string coluna, string table)
        {
            List<string> valoresUnicos = new List<string>();
            try
            {
                using (connection)
                {
                    if (connection == null) return valoresUnicos;

                    string query = $"SELECT DISTINCT {coluna} FROM {table} ORDER BY {coluna}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                valoresUnicos.Add(reader[coluna].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar valores da coluna: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valoresUnicos;
        }

        public List<string> GetRowValues(Dictionary<string, object> filtros, List<string> colunasDesejadas, string TABLE)
        {
            List<string> valoresLinha = new List<string>();
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return valoresLinha;

                    string colunas = string.Join(", ", colunasDesejadas);
                    string whereClause = string.Join(" AND ", filtros.Select(f => f.Key + " = '" + f.Value + "'"));
                    string query = $"SELECT {colunas} FROM {TABLE} WHERE {whereClause}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string value = reader[i].ToString().Trim();
                                    if (!valoresLinha.Contains(value) && value != "0.0")
                                    {
                                        valoresLinha.Add(value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar valores da linha: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valoresLinha;
        }
        public List<List<string>> GetAllValues(List<string> colunasDesejadas, string TABLE)
        {
            List<List<string>> valoresLinha = new List<List<string>>();
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return valoresLinha;

                    string colunas = string.Join(", ", colunasDesejadas);
                    string query = $"SELECT {colunas} FROM {TABLE}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                List<string> linha = new List<string>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    linha.Add(reader[i].ToString().Trim());
                                }
                                valoresLinha.Add(linha);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar todos os valores: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return valoresLinha;
        }
        public void RemoverDuplicatas(string tableName, string idColumn, List<string> colunas)
        {
            using (SqlConnection connection = Connect())
            {
                string colunasJoin = string.Join(", ", colunas);

                string query = $@"
            WITH CTE_Duplicates AS (
                SELECT 
                    {idColumn},
                    ROW_NUMBER() OVER (
                        PARTITION BY {colunasJoin} 
                        ORDER BY {idColumn} -- Ordenando pela chave primária para manter o menor ID
                    ) AS row_num
                FROM {tableName}
            )
            DELETE FROM {tableName} WHERE {idColumn} IN (
                SELECT {idColumn} FROM CTE_Duplicates WHERE row_num > 1
            );";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine($"{rowsAffected} linhas duplicadas removidas.");
            }
        }

        public bool CadastrarItensDataGrid(DataGridView dataGrid,string User)
        {
            using (SqlConnection connection = Connect())
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction()) // Usar transação para segurança
                {
                    try
                    {
                        string query = @"
                    INSERT INTO MATERIAIS (CATEGORIA, DIAMETRO, ESPESSURA, COMPRIMENTO, M4, COD1, COD2, COD3,CAD_POR) 
                    VALUES (@CATEGORIA, @DIAMETRO, @ESPESSURA, @COMPRIMENTO, @M4, @COD1, @COD2, @COD3,@CAD_POR)";

                        foreach (DataGridViewRow row in dataGrid.Rows)
                        {
                            if (!row.IsNewRow) // Evita erro ao processar a linha vazia no final
                            {
                                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                                {
                                    // Pegando os valores a partir da segunda coluna (index 1)
                                    command.Parameters.AddWithValue("@CATEGORIA", row.Cells[1].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@DIAMETRO", row.Cells[2].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@ESPESSURA", row.Cells[3].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@COMPRIMENTO", row.Cells[4].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@M4", row.Cells[5].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@COD1", row.Cells[6].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@COD2", row.Cells[7].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@COD3", row.Cells[8].Value?.ToString() ?? "");
                                    command.Parameters.AddWithValue("@CAD_POR", User ?? "");
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        transaction.Commit(); // Confirma a transação se tudo deu certo
                        Console.WriteLine("Todos os itens foram cadastrados com sucesso!");
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Desfaz a transação em caso de erro
                        Console.WriteLine("Erro ao cadastrar os itens: " + ex.Message);
                        return false;
                    }
                }
            }
        }

        public void ExcluirMateriaisPorCategoria(string categoria)
        {
            try
            {
                using (SqlConnection connection = Connect())
                {
                    if (connection == null) return;

                    string query = "DELETE FROM MATERIAIS WHERE CATEGORIA = @CATEGORIA";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CATEGORIA", categoria);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        MessageBox.Show($"{rowsAffected} materiais da categoria '{categoria}' foram excluídos.",
                            "Exclusão Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao excluir materiais da categoria: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
