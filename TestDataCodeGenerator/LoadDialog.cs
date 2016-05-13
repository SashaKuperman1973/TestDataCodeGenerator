using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDataCodeGenerator
{
    public partial class LoadDialog : Form
    {
        private string profileName = string.Empty;

        public Profile ProfileToLoad { get; set; }

        private ProfileCollection profileCollection;

        public LoadDialog()
        {
            InitializeComponent();
        }

        private void LoadDialog_Load(object sender, EventArgs e)
        {
            this.profileCollection = ProfileStorage.Deserialize() ?? new ProfileCollection();

            if (this.profileCollection.ProfileList == null)
            {
                this.profileCollection.ProfileList = new List<Profile>();
            }

            this.lbxProfileList.Items.AddRange(this.profileCollection.ProfileList.Select(list => list.ProfileName).ToArray());
        }

        private void lbxProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.profileName = this.lbxProfileList.Text;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.profileName.Trim() == string.Empty)
            {
                return;
            }

            if (MessageBox.Show(
                $"Wipe existing form values and load profile {this.profileName.Trim()}?",
                "Load Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }

            this.ProfileToLoad =
                this.profileCollection?.ProfileList.First(
                    profile => profile.ProfileName.Equals(this.profileName, StringComparison.InvariantCulture));
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CodeGenerator.Delete(this.profileName, this.profileCollection.ProfileList, this.lbxProfileList);
        }
    }
}
