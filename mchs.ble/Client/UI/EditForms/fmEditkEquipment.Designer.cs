namespace Client.UI.EditForms
{
    partial class fmEditkEquipment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmEditkEquipment));
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.teName = new DevExpress.XtraEditors.TextEdit();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ceIsUniq = new DevExpress.XtraEditors.CheckEdit();
            this.lueGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.teDescription = new DevExpress.XtraEditors.TextEdit();
            this.sbSave = new DevExpress.XtraEditors.SimpleButton();
            this.sbCancel = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ceIsUniq.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem3,
            this.layoutControlItem6});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(484, 142);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 95);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 27);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(10, 27);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(262, 27);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.teName;
            this.layoutControlItem1.CustomizationFormText = "Пароль";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(169, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Название";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(112, 13);
            // 
            // teName
            // 
            this.teName.Location = new System.Drawing.Point(127, 12);
            this.teName.Name = "teName";
            this.teName.Size = new System.Drawing.Size(345, 20);
            this.teName.StyleController = this.layoutControl1;
            this.teName.TabIndex = 4;
            this.teName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teName_KeyDown);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ceIsUniq);
            this.layoutControl1.Controls.Add(this.lueGroup);
            this.layoutControl1.Controls.Add(this.teDescription);
            this.layoutControl1.Controls.Add(this.sbSave);
            this.layoutControl1.Controls.Add(this.sbCancel);
            this.layoutControl1.Controls.Add(this.teName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(619, 165, 250, 350);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(484, 142);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ceIsUniq
            // 
            this.ceIsUniq.Location = new System.Drawing.Point(12, 84);
            this.ceIsUniq.Name = "ceIsUniq";
            this.ceIsUniq.Properties.Caption = "Уникальное оборудование";
            this.ceIsUniq.Size = new System.Drawing.Size(460, 19);
            this.ceIsUniq.StyleController = this.layoutControl1;
            this.ceIsUniq.TabIndex = 8;
            this.ceIsUniq.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ceIsUniq_KeyDown);
            // 
            // lueGroup
            // 
            this.lueGroup.EditValue = "<Null>";
            this.lueGroup.Location = new System.Drawing.Point(127, 60);
            this.lueGroup.Name = "lueGroup";
            this.lueGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lueGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Группа")});
            this.lueGroup.Properties.NullText = "";
            this.lueGroup.Properties.ShowFooter = false;
            this.lueGroup.Properties.ShowHeader = false;
            this.lueGroup.Properties.ShowLines = false;
            this.lueGroup.Size = new System.Drawing.Size(345, 20);
            this.lueGroup.StyleController = this.layoutControl1;
            this.lueGroup.TabIndex = 7;
            this.lueGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lueGroup_KeyDown);
            // 
            // teDescription
            // 
            this.teDescription.Location = new System.Drawing.Point(127, 36);
            this.teDescription.Name = "teDescription";
            this.teDescription.Size = new System.Drawing.Size(345, 20);
            this.teDescription.StyleController = this.layoutControl1;
            this.teDescription.TabIndex = 5;
            this.teDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.teDescription_KeyDown);
            // 
            // sbSave
            // 
            this.sbSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.sbSave.Image = global::Client.Properties.Resources.xSave;
            this.sbSave.Location = new System.Drawing.Point(274, 107);
            this.sbSave.Name = "sbSave";
            this.sbSave.Size = new System.Drawing.Size(97, 23);
            this.sbSave.StyleController = this.layoutControl1;
            this.sbSave.TabIndex = 6;
            this.sbSave.Text = "Сохранить";
            // 
            // sbCancel
            // 
            this.sbCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sbCancel.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.sbCancel.Image = global::Client.Properties.Resources.xCancel;
            this.sbCancel.Location = new System.Drawing.Point(375, 107);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new System.Drawing.Size(97, 23);
            this.sbCancel.StyleController = this.layoutControl1;
            this.sbCancel.TabIndex = 5;
            this.sbCancel.Text = "Отменить";
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.sbCancel;
            this.layoutControlItem2.ControlAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(363, 95);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(101, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.teDescription;
            this.layoutControlItem4.CustomizationFormText = "Описание";
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(169, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "Описание";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(112, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lueGroup;
            this.layoutControlItem5.CustomizationFormText = "Группа оборудования";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 48);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(169, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(464, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "Группа оборудования";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(112, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.sbSave;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(262, 95);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(101, 27);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.ceIsUniq;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 72);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(464, 23);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // fmEditkEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 142);
            this.Controls.Add(this.layoutControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 180);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 180);
            this.Name = "fmEditkEquipment";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Редактирование типа оборудования";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ceIsUniq.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lueGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.teDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit teName;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit lueGroup;
        private DevExpress.XtraEditors.TextEdit teDescription;
        private DevExpress.XtraEditors.SimpleButton sbCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton sbSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.CheckEdit ceIsUniq;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;

    }
}