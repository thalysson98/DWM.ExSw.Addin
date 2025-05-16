using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace DWM.ExSw.Addin.Core
{
    public class Vortex_In
    {
        bool response { get; set; }
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
        //Verifica se o login esta feito
        public bool is_loged()
        {
            command("login");
            if(response == true) 
            {
                return true;
            }
            else { return false; }
        }

        //Abre janela Vortex
        public bool showVortex()
        {
            try
            {
               command("showvortex");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
            return false;
        }

        public async void command(string command)
        {
            try
            {
                using (var pipeClient = new NamedPipeClientStream(".", "b4da2b18-49fc-4b83-89a6-19b73e754ee3", PipeDirection.InOut))
                {
                    await pipeClient.ConnectAsync(); // Aguarda conexão com o servidor

                    using (var writer = new StreamWriter(pipeClient) { AutoFlush = true })
                    using (var reader = new StreamReader(pipeClient))
                    {
                        // Envia o comando ao servidor
                        await writer.WriteLineAsync(command);

                        // Aguarda a resposta do servidor
                        var response_ = await reader.ReadLineAsync();
                        if (response_ == "true") { response = true; } else {  response = false; }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"Erro ao enviar comando: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
