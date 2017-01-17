// --------------------------------------------------------------------------------------------------------------------
// <copyright file="fmLoading.Designer.cs" company="ИНИТ-центр">
//   ИНИТ-центр, 2013г.
// </copyright>
// <summary>
//   Форма загрузки
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Init.Tools.UI.Forms
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1400:AccessModifierMustBeDeclared", Justification = "Reviewed. Suppression is OK here.")]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
       
    partial class fmLoading
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmLoading));
            this.laPleaseWait = new System.Windows.Forms.Label();
            this.pbPleaseWait = new System.Windows.Forms.PictureBox();
            this.paPleaseWait = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPleaseWait)).BeginInit();
            this.paPleaseWait.SuspendLayout();
            this.SuspendLayout();
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
            // paPleaseWait
            // 
            this.paPleaseWait.BackColor = System.Drawing.Color.White;
            this.paPleaseWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paPleaseWait.Controls.Add(this.pbPleaseWait);
            this.paPleaseWait.Controls.Add(this.laPleaseWait);
            this.paPleaseWait.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.paPleaseWait.Location = new System.Drawing.Point(0, 0);
            this.paPleaseWait.Name = "paPleaseWait";
            this.paPleaseWait.Size = new System.Drawing.Size(255, 52);
            this.paPleaseWait.TabIndex = 4;
            // 
            // fmLoading
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolTip;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(255, 52);
            this.ControlBox = false;
            this.Controls.Add(this.paPleaseWait);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fmLoading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            ((System.ComponentModel.ISupportInitialize)(this.pbPleaseWait)).EndInit();
            this.paPleaseWait.ResumeLayout(false);
            this.paPleaseWait.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laPleaseWait;
        private System.Windows.Forms.PictureBox pbPleaseWait;
        private System.Windows.Forms.Panel paPleaseWait;
    }
}