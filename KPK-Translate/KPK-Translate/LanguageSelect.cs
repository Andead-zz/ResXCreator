using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace KPK_Translate
{
    public partial class LanguageSelect : Form
    {
        public LanguageSelect()
        {
            InitializeComponent();
        }

        private void LanguageSelect_Load(object sender, EventArgs e)
        {
            languagesComboBox.DisplayMember = "DisplayName";
            languagesComboBox.ValueMember = "TwoLetterISOLanguageName";
            languagesComboBox.Items.Add(new CultureInfo("en"));
            languagesComboBox.Items.Add(new CultureInfo("es"));

            foreach (var cultureInfo in 
                CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).Where(
                    c =>
                        c.TwoLetterISOLanguageName.ToLowerInvariant() != "en" &&
                        c.TwoLetterISOLanguageName.ToLowerInvariant() != "es"))
            {
                languagesComboBox.Items.Add(cultureInfo);
            }

            languagesComboBox.SelectedIndex = 0;
        }

        internal new CultureInfo ShowDialog(IWin32Window owner)
        {
            base.ShowDialog(owner);
            return (CultureInfo) languagesComboBox.SelectedItem;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}