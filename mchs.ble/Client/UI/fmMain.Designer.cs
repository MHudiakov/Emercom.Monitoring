namespace Client.UI
{
    partial class fmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fmMain));
            this.splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.layoutControl = new DevExpress.XtraLayout.LayoutControl();
            this.navBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.nbgMonitoring = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiRealTime = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTripRealTime = new DevExpress.XtraNavBar.NavBarItem();
            this.nvgUnits = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiUnits = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTripHistory = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgEquipmentsMovement = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiEquipments = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiMovementLog = new DevExpress.XtraNavBar.NavBarItem();
            this.nbgCatalogs = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiCatalogkEquipment = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiCatalogkObject = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiCatalogkGroup = new DevExpress.XtraNavBar.NavBarItem();
            this.navbarImageListLarge = new System.Windows.Forms.ImageList(this.components);
            this.navbarImageList = new System.Windows.Forms.ImageList(this.components);
            this.layoutControlGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciNavBarControl = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).BeginInit();
            this.splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).BeginInit();
            this.layoutControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNavBarControl)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl
            // 
            this.splitContainerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerControl.Location = new System.Drawing.Point(3, 3);
            this.splitContainerControl.Name = "splitContainerControl";
            this.splitContainerControl.Panel1.Controls.Add(this.layoutControl);
            this.splitContainerControl.Panel1.Text = "Panel1";
            this.splitContainerControl.Panel2.Text = "Panel2";
            this.splitContainerControl.Size = new System.Drawing.Size(1328, 758);
            this.splitContainerControl.SplitterPosition = 256;
            this.splitContainerControl.TabIndex = 0;
            this.splitContainerControl.Text = "splitContainerControl1";
            this.splitContainerControl.DoubleClick += new System.EventHandler(this.splitContainerControl_DoubleClick);
            // 
            // layoutControl
            // 
            this.layoutControl.Controls.Add(this.navBarControl);
            this.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl.Location = new System.Drawing.Point(0, 0);
            this.layoutControl.Name = "layoutControl";
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.BackColor = System.Drawing.Color.LightGray;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseBackColor = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseFont = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.Options.UseTextOptions = true;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControl.OptionsPrint.AppearanceGroupCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.layoutControl.OptionsPrint.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.layoutControl.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl.Root = this.layoutControlGroup;
            this.layoutControl.Size = new System.Drawing.Size(256, 758);
            this.layoutControl.TabIndex = 0;
            this.layoutControl.Text = "layoutControl1";
            // 
            // navBarControl
            // 
            this.navBarControl.ActiveGroup = this.nbgMonitoring;
            this.navBarControl.Appearance.Item.Options.UseTextOptions = true;
            this.navBarControl.Appearance.Item.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.navBarControl.Appearance.ItemActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.navBarControl.Appearance.ItemActive.Options.UseBackColor = true;
            this.navBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbgMonitoring,
            this.nvgUnits,
            this.nbgEquipmentsMovement,
            this.nbgCatalogs});
            this.navBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiUnits,
            this.nbiEquipments,
            this.nbiCatalogkGroup,
            this.nbiCatalogkEquipment,
            this.nbiCatalogkObject,
            this.nbiRealTime,
            this.nbiMovementLog,
            this.nbiTripHistory,
            this.nbiTripRealTime});
            this.navBarControl.LargeImages = this.navbarImageListLarge;
            this.navBarControl.Location = new System.Drawing.Point(12, 12);
            this.navBarControl.Name = "navBarControl";
            this.navBarControl.OptionsNavPane.ExpandedWidth = 232;
            this.navBarControl.Size = new System.Drawing.Size(232, 734);
            this.navBarControl.SmallImages = this.navbarImageList;
            this.navBarControl.TabIndex = 4;
            this.navBarControl.Text = "navBarControl";
            // 
            // nbgMonitoring
            // 
            this.nbgMonitoring.Caption = "Мониторинг";
            this.nbgMonitoring.Expanded = true;
            this.nbgMonitoring.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiRealTime),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTripRealTime)});
            this.nbgMonitoring.LargeImageIndex = 8;
            this.nbgMonitoring.Name = "nbgMonitoring";
            // 
            // nbiRealTime
            // 
            this.nbiRealTime.Caption = "Реальное время";
            this.nbiRealTime.Name = "nbiRealTime";
            this.nbiRealTime.SmallImageIndex = 35;
            this.nbiRealTime.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbiTripRealTime
            // 
            this.nbiTripRealTime.Caption = "Мониторинг машин";
            this.nbiTripRealTime.Name = "nbiTripRealTime";
            this.nbiTripRealTime.SmallImageIndex = 49;
            this.nbiTripRealTime.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nvgUnits
            // 
            this.nvgUnits.Caption = "Машины и вызовы";
            this.nvgUnits.Expanded = true;
            this.nvgUnits.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUnits),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTripHistory)});
            this.nvgUnits.LargeImageIndex = 10;
            this.nvgUnits.Name = "nvgUnits";
            // 
            // nbiUnits
            // 
            this.nbiUnits.Caption = "Укомплектованность машин";
            this.nbiUnits.Name = "nbiUnits";
            this.nbiUnits.SmallImageIndex = 46;
            this.nbiUnits.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbiTripHistory
            // 
            this.nbiTripHistory.Caption = "История вызовов";
            this.nbiTripHistory.Name = "nbiTripHistory";
            this.nbiTripHistory.SmallImageIndex = 40;
            this.nbiTripHistory.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbgEquipmentsMovement
            // 
            this.nbgEquipmentsMovement.Caption = "Оборудование и движение";
            this.nbgEquipmentsMovement.Expanded = true;
            this.nbgEquipmentsMovement.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiEquipments),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiMovementLog)});
            this.nbgEquipmentsMovement.LargeImageIndex = 11;
            this.nbgEquipmentsMovement.Name = "nbgEquipmentsMovement";
            // 
            // nbiEquipments
            // 
            this.nbiEquipments.Caption = "Учёт оборудования";
            this.nbiEquipments.Name = "nbiEquipments";
            this.nbiEquipments.SmallImageIndex = 50;
            this.nbiEquipments.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbiMovementLog
            // 
            this.nbiMovementLog.Caption = "Журнал движения";
            this.nbiMovementLog.Name = "nbiMovementLog";
            this.nbiMovementLog.SmallImageIndex = 37;
            this.nbiMovementLog.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbgCatalogs
            // 
            this.nbgCatalogs.Caption = "Справочники";
            this.nbgCatalogs.Expanded = true;
            this.nbgCatalogs.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCatalogkEquipment),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiCatalogkObject)});
            this.nbgCatalogs.LargeImageIndex = 12;
            this.nbgCatalogs.Name = "nbgCatalogs";
            // 
            // nbiCatalogkEquipment
            // 
            this.nbiCatalogkEquipment.Caption = "Справочник оборудования";
            this.nbiCatalogkEquipment.Name = "nbiCatalogkEquipment";
            this.nbiCatalogkEquipment.SmallImageIndex = 43;
            this.nbiCatalogkEquipment.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbiCatalogkObject
            // 
            this.nbiCatalogkObject.Caption = "Справочник типов объектов";
            this.nbiCatalogkObject.Name = "nbiCatalogkObject";
            this.nbiCatalogkObject.SmallImageIndex = 44;
            this.nbiCatalogkObject.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // nbiCatalogkGroup
            // 
            this.nbiCatalogkGroup.Caption = "Справочник групп оборудования";
            this.nbiCatalogkGroup.Name = "nbiCatalogkGroup";
            this.nbiCatalogkGroup.SmallImageIndex = 42;
            this.nbiCatalogkGroup.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.NavBarItem_LinkClicked);
            // 
            // navbarImageListLarge
            // 
            this.navbarImageListLarge.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("navbarImageListLarge.ImageStream")));
            this.navbarImageListLarge.TransparentColor = System.Drawing.Color.Transparent;
            this.navbarImageListLarge.Images.SetKeyName(0, "Mail_16x16.png");
            this.navbarImageListLarge.Images.SetKeyName(1, "Organizer_16x16.png");
            this.navbarImageListLarge.Images.SetKeyName(2, "spreadsheet.png");
            this.navbarImageListLarge.Images.SetKeyName(3, "мониторинг 4.png");
            this.navbarImageListLarge.Images.SetKeyName(4, "contents.png");
            this.navbarImageListLarge.Images.SetKeyName(5, "Log_small.png");
            this.navbarImageListLarge.Images.SetKeyName(6, "Plan32.png");
            this.navbarImageListLarge.Images.SetKeyName(7, "Shedule32.png");
            this.navbarImageListLarge.Images.SetKeyName(8, "1.png");
            this.navbarImageListLarge.Images.SetKeyName(9, "2.png");
            this.navbarImageListLarge.Images.SetKeyName(10, "3.png");
            this.navbarImageListLarge.Images.SetKeyName(11, "4.png");
            this.navbarImageListLarge.Images.SetKeyName(12, "5.png");
            this.navbarImageListLarge.Images.SetKeyName(13, "6.png");
            // 
            // navbarImageList
            // 
            this.navbarImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("navbarImageList.ImageStream")));
            this.navbarImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.navbarImageList.Images.SetKeyName(0, "Inbox_16x16.png");
            this.navbarImageList.Images.SetKeyName(1, "Outbox_16x16.png");
            this.navbarImageList.Images.SetKeyName(2, "Drafts_16x16.png");
            this.navbarImageList.Images.SetKeyName(3, "Trash_16x16.png");
            this.navbarImageList.Images.SetKeyName(4, "Calendar_16x16.png");
            this.navbarImageList.Images.SetKeyName(5, "Tasks_16x16.png");
            this.navbarImageList.Images.SetKeyName(6, "015.png");
            this.navbarImageList.Images.SetKeyName(7, "016.png");
            this.navbarImageList.Images.SetKeyName(8, "018.png");
            this.navbarImageList.Images.SetKeyName(9, "FilterScript_small.png");
            this.navbarImageList.Images.SetKeyName(10, "PersAnalize.png");
            this.navbarImageList.Images.SetKeyName(11, "MachineAnalize.png");
            this.navbarImageList.Images.SetKeyName(12, "spreadsheet_document.png");
            this.navbarImageList.Images.SetKeyName(13, "MonitorOnline.png");
            this.navbarImageList.Images.SetKeyName(14, "HistoryLog.png");
            this.navbarImageList.Images.SetKeyName(15, "MonitorWarning.png");
            this.navbarImageList.Images.SetKeyName(16, "HumanResource.png");
            this.navbarImageList.Images.SetKeyName(17, "StankiTypes_smal.png");
            this.navbarImageList.Images.SetKeyName(18, "NldTypes_small.png");
            this.navbarImageList.Images.SetKeyName(19, "DetailTypes_small.png");
            this.navbarImageList.Images.SetKeyName(20, "ToolTypes_small.png");
            this.navbarImageList.Images.SetKeyName(21, "TechOperation_small.png");
            this.navbarImageList.Images.SetKeyName(22, "PurposeTypes_small.png");
            this.navbarImageList.Images.SetKeyName(23, "DetailMakeLog_small.png");
            this.navbarImageList.Images.SetKeyName(24, "TimeLostLog_small.png");
            this.navbarImageList.Images.SetKeyName(25, "RemontLog_small.png");
            this.navbarImageList.Images.SetKeyName(26, "InstrumentDownLog_small.png");
            this.navbarImageList.Images.SetKeyName(27, "RoleClassifier_small.png");
            this.navbarImageList.Images.SetKeyName(28, "DevicesTypes_small.png");
            this.navbarImageList.Images.SetKeyName(29, "Products_16.png");
            this.navbarImageList.Images.SetKeyName(30, "OperPrograms_16.png");
            this.navbarImageList.Images.SetKeyName(31, "MastersPlan16.png");
            this.navbarImageList.Images.SetKeyName(32, "Fact16.png");
            this.navbarImageList.Images.SetKeyName(33, "WorkShedule16.png");
            this.navbarImageList.Images.SetKeyName(34, "CurrentRemontLog16.png");
            this.navbarImageList.Images.SetKeyName(35, "1_1.png");
            this.navbarImageList.Images.SetKeyName(36, "1_2.png");
            this.navbarImageList.Images.SetKeyName(37, "1_3.png");
            this.navbarImageList.Images.SetKeyName(38, "1_4.png");
            this.navbarImageList.Images.SetKeyName(39, "2_1.png");
            this.navbarImageList.Images.SetKeyName(40, "2_2.png");
            this.navbarImageList.Images.SetKeyName(41, "2_3.png");
            this.navbarImageList.Images.SetKeyName(42, "2_4.png");
            this.navbarImageList.Images.SetKeyName(43, "3_1.png");
            this.navbarImageList.Images.SetKeyName(44, "3_2.png");
            this.navbarImageList.Images.SetKeyName(45, "4_1.png");
            this.navbarImageList.Images.SetKeyName(46, "4_2.png");
            this.navbarImageList.Images.SetKeyName(47, "5_1.png");
            this.navbarImageList.Images.SetKeyName(48, "6_1.png");
            this.navbarImageList.Images.SetKeyName(49, "lupa25.png");
            this.navbarImageList.Images.SetKeyName(50, "Diagnostic_sm.png");
            this.navbarImageList.Images.SetKeyName(51, "tv.png");
            // 
            // layoutControlGroup
            // 
            this.layoutControlGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup.GroupBordersVisible = false;
            this.layoutControlGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciNavBarControl});
            this.layoutControlGroup.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup.Name = "layoutControlGroup";
            this.layoutControlGroup.Size = new System.Drawing.Size(256, 758);
            this.layoutControlGroup.TextVisible = false;
            // 
            // lciNavBarControl
            // 
            this.lciNavBarControl.Control = this.navBarControl;
            this.lciNavBarControl.Location = new System.Drawing.Point(0, 0);
            this.lciNavBarControl.Name = "lciNavBarControl";
            this.lciNavBarControl.Size = new System.Drawing.Size(236, 738);
            this.lciNavBarControl.TextSize = new System.Drawing.Size(0, 0);
            this.lciNavBarControl.TextVisible = false;
            // 
            // fmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1334, 762);
            this.Controls.Add(this.splitContainerControl);
            this.Name = "fmMain";
            this.Text = "fmMain";
            this.Load += new System.EventHandler(this.fmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl)).EndInit();
            this.splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl)).EndInit();
            this.layoutControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNavBarControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl;
        private DevExpress.XtraLayout.LayoutControl layoutControl;
        private DevExpress.XtraNavBar.NavBarControl navBarControl;
        private DevExpress.XtraNavBar.NavBarGroup nbgEquipmentsMovement;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup;
        private DevExpress.XtraLayout.LayoutControlItem lciNavBarControl;
        private DevExpress.XtraNavBar.NavBarItem nbiUnits;
        private DevExpress.XtraNavBar.NavBarItem nbiEquipments;
        private DevExpress.XtraNavBar.NavBarGroup nbgCatalogs;
        private DevExpress.XtraNavBar.NavBarItem nbiCatalogkGroup;
        private DevExpress.XtraNavBar.NavBarItem nbiCatalogkEquipment;
        private DevExpress.XtraNavBar.NavBarItem nbiCatalogkObject;
        private System.Windows.Forms.ImageList navbarImageList;
        private System.Windows.Forms.ImageList navbarImageListLarge;
        private DevExpress.XtraNavBar.NavBarGroup nbgMonitoring;
        private DevExpress.XtraNavBar.NavBarItem nbiRealTime;
        private DevExpress.XtraNavBar.NavBarItem nbiMovementLog;
        private DevExpress.XtraNavBar.NavBarGroup nvgUnits;
        private DevExpress.XtraNavBar.NavBarItem nbiTripHistory;
        private DevExpress.XtraNavBar.NavBarItem nbiTripRealTime;
    }
}