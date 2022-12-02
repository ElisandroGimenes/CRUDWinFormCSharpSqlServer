using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CRUDWindowsFormCSharpSqlServer
{
    
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        SqlDataAdapter da;

        String strSql;
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            //String conexão com banco de dados
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-CCH60LK6;Initial Catalog=teste;Integrated Security=True");

            //String de inserção no banco de dados
            string strSql = "INSERT INTO pessoas (id, nome, email, telefone) VALUES (@id, @nome, @email, @telefone)";
            Random numeroID = new Random();
            numeroID.Next();

            try
            {
                //Cria um objeto do tipo comando passando com parâmetros as strings sql e conn 
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //Insere os dados  da txtBox no comando sql
                cmd.Parameters.Add(new SqlParameter("@id", numeroID.Next()));
                cmd.Parameters.Add(new SqlParameter("@nome", this.txtNome.Text));
                cmd.Parameters.Add(new SqlParameter("@email", this.txtEmail.Text));
                cmd.Parameters.Add(new SqlParameter("@telefone", this.txtTelefone.Text));

                //Abre a conexão com o banco
                conn.Open();

                //Executa o comando Sql
                cmd.ExecuteNonQuery();

                //Fecha a conexão com o banco
                conn.Close();

                MessageBox.Show("Registro salvo com sucesso!");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ocorreu o erro de conexão: " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnExibir_Click(object sender, EventArgs e)
        {
            
            try
            {
                //String conexão com banco de dados
                SqlConnection conn = new SqlConnection("Data Source=LAPTOP-CCH60LK6;Initial Catalog=teste;Integrated Security=True");

                //String de inserção no banco de dados
                string strSql = "SELECT * FROM pessoas"; 
                
                DataSet ds = new DataSet();

                da = new SqlDataAdapter(strSql, conn);
               
                //Cria um objeto do tipo comando passando com parâmetros as strings sql e conn 
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //Abre a conexão com o banco
                conn.Open();

                //Preenche os campos
                da.Fill(ds);

                //Exibe no data gride view
                DgvDados.DataSource = ds.Tables[0];

                //Fecha a conexão com o banco
                conn.Close();

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ocorreu o erro de conexão: " + ex);
            }
            finally
            {           
                conn= null;
            }
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            //String conexão com banco de dados
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-CCH60LK6;Initial Catalog=teste;Integrated Security=True");

            //String de inserção no banco de dados
            string strSql = "SELECT * FROM pessoas WHERE Id = @id";
            
            try
            {
                //Cria um objeto do tipo comando passando com parâmetros as strings sql e conn 
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //Insere os dados  da txtBox no comando sql
                cmd.Parameters.AddWithValue("@id", txtID.Text);
               
                //Abre a conexão com o banco
                conn.Open();

                reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    txtNome.Text = (string)reader["nome"];
                    txtEmail.Text = (string)reader["email"];
                    txtTelefone.Text = Convert.ToString(reader["telefone"]);
                }               
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ocorreu o erro de conexão: " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            //String conexão com banco de dados
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-CCH60LK6;Initial Catalog=teste;Integrated Security=True");

            //String de inserção no banco de dados
            string strSql = "UPDATE pessoas SET nome = @nome, email = @email, telefone = @telefone WHERE id = @id";

            try
            {
                //Cria um objeto do tipo comando passando com parâmetros as strings sql e conn 
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //Insere os dados  da txtBox no comando sql
                cmd.Parameters.Add(new SqlParameter("@id", this.txtID.Text));
                cmd.Parameters.Add(new SqlParameter("@nome", this.txtNome.Text));
                cmd.Parameters.Add(new SqlParameter("@email", this.txtEmail.Text));
                cmd.Parameters.Add(new SqlParameter("@telefone", this.txtTelefone.Text));

                //Abre a conexão com o banco
                conn.Open();

                //Executa o comando Sql
                cmd.ExecuteNonQuery();

                //Fecha a conexão com o banco
                conn.Close();

                MessageBox.Show("Alteração salva com sucesso!");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ocorreu o erro de conexão: " + ex);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            //String conexão com banco de dados
            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-CCH60LK6;Initial Catalog=teste;Integrated Security=True");

            //String de inserção no banco de dados
            string strSql = "DELETE pessoas WHERE id = @id";
          
            try
            {
                //Cria um objeto do tipo comando passando com parâmetros as strings sql e conn 
                SqlCommand cmd = new SqlCommand(strSql, conn);

                //Insere os dados  da txtBox no comando sql
                cmd.Parameters.Add(new SqlParameter("@id", txtID.Text));
                
                //Abre a conexão com o banco
                conn.Open();

                //Executa o comando Sql
                cmd.ExecuteNonQuery();

                //Fecha a conexão com o banco
                conn.Close();

                MessageBox.Show("Registro excluído com sucesso!");

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Ocorreu o erro de conexão: " + ex);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}



