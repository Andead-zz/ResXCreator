using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ResXCreator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool EnableEditAndCreateCopy
        {
            set { editButton.Enabled = createCopyButton.Enabled = value; }
        }

        /// <summary>
        ///     Выбранный файл в дереве
        /// </summary>
        private FileInfo SelectedFile
        {
            get
            {
                return File.Exists((string) filesTreeView.SelectedNode.Tag) == false
                    ? null
                    : new FileInfo((string) filesTreeView.SelectedNode.Tag);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshFilesTree();
        }

        /// <summary>
        ///     Обновляет дерево файлов
        /// </summary>
        private void RefreshFilesTree()
        {
            // Очищаем дерево
            filesTreeView.Nodes.Clear();

            // Добавляем рабочий стол и мои документы
            var myDocsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filesTreeView.Nodes.Add(new TreeNode("Мои документы") {Tag = myDocsPath});
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filesTreeView.Nodes.Add(
                new TreeNode("Рабочий стол") {Tag = desktopPath});

            // Заполняем информацию о папках логических дисков
            foreach (var drive in Environment.GetLogicalDrives())
            {
                filesTreeView.Nodes.Add(new TreeNode(drive) {Tag = drive});
            }

            new Thread(() =>
            {
                foreach (var rootNode in filesTreeView.Nodes.OfType<TreeNode>())
                {
                    FetchSubdirectories(rootNode);
                }
            })
            {
                IsBackground = true,
                Priority = ThreadPriority.BelowNormal
            }.Start();
        }

        /// <summary>
        ///     Добавляет узел в дерево в потоке формы
        /// </summary>
        /// <param name="parentNode">Узел дерева, в который производится добавление</param>
        /// <param name="key">Имя узла</param>
        /// <param name="text">Текст</param>
        /// <param name="isCatalog">Признак каталога</param>
        /// <returns>Добавленный узел</returns>
        private static TreeNode AddNode(TreeNode parentNode, string key, string text, bool isCatalog = false)
        {
            if (parentNode.TreeView.InvokeRequired)
            {
                return (TreeNode) parentNode.TreeView.Invoke(new Func<TreeNode>(() => AddNode(parentNode, key, text)));
            }

            var node = new TreeNode(text)
            {
                Tag = key,
                ImageIndex = isCatalog ? 0 : 2,
                SelectedImageIndex = isCatalog ? 0 : 2
            };
            parentNode.Nodes.Add(node);

            return node;
        }

        private static void RemoveNode(TreeNode parentNode)
        {
            if (parentNode.TreeView.InvokeRequired)
            {
                parentNode.TreeView.Invoke(new Action(() => RemoveNode(parentNode)));
                return;
            }

            parentNode.Remove();
        }

        private static void FetchSubdirectories(TreeNode parentNode)
        {
            parentNode.Nodes.Clear();
            var directory = new DirectoryInfo((string) parentNode.Tag);
            try
            {
                foreach (var subDirectory in directory.GetDirectories())
                {
                    var subNode = AddNode(parentNode, subDirectory.FullName, subDirectory.Name, true);

                    try
                    {
                        foreach (var subSubDirectory in subDirectory.GetDirectories())
                        {
                            AddNode(subNode, subSubDirectory.FullName, subSubDirectory.Name);
                            break;
                        }
                    }
                    catch
                    {
                        RemoveNode(subNode);
                    }
                }

                foreach (var file in directory.GetFiles("*.resx"))
                {
                    AddNode(parentNode, file.FullName, file.Name);
                }
            }
            catch
            {
                RemoveNode(parentNode);
            }
        }

        private void filesTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            FetchSubdirectories(e.Node);

            e.Node.SelectedImageIndex = e.Node.ImageIndex = 1;
        }

        private void filesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (File.Exists((string) e.Node.Tag))
            {
                EnableEditAndCreateCopy = true;
                ReloadPreview();
                return;
            }

            EnableEditAndCreateCopy = false;
            e.Node.SelectedImageIndex = e.Node.IsExpanded ? 1 : 0;
        }

        private void ReloadPreview()
        {
            previewDataGridView.DataSource = Resources.ExtractTranslatableResources(SelectedFile.FullName);
        }

        private void filesTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode subNode in e.Node.Nodes)
            {
                subNode.Nodes.Clear();
            }

            e.Node.SelectedImageIndex = e.Node.ImageIndex = 0;
        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshFilesTree();
        }

        private void createCopyButton_Click(object sender, EventArgs e)
        {
            var selectedCulture = new LanguageSelect().ShowDialog(this);

            var directoryName = SelectedFile.DirectoryName;
            if (directoryName == null)
            {
                throw new FileNotFoundException("Не определена директория выбранного файла.");
            }

            var fileName = SelectedFile.Name.Substring(0, SelectedFile.Name.Length - SelectedFile.Extension.Length);

            var newShortName = fileName + "." + selectedCulture.TwoLetterISOLanguageName.ToLowerInvariant() + ".resx";

            var newFileName = Path.Combine(directoryName, newShortName);

            if (File.Exists(newFileName) &&
                MessageBox.Show("Копия с этим языком уже существует. Перезаписать её?", "Создать копию",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) ==
                DialogResult.Cancel)
            {
                return;
            }

            File.Copy(SelectedFile.FullName, newFileName, true);

            filesTreeView.SelectedNode.Parent.Nodes.Add(new TreeNode(newShortName) {Tag = newFileName});

            var editForm = new Edit {Filename = newFileName};
            editForm.DataSaved += (o, args) => ReloadPreview();
            editForm.Show();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            var editForm = new Edit {Filename = SelectedFile.FullName};
            editForm.DataSaved += (o, args) => ReloadPreview();
            editForm.Show();
        }
    }
}