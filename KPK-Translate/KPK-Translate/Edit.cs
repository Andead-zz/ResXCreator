using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ResXCreator
{
    /// <summary>
    ///     Форма редактирования *resx-файла
    /// </summary>
    internal partial class Edit : Form
    {
        private string _filename;

        public Edit()
        {
            InitializeComponent();
        }

        public string Filename
        {
            get { return _filename; }
            set
            {
                _filename = value;

                if (!File.Exists(value))
                {
                    throw new ArgumentException("Файл не существует");
                }

                var fileInfo = new FileInfo(value);
                Text = string.Format("{0} - редактирование", fileInfo.Name);

                Reload();
            }
        }

        private void Reload()
        {
            editDataGridView.DataSource = Resources.ExtractTranslatableResources(Filename);
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            Resources.TranslateResources((List<StringResource>) editDataGridView.DataSource, Filename);
            OnDataSaved();
            MessageBox.Show(@"Изменения сохранены!", Application.ProductName, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        internal event EventHandler DataSaved;

        protected virtual void OnDataSaved()
        {
            var handler = DataSaved;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}