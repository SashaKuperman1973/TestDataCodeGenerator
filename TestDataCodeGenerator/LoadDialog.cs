/*
    Copyright 2016 Alexander Kuperman

    This file is part of TestDataCodeGenerator.

    TestDataCodeGenerator is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    TestDataCodeGenerator is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with TestDataFramework.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
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
            CodeGenerator.Delete(this.profileName, this.profileCollection, this.lbxProfileList);
        }
    }
}
