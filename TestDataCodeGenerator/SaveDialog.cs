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
    public partial class SaveDialog : Form
    {
        private ProfileCollection profileCollection;

        public SaveDialog()
        {
            this.InitializeComponent();
        }

        private void SaveDialog_Load(object sender, EventArgs e)
        {
            this.profileCollection = ProfileStorage.Deserialize() ?? new ProfileCollection();

            if (this.profileCollection.ProfileList == null)
            {
                this.profileCollection.ProfileList = new List<Profile>();
            }

            this.lbxProfileList.Sorted = true;
            this.lbxProfileList.Items.AddRange(this.profileCollection.ProfileList.Select(list => list.ProfileName).ToArray());

            this.txtProfileName.Text = ((CodeGenerator)this.Owner).ProfileName;
        }

        private void lbxProfileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtProfileName.Text = this.lbxProfileList.Text;
        }

        private void txtSave_Click(object sender, EventArgs e)
        {
            if (this.txtProfileName.Text.Trim() == string.Empty)
            {
                return;
            }

            for (int i = 0; i < this.lbxProfileList.Items.Count; i++)
            {
                if (
                    !this.txtProfileName.Text.Trim()
                        .Equals((string) this.lbxProfileList.Items[i], StringComparison.InvariantCultureIgnoreCase))
                    continue;

                if (MessageBox.Show(
                    $"Profile {(string)this.lbxProfileList.Items[i]} will be overwritten. Continue?",
                    "Save Profile", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return;
                }

                break;
            }

            Profile profileToWrite =
                this.profileCollection.ProfileList.FirstOrDefault(
                    profile =>
                        profile.ProfileName.Equals(this.txtProfileName.Text.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (profileToWrite == null)
            {
                profileToWrite = new Profile();
                this.profileCollection.ProfileList.Add(profileToWrite);
            }

            var mainForm = (CodeGenerator) this.Owner;

            profileToWrite.ProfileName = this.txtProfileName.Text.Trim();
            profileToWrite.ConnectionString = mainForm.ConnectionString;
            profileToWrite.DefaultNameSpace = mainForm.DefaultNameSpace;
            profileToWrite.GeneratedClassName = mainForm.GeneratedClassName;
            profileToWrite.GenerationOption = mainForm.GenerationOption;
            profileToWrite.OutputFolder = mainForm.OutputFolder;
            profileToWrite.TableList = mainForm.TableList;

            ProfileStorage.Serialize(this.profileCollection);

            ((CodeGenerator) this.Owner).ProfileName = profileToWrite.ProfileName;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            CodeGenerator.Delete(this.txtProfileName.Text, this.profileCollection.ProfileList, this.lbxProfileList);
        }
    }
}
