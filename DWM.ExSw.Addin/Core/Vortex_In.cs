using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DWM.ExSw.Addin.Core
{
    public class Vortex_In
    {
        TcpClient client;
        NetworkStream stream;
        //Abre Vortex pelo solid
        public bool OpenVortex()
        {
            Process[] processos = Process.GetProcessesByName("VortexIntegration");
            if (processos.Length == 0)
            {
                //string caminhoAplicacao = Path.Combine(Path.GetDirectoryName(typeof(TaskpaneIntegration).Assembly.CodeBase).Replace(@"file:\", ""), "VortexIntegration.exe");
                string caminhoAplicacao = "C:\\Users\\thaly\\OneDrive\\Documentos\\GitHub\\VortexIntegration\\bin\\Debug\\VortexIntegration.exe";
                try
                {
                    Process.Start(caminhoAplicacao);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }
        void command(string command)
        {

            try
            {
                if (client != null && stream != null)
                {
                    byte[] commandBytes = Encoding.UTF8.GetBytes(command);
                    stream.Write(commandBytes, 0, commandBytes.Length);
                    //client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }

        }
        string response()
        {
            string res = "";
            try
            {
                if (client != null && stream != null)
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    res = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //client.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            return res;
        }
        //Verifica se o login esta feito
        public bool is_loged()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();
                command("login");
                if(response() == "true") 
                {
                    client.Close();
                    return true; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            return false;
        }

        //Abre janela Vortex
        public bool showVortex()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 5000);
                stream = client.GetStream();
                command("showvortex");
                client.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            return false;
        }


    }
}
