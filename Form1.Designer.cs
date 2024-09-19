namespace Raycasting
{
    partial class form_Raycasting
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cnv_TopDown = new PictureBox();
            lbl_rad = new Label();
            lbl_grad = new Label();
            cnv_fp = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)cnv_TopDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cnv_fp).BeginInit();
            SuspendLayout();
            // 
            // cnv_TopDown
            // 
            cnv_TopDown.BackColor = SystemColors.ControlText;
            cnv_TopDown.Location = new Point(12, 12);
            cnv_TopDown.Name = "cnv_TopDown";
            cnv_TopDown.Size = new Size(525, 675);
            cnv_TopDown.TabIndex = 0;
            cnv_TopDown.TabStop = false;
            cnv_TopDown.Click += cnv_TopDown_Click;
            cnv_TopDown.Paint += cnv_TopDown_Paint;
            // 
            // lbl_rad
            // 
            lbl_rad.AutoSize = true;
            lbl_rad.Location = new Point(16, 15);
            lbl_rad.Name = "lbl_rad";
            lbl_rad.Size = new Size(38, 15);
            lbl_rad.TabIndex = 2;
            lbl_rad.Text = "label1";
            // 
            // lbl_grad
            // 
            lbl_grad.AutoSize = true;
            lbl_grad.Location = new Point(16, 41);
            lbl_grad.Name = "lbl_grad";
            lbl_grad.Size = new Size(38, 15);
            lbl_grad.TabIndex = 3;
            lbl_grad.Text = "label1";
            // 
            // cnv_fp
            // 
            cnv_fp.BackColor = SystemColors.ActiveCaptionText;
            cnv_fp.Location = new Point(553, 12);
            cnv_fp.Name = "cnv_fp";
            cnv_fp.Size = new Size(519, 675);
            cnv_fp.TabIndex = 4;
            cnv_fp.TabStop = false;
            cnv_fp.Click += cnv_fp_Click;
            cnv_fp.Paint += cnv_fp_Paint;
            // 
            // form_Raycasting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 699);
            Controls.Add(cnv_fp);
            Controls.Add(lbl_grad);
            Controls.Add(lbl_rad);
            Controls.Add(cnv_TopDown);
            Name = "form_Raycasting";
            Text = "Raycasting";
            KeyDown += form_Raycasting_KeyDown;
            KeyUp += form_Raycasting_KeyUp;
            ((System.ComponentModel.ISupportInitialize)cnv_TopDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)cnv_fp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox cnv_TopDown;
        private Label lbl_rad;
        private Label lbl_grad;
        private PictureBox cnv_fp;
    }
}
