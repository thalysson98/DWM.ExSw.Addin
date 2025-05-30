using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Presumindo que esta classe faz parte de um contexto maior
// onde 'SldWorks' é a referência para a API do SOLIDWORKS.
namespace DWM.ExSw.Addin.Base
{
    public class AssemblyStructureParser
    {
        /// <summary>
        /// Representa um nó na árvore de componentes, adequado para um TreeView com lazy loading.
        /// </summary>
        public class TreeNodeModel
        {
            /// <summary>
            /// Nome do componente para exibição.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// O objeto Component2 real do SOLIDWORKS.
            /// </summary>
            public Component2 Component { get; set; }

            /// <summary>
            /// Lista de nós filhos deste componente. Será populada sob demanda.
            /// </summary>
            public List<TreeNodeModel> Children { get; set; }

            /// <summary>
            /// Um identificador único para o componente, útil para chaves no TreeView.
            /// Ex: Caminho do componente.
            /// </summary>
            public string UniqueID { get; set; }

            /// <summary>
            /// Indica se os filhos deste nó já foram carregados.
            /// </summary>
            public bool AreChildrenLoaded { get; set; }

            public TreeNodeModel()
            {
                Children = new List<TreeNodeModel>();
                AreChildrenLoaded = false; // Inicialmente, os filhos não estão carregados.
            }

            /// <summary>
            /// Verifica se o componente associado a este nó possui filhos no SOLIDWORKS.
            /// Não carrega os filhos, apenas verifica a existência.
            /// </summary>
            /// <returns>True se o componente tiver filhos, false caso contrário.</returns>
            public bool HasSwChildren()
            {
                if (Component == null) return false;
                object[] vChildren = (object[])Component.GetChildren();
                return vChildren != null && vChildren.Length > 0;
            }
        }

