namespace Assignment2Question1
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.patientName_editbox = new System.Windows.Forms.TextBox();
            this.patient_DOB = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.email_editbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.genderMale = new System.Windows.Forms.RadioButton();
            this.genderFemale = new System.Windows.Forms.RadioButton();
            this.submit = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.bpm_text = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider4 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Date Of Birth";
            // 
            // patientName_editbox
            // 
            this.patientName_editbox.Location = new System.Drawing.Point(212, 61);
            this.patientName_editbox.Name = "patientName_editbox";
            this.patientName_editbox.Size = new System.Drawing.Size(241, 20);
            this.patientName_editbox.TabIndex = 2;
            // 
            // patient_DOB
            // 
            this.patient_DOB.Location = new System.Drawing.Point(212, 157);
            this.patient_DOB.Name = "patient_DOB";
            this.patient_DOB.Size = new System.Drawing.Size(241, 20);
            this.patient_DOB.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(37, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email";
            // 
            // email_editbox
            // 
            this.email_editbox.Location = new System.Drawing.Point(212, 110);
            this.email_editbox.Name = "email_editbox";
            this.email_editbox.Size = new System.Drawing.Size(241, 20);
            this.email_editbox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(37, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Gender";
            // 
            // genderMale
            // 
            this.genderMale.AutoSize = true;
            this.genderMale.BackColor = System.Drawing.Color.Transparent;
            this.genderMale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderMale.Location = new System.Drawing.Point(212, 210);
            this.genderMale.Name = "genderMale";
            this.genderMale.Size = new System.Drawing.Size(56, 20);
            this.genderMale.TabIndex = 8;
            this.genderMale.TabStop = true;
            this.genderMale.Text = "Male";
            this.genderMale.UseVisualStyleBackColor = false;
            // 
            // genderFemale
            // 
            this.genderFemale.AutoSize = true;
            this.genderFemale.BackColor = System.Drawing.Color.Transparent;
            this.genderFemale.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderFemale.Location = new System.Drawing.Point(351, 208);
            this.genderFemale.Name = "genderFemale";
            this.genderFemale.Size = new System.Drawing.Size(72, 20);
            this.genderFemale.TabIndex = 9;
            this.genderFemale.TabStop = true;
            this.genderFemale.Text = "Female";
            this.genderFemale.UseVisualStyleBackColor = false;
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(212, 329);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(110, 28);
            this.submit.TabIndex = 10;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.button1_Click);
            // 
            // reset
            // 
            this.reset.Location = new System.Drawing.Point(360, 329);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(93, 28);
            this.reset.TabIndex = 11;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // bpm_text
            // 
            this.bpm_text.Location = new System.Drawing.Point(212, 261);
            this.bpm_text.Name = "bpm_text";
            this.bpm_text.Size = new System.Drawing.Size(241, 20);
            this.bpm_text.TabIndex = 12;
            this.bpm_text.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 13;
            this.label5.Text = "BPM";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // errorProvider4
            // 
            this.errorProvider4.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Assignment2Question1.Properties.Resources.background2;
            this.ClientSize = new System.Drawing.Size(739, 450);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.bpm_text);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.genderFemale);
            this.Controls.Add(this.genderMale);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.email_editbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.patient_DOB);
            this.Controls.Add(this.patientName_editbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox patientName_editbox;
        private System.Windows.Forms.DateTimePicker patient_DOB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox email_editbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton genderMale;
        private System.Windows.Forms.RadioButton genderFemale;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.TextBox bpm_text;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
        private System.Windows.Forms.ErrorProvider errorProvider4;
    }
}

