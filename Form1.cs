using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWM.ExSw.Addin
{

    // No seu Form.cs (ou onde você inicializa o TreeListView)
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            ConfigureTreeListView();
            LoadTaskData();
        }

        private void ConfigureTreeListView()
        {
            // 1. Defina as Colunas:
            // Crie as colunas e associe-as às propriedades da sua classe TaskItem.
            // A primeira coluna (Name) será a "coluna da árvore" onde os nós se expandem/contraem.

            // Coluna "Tarefa" (nome da tarefa/projeto)
            OLVColumn colName = new OLVColumn("Tarefa", "Name");
            colName.Width = 250;
            this.treeListViewTasks.Columns.Add(colName);

            // Coluna "Status"
            OLVColumn colStatus = new OLVColumn("Status", "Status");
            colStatus.Width = 100;
            this.treeListViewTasks.Columns.Add(colStatus);

            // Coluna "Vencimento" (Data de Vencimento)
            OLVColumn colDueDate = new OLVColumn("Vencimento", "DueDate");
            colDueDate.Width = 120;
            colDueDate.AspectToStringConverter = delegate (object x)
            {
                // Formata a data para uma string mais amigável
                if (x is DateTime date)
                {
                    return date.ToShortDateString();
                }
                return string.Empty;
            };
            this.treeListViewTasks.Columns.Add(colDueDate);


            // 2. Configure a Hierarquia (muito importante para o TreeListView):
            // Diga ao TreeListView como identificar se um nó pode ser expandido
            // e como obter os nós filhos de um determinado nó.
            this.treeListViewTasks.CanExpandGetter = delegate (object x)
            {
                // Um item pode ser expandido se for um TaskItem e tiver filhos
                return (x is TaskItem item) && item.Children.Any();
            };

            this.treeListViewTasks.ChildrenGetter = delegate (object x)
            {
                // Retorna a lista de filhos para o item
                if (x is TaskItem item)
                {
                    return item.GetChildren();
                }
                return null;
            };

            // Outras configurações úteis (opcional):
            this.treeListViewTasks.FullRowSelect = true; // Seleciona a linha inteira ao clicar
            this.treeListViewTasks.HideSelection = false; // Mantém a seleção visível mesmo quando o controle perde o foco
            this.treeListViewTasks.ShowGroups = false; // Não exibe grupos (comum para TreeViews)
        }

        private void LoadTaskData()
        {
            // 3. Crie seus Dados Hierárquicos:
            // Crie instâncias da sua classe TaskItem e organize-as em uma hierarquia.

            var projectA = new TaskItem("Projeto Alpha", "Em Andamento", new DateTime(2025, 12, 31));
            var task1A = new TaskItem("Planejamento", "Concluído", new DateTime(2025, 6, 15));
            var subtask1A = new TaskItem("Reunião Inicial", "Concluído", new DateTime(2025, 6, 5));
            var subtask2A = new TaskItem("Definir Escopo", "Concluído", new DateTime(2025, 6, 10));
            task1A.Children.Add(subtask1A);
            task1A.Children.Add(subtask2A);

            var task2A = new TaskItem("Desenvolvimento", "Em Andamento", new DateTime(2025, 9, 30));
            var subtask3A = new TaskItem("Codificar Módulo X", "Em Andamento", new DateTime(2025, 8, 15));
            var subtask4A = new TaskItem("Testar Módulo Y", "Pendente", new DateTime(2025, 9, 15));
            task2A.Children.Add(subtask3A);
            task2A.Children.Add(subtask4A);

            projectA.Children.Add(task1A);
            projectA.Children.Add(task2A);

            var projectB = new TaskItem("Projeto Beta", "Pendente", new DateTime(2026, 1, 31));
            var task1B = new TaskItem("Pesquisa de Mercado", "Pendente", new DateTime(2025, 7, 30));
            projectB.Children.Add(task1B);

            // 4. Atribua a Lista de Nós Raiz ao TreeListView:
            // Passe uma lista dos itens de nível superior (os "pais" de todos os outros).
            List<TaskItem> rootTasks = new List<TaskItem> { projectA, projectB };
            this.treeListViewTasks.Roots = rootTasks;

            // Opcional: Expanda todos os nós ao carregar
            this.treeListViewTasks.ExpandAll();
        }
    }
    // 1. Defina suas classes de dados
    public class TaskItem
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime DueDate { get; set; }

        // --- Nova propriedade para o Checkbox ---
        public bool IsCompleted { get; set; } // Representará o estado do checkbox

        public List<TaskItem> Children { get; set; } = new List<TaskItem>();

        public IEnumerable<TaskItem> GetChildren()
        {
            return Children;
        }

        // Construtor atualizado para incluir o novo parâmetro
        public TaskItem(string name, string status, DateTime dueDate, bool isCompleted = false)
        {
            Name = name;
            Status = status;
            DueDate = dueDate;
            IsCompleted = isCompleted; // Inicializa a nova propriedade
        }
    }

}