        /// <summary>
        /// Obtém apenas os componentes de nível 1 (raiz) da montagem.
        /// </summary>
        /// <param name="assyDoc">O documento de montagem do SOLIDWORKS.</param>
        /// <returns>Uma lista de TreeNodeModel representando os componentes de nível superior.</returns>
        public List<TreeNodeModel> GetTopLevelNodes(AssemblyDoc assyDoc)
        {
            List<TreeNodeModel> topLevelNodes = new List<TreeNodeModel>();
            if (assyDoc == null)
            {
                Debug.WriteLine("AssemblyDoc fornecido é nulo.");
                return topLevelNodes;
            }

            // GetComponents(false) obtém apenas os componentes de nível superior.
            object[] vRootComps = (object[])assyDoc.GetComponents(false);

            if (vRootComps != null)
            {
                foreach (object compObj in vRootComps)
                {
                    Component2 rootSwComp = compObj as Component2;
                    if (rootSwComp != null && !rootSwComp.IsSuppressed())
                    {
                        TreeNodeModel node = CreateSingleNodeModel(rootSwComp);
                        if (node != null)
                        {
                            topLevelNodes.Add(node);
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Nenhum componente de nível superior encontrado na montagem.");
            }
            return topLevelNodes;
        }

        /// <summary>
        /// Cria um modelo de nó para um único componente do SOLIDWORKS, sem carregar seus filhos recursivamente.
        /// </summary>
        /// <param name="swComponent">O componente do SOLIDWORKS.</param>
        /// <returns>Um TreeNodeModel para o componente, ou null se o componente for inválido ou suprimido.</returns>
        private TreeNodeModel CreateSingleNodeModel(Component2 swComponent)
        {
            if (swComponent == null || swComponent.IsSuppressed())
            {
                return null;
            }

            return new TreeNodeModel
            {
                Name = swComponent.Name2, // Nome de exibição
                Component = swComponent,    // Referência ao objeto Component2
                UniqueID = swComponent.GetPathName(), // ID único
                AreChildrenLoaded = false // Filhos não são carregados aqui
            };
        }

        /// <summary>
        /// Carrega os filhos diretos do componente associado ao parentNodeModel.
        /// Os filhos carregados são adicionados à coleção Children de parentNodeModel.
        /// </summary>
        /// <param name="parentNodeModel">O nó pai cujos filhos devem ser carregados.</param>
        public void LoadChildrenForNodeModel(TreeNodeModel parentNodeModel)
        {
            if (parentNodeModel == null || parentNodeModel.Component == null || parentNodeModel.AreChildrenLoaded)
            {
                // Não faz nada se o nó for nulo, não tiver componente, ou se os filhos já foram carregados.
                return;
            }

            // Limpa filhos existentes (caso seja uma recarga ou dummy nodes)
            // parentNodeModel.Children.Clear(); // Opcional, dependendo da estratégia de dummy node

            object[] vChildren = (object[])parentNodeModel.Component.GetChildren();
            if (vChildren != null)
            {
                foreach (object childObj in vChildren)
                {
                    Component2 childSwComp = childObj as Component2;
                    if (childSwComp != null && !childSwComp.IsSuppressed())
                    {
                        TreeNodeModel childNode = CreateSingleNodeModel(childSwComp);
                        if (childNode != null)
                        {
                            parentNodeModel.Children.Add(childNode);
                        }
                    }
                }
            }
            parentNodeModel.AreChildrenLoaded = true; // Marca que os filhos deste nó foram carregados.
        }


        // As funções GetCompModels_OriginalFlatList e ContainsComponent permanecem inalteradas
        // caso ainda sejam necessárias para outros propósitos.

        public Component2[] GetCompModels_OriginalFlatList(AssemblyDoc assy)
        {
            object[] vComps = (object[])assy.GetComponents(true);
            List<Component2> pecatotal = new List<Component2>();

            foreach (object comp in vComps)
            {
                Component2 swComp = (Component2)comp;
                if (!swComp.IsSuppressed())
                {
                    if (!ContainsComponent(pecatotal.ToArray(), swComp))
                    {
                        pecatotal.Add(swComp);
                    }
                }
            }

            if (pecatotal.Count == 0)
            {
                return new Component2[0];
            }
            else
            {
                return pecatotal.ToArray();
            }
        }

        private bool ContainsComponent(Component2[] comps, Component2 swComp)
        {
            foreach (Component2 thisComp in comps)
            {
                if (thisComp.GetPathName() == swComp.GetPathName())
                {
                    return true;
                }
            }
            return false;
        }
    }

    /*
    ---------------------------------------------------------------------------------
    Exemplo de como integrar com um TreeView do Windows Forms (coloque no seu Form):
    ---------------------------------------------------------------------------------

    // Supondo que você tenha:
    // - um controle TreeView chamado 'myTreeView'
    // - um botão chamado 'btn_atualizar'
    // - uma instância do AssemblyStructureParser:
    //   AssemblyStructureParser structureParser = new AssemblyStructureParser();
    // - uma referência ao documento de montagem ativo do SOLIDWORKS:
    //   AssemblyDoc swAssyDoc = (AssemblyDoc)swApp.ActiveDoc; (onde swApp é ISldWorks)

    private void btn_atualizar_Click(object sender, EventArgs e)
    {
        if (swAssyDoc == null)
        {
            MessageBox.Show("Nenhum documento de montagem ativo.");
            return;
        }

        myTreeView.Nodes.Clear(); // Limpa nós existentes

        List<AssemblyStructureParser.TreeNodeModel> topLevelModels = structureParser.GetTopLevelNodes(swAssyDoc);

        foreach (AssemblyStructureParser.TreeNodeModel model in topLevelModels)
        {
            TreeNode uiNode = new TreeNode(model.Name);
            uiNode.Tag = model; // Armazena o modelo de dados no Tag do nó da UI

            // Adiciona um nó "dummy" se o componente tiver filhos, para que o TreeView mostre o sinal de [+]
            // Isso sinaliza ao usuário que o nó pode ser expandido.
            if (model.HasSwChildren())
            {
                uiNode.Nodes.Add(new TreeNode("Carregando...")); // Nó temporário
            }
            myTreeView.Nodes.Add(uiNode);
        }
    }

    private void myTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        TreeNode expandingUiNode = e.Node;
        AssemblyStructureParser.TreeNodeModel model = expandingUiNode.Tag as AssemblyStructureParser.TreeNodeModel;

        if (model != null && !model.AreChildrenLoaded)
        {
            // Remove o nó "dummy" (se existir)
            expandingUiNode.Nodes.Clear();

            // Carrega os filhos reais do modelo de dados
            structureParser.LoadChildrenForNodeModel(model);

            // Popula a UI com os filhos carregados
            foreach (AssemblyStructureParser.TreeNodeModel childModel in model.Children)
            {
                TreeNode uiChildNode = new TreeNode(childModel.Name);
                uiChildNode.Tag = childModel;

                // Adiciona um nó "dummy" para o filho, se ele também tiver filhos não carregados
                if (childModel.HasSwChildren())
                {
                    uiChildNode.Nodes.Add(new TreeNode("Carregando..."));
                }
                expandingUiNode.Nodes.Add(uiChildNode);
            }
        }
    }
    */
}