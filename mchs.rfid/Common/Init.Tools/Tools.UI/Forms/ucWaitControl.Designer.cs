// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucWaitControl.Designer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Defines the ucWaitControl type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    partial class ucWaitControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// True if managed resources should be disposed; otherwise, false.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWaitControl));
            this.paPleaseWait = new System.Windows.Forms.Panel();
            this.pbPleaseWait = new System.Windows.Forms.PictureBox();
            this.laPleaseWait = new System.Windows.Forms.Label();
            this.heCancel = new DevExpress.XtraEditors.HyperLinkEdit();
            this.paPleaseWait.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPleaseWait)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heCancel.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // paPleaseWait
            // 
            this.paPleaseWait.BackColor = System.Drawing.Color.White;
            this.paPleaseWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paPleaseWait.Controls.Add(this.heCancel);
            this.paPleaseWait.Controls.Add(this.pbPleaseWait);
            this.paPleaseWait.Controls.Add(this.laPleaseWait);
            this.paPleaseWait.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.paPleaseWait.Location = new System.Drawing.Point(0, 0);
            this.paPleaseWait.Name = "paPleaseWait";
            this.paPleaseWait.Size = new System.Drawing.Size(255, 52);
            this.paPleaseWait.TabIndex = 5;
            // 
            // pbPleaseWait
            // 
            this.pbPleaseWait.Image = ((System.Drawing.Image)(resources.GetObject("pbPleaseWait.Image")));
            this.pbPleaseWait.Location = new System.Drawing.Point(5, 5);
            this.pbPleaseWait.Name = "pbPleaseWait";
            this.pbPleaseWait.Size = new System.Drawing.Size(40, 40);
            this.pbPleaseWait.TabIndex = 3;
            this.pbPleaseWait.TabStop = false;
            this.pbPleaseWait.WaitOnLoad = true;
            // 
            // laPleaseWait
            // 
            this.laPleaseWait.AutoSize = true;
            this.laPleaseWait.Font = new System.Drawing.Font("Arial", 12F);
            this.laPleaseWait.ForeColor = System.Drawing.Color.Gray;
            this.laPleaseWait.Location = new System.Drawing.Point(52, 17);
            this.laPleaseWait.Name = "laPleaseWait";
            this.laPleaseWait.Size = new System.Drawing.Size(192, 18);
            this.laPleaseWait.TabIndex = 2;
            this.laPleaseWait.Text = "Пожалуйста, подождите...";
            // 
            // heCancel
            // 
            this.heCancel.EditValue = "x";
            this.heCancel.Location = new System.Drawing.Point(240, 0);
            this.heCancel.Name = "heCancel";
            this.heCancel.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.heCancel.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.heCancel.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.heCancel.Properties.Appearance.Options.UseBackColor = true;
            this.heCancel.Properties.Appearance.Options.UseFont = true;
            this.heCancel.Properties.Appearance.Options.UseForeColor = true;
            this.heCancel.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.heCancel.Size = new System.Drawing.Size(11, 17);
            this.heCancel.TabIndex = 4;
            this.heCancel.Visible = false;
            this.heCancel.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.HeCancelOpenLink);
            // 
            // ucWaitControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.paPleaseWait);
            this.Name = "ucWaitControl";
            this.Size = new System.Drawing.Size(255, 52);
            this.paPleaseWait.ResumeLayout(false);
            this.paPleaseWait.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPleaseWait)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heCancel.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel paPleaseWait;
        private System.Windows.Forms.PictureBox pbPleaseWait;
        private System.Windows.Forms.Label laPleaseWait;
        private DevExpress.XtraEditors.HyperLinkEdit heCancel;
    }
}
