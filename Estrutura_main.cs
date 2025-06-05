using DWM.ExSw.Addin.Base; // Contém AssemblyStructureParser
using DWM.TaskPaneHost;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using static DWM.ExSw.Addin.Base.AssemblyStructureParser;


namespace DWM.ExSw.Addin
{
    public partial class Estrutura_main : Form
    {
        private TaskpaneHostUI Taskpane; // Mantido se usado em outros lugares
        private SldWorks swApp;
        private ModelDoc2 swModel;
        private AssemblyStructureParser structureParser; // Instância do nosso parser

        // Construtor modificado para inicializar o parser
        public Estrutura_main(TaskpaneHostUI taskpane, ModelDoc2 model, SldWorks app) // Corrigido tipo do TaskpaneHostUI
        {
            InitializeComponent();
            Taskpane = taskpane; // Mantido
            swModel = model; // O ModelDoc2 ativo
            swApp = app;
            structureParser = new AssemblyStructureParser(); // Inicializa o parser

        }

        public void GetValues(string codigo)
        {
            // Seu código para Dt_EstruturaBD permanece o mesmo.
            Dictionary<string, List<string>> estrutura = new Dictionary<string, List<string>>();
            estrutura = Taskpane.banco.GetEstrutura(codigo); // Assumindo que Taskpane e banco estão corretos
            Dt_EstruturaBD.Rows.Clear();
            if (codigo != null)
            {
                foreach (var item in estrutura)
                {
                    List<string> valores = item.Value;
                    string coluna1 = valores[2];
                    string coluna2 = valores[1];
                    string coluna3 = valores[3];
                    string coluna4 = valores[4];
                    string coluna5 = valores[5];
                    Dt_EstruturaBD.Rows.Add(coluna1, coluna2, coluna3, coluna4, coluna5);
                }
            }
            Console.WriteLine("GetValues chamado com: " + codigo); // Placeholder
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
            tree_estrutura.Nodes.Clear();

            if (swModel == null || swModel.GetType() != (int)swDocumentTypes_e.swDocASSEMBLY)
            {
                MessageBox.Show("Por favor, abra um documento de montagem do SOLIDWORKS.", "Nenhuma Montagem Ativa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AssemblyDoc swAssyDoc = (AssemblyDoc)swModel;
            swSpecialComands cmd = new swSpecialComands(); // Supondo que esta classe está definida

            // Obtém o nome/código da montagem principal.
            // Este será usado tanto para o texto do nó quanto para o 'Tag' (código para GetValues).
            string nomeMontagemPrincipal = cmd.sw_GetNameFile(swModel);
            if (string.IsNullOrEmpty(nomeMontagemPrincipal))
            {
                nomeMontagemPrincipal = "Montagem Principal"; // Fallback
            }

            // Cria o nó raiz para a montagem principal
            TreeNode noRaizMontagem = new TreeNode(nomeMontagemPrincipal);
            noRaizMontagem.Tag = nomeMontagemPrincipal; // Armazena o código/nome no Tag para ser usado por GetValues

            // Obtém os componentes de nível 1
            List<AssemblyStructureParser.TreeNodeModel> topLevelModels = structureParser.GetTopLevelNodes(swAssyDoc);

            // Ordena os componentes de nível 1 pelo nome
            var sortedTopLevelModels = topLevelModels.OrderBy(model => model.Name).ToList();

            // Adiciona os componentes de nível 1 como filhos do nó da montagem principal
            foreach (AssemblyStructureParser.TreeNodeModel modelFilho in sortedTopLevelModels)
            {
                TreeNode uiNodeFilho = new TreeNode(modelFilho.Name);
                uiNodeFilho.Tag = modelFilho; // Armazena o TreeNodeModel do componente no Tag

                if (modelFilho.HasSwChildren())
                {
                    uiNodeFilho.Nodes.Add(new TreeNode("Carregando...")); // Nó dummy para lazy loading
                }
                noRaizMontagem.Nodes.Add(uiNodeFilho);
            }

            // Adiciona o nó da montagem principal (com seus filhos) à TreeView
            tree_estrutura.Nodes.Add(noRaizMontagem);

            // Opcionalmente, expande o nó da montagem principal automaticamente
            if (noRaizMontagem.Nodes.Count > 0)
            {
                noRaizMontagem.Expand();
            }
        }

        private void tree_estrutura_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode expandingUiNode = e.Node;
            // Apenas processa se o Tag for um TreeNodeModel (ou seja, um componente, não a montagem raiz)
            if (expandingUiNode.Tag is AssemblyStructureParser.TreeNodeModel model)
            {
                if (!model.AreChildrenLoaded)
                {
                    expandingUiNode.Nodes.Clear();
                    structureParser.LoadChildrenForNodeModel(model);
                    var sortedChildren = model.Children.OrderBy(childModel => childModel.Name).ToList();

                    foreach (AssemblyStructureParser.TreeNodeModel childModel in sortedChildren)
                    {
                        TreeNode uiChildNode = new TreeNode(childModel.Name);
                        uiChildNode.Tag = childModel;
                        if (childModel.HasSwChildren())
                        {
                            uiChildNode.Nodes.Add(new TreeNode("Carregando..."));
                        }
                        expandingUiNode.Nodes.Add(uiChildNode);
                    }
                }
            }// Marca que os filhos da UI foram carregados (AreChildrenLoaded no modelo de dados já foi setado em LoadChildrenForNodeModel)
                    
        }

        private void Form1_FormClosed(Object sender, FormClosedEventArgs e) // O nome do método deve corresponder ao evento
        {
            if (Taskpane != null) Taskpane.formEstrutura = null; // Mantido
            Console.WriteLine("Form Estrutura_main fechado."); // Placeholder
        }

        private void tree_estrutura_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                if (e.Node.Tag is AssemblyStructureParser.TreeNodeModel selectedComponentModel)
                {
                    // É um componente
                    Console.WriteLine($"Componente selecionado: {selectedComponentModel.Name}, Caminho: {selectedComponentModel.UniqueID}");
                    ModelDoc2 model = (ModelDoc2)selectedComponentModel.Component.GetModelDoc2();
                    swSpecialComands cmd = new swSpecialComands();
                    string var;
                    cmd.sw_GetCustomProperty("Comprimento", model, "", out var);
                    swModel.ClearSelection();
                    cmd.SW_SelectOBJ(swModel, selectedComponentModel.Component);

                    GetValues(var);
                }
                else if (e.Node.Tag is string assemblyCode)
                {
                    // É o nó da montagem principal (assumindo que o Tag é a string do código/nome)
                    Console.WriteLine($"Montagem principal selecionada: {assemblyCode}");
                    swSpecialComands cmd = new swSpecialComands();
                    string var;
                    cmd.sw_GetCustomProperty("Comprimento", swModel, "", out var);
                    swModel.ClearSelection();
                    cmd.SW_SelectOBJ(swModel, swModel);

                    GetValues(var);
                }
            }
            
        }

        private void btn_validate_Click(object sender, EventArgs e)
        {

        }
    }
}
