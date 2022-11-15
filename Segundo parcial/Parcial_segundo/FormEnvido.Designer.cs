
namespace Parcial_segundo
{
    partial class FormEnvido
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonQuiero = new System.Windows.Forms.Button();
            this.buttonNoQuiero = new System.Windows.Forms.Button();
            this.buttonFaltaEnvido = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(42, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "Un jugador ha cantado envido";
            // 
            // buttonQuiero
            // 
            this.buttonQuiero.Location = new System.Drawing.Point(82, 90);
            this.buttonQuiero.Name = "buttonQuiero";
            this.buttonQuiero.Size = new System.Drawing.Size(93, 34);
            this.buttonQuiero.TabIndex = 5;
            this.buttonQuiero.Text = "Quiero";
            this.buttonQuiero.UseVisualStyleBackColor = true;
            this.buttonQuiero.Click += new System.EventHandler(this.buttonQuiero_Click);
            // 
            // buttonNoQuiero
            // 
            this.buttonNoQuiero.Location = new System.Drawing.Point(82, 215);
            this.buttonNoQuiero.Name = "buttonNoQuiero";
            this.buttonNoQuiero.Size = new System.Drawing.Size(93, 34);
            this.buttonNoQuiero.TabIndex = 6;
            this.buttonNoQuiero.Text = "No quiero";
            this.buttonNoQuiero.UseVisualStyleBackColor = true;
            this.buttonNoQuiero.Click += new System.EventHandler(this.buttonNoQuiero_Click);
            // 
            // buttonFaltaEnvido
            // 
            this.buttonFaltaEnvido.Location = new System.Drawing.Point(82, 152);
            this.buttonFaltaEnvido.Name = "buttonFaltaEnvido";
            this.buttonFaltaEnvido.Size = new System.Drawing.Size(93, 34);
            this.buttonFaltaEnvido.TabIndex = 7;
            this.buttonFaltaEnvido.Text = "Falta envido";
            this.buttonFaltaEnvido.UseVisualStyleBackColor = true;
            this.buttonFaltaEnvido.Click += new System.EventHandler(this.buttonFaltaEnvido_Click);
            // 
            // FormEnvido
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(282, 296);
            this.Controls.Add(this.buttonFaltaEnvido);
            this.Controls.Add(this.buttonNoQuiero);
            this.Controls.Add(this.buttonQuiero);
            this.Controls.Add(this.label1);
            this.Name = "FormEnvido";
            this.Text = "FormEnvido";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEnvido_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonQuiero;
        private System.Windows.Forms.Button buttonNoQuiero;
        private System.Windows.Forms.Button buttonFaltaEnvido;
    }
}