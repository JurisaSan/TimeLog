using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeLog
{
   
    internal class ManagingControls
    {
        private Button addButton;
        private RadioButton standBy;
        private Label standByLabel;
        private Label timeCounterLabel;
        private Label counterLabelText;
        public ManagingControls() { }
        public Button ConfigureAddButton()
        {
            // Create and configure the addButton
            addButton = new Button();
            addButton.Text = "+";
            addButton.Size = new Size(25, 25);
            addButton.Font = new Font(addButton.Font, FontStyle.Bold);
            addButton.Font = new Font(addButton.Font.FontFamily, 8);
            addButton.Location = new Point(10, 10);
            addButton.TextAlign = ContentAlignment.MiddleCenter; // Center the text in the button
            addButton.BackColor = Color.Green;
            addButton.ForeColor = Color.White;
            return addButton;
          
        }

        public Label CounterLabelText()
        {
            // Create and configure the standByLabel
            counterLabelText = new Label();
            counterLabelText.Text = "Current task is active: ";
            counterLabelText.TextAlign = ContentAlignment.MiddleCenter;
            counterLabelText.Font = new Font(counterLabelText.Font.FontFamily, 8);
            counterLabelText.Size = new System.Drawing.Size(200, 20);
            counterLabelText.Location = new Point(90, 15);
            return counterLabelText;
        }

        public Label TimeCounterLabel()
        {
            // Create and configure the standByLabel
            timeCounterLabel = new Label();
            timeCounterLabel.Text = "00:00:00";
            timeCounterLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeCounterLabel.Font = new Font("Times New Roman", 8,FontStyle.Bold);
            timeCounterLabel.ForeColor = Color.Green;
            timeCounterLabel.BorderStyle = BorderStyle.FixedSingle;
            timeCounterLabel.BackColor = Color.LightYellow;
            timeCounterLabel.Size = new System.Drawing.Size(62, 20);
            timeCounterLabel.Location = new Point(290, 15);
            return timeCounterLabel;
        }

        public RadioButton ConfigureStandByButton()
        {
            // Create and configure the standBy
            standBy = new RadioButton();
            standBy.Text = "";
            standBy.Size = new System.Drawing.Size(14, 14);
            standBy.Location = new System.Drawing.Point(570, 17);
            standBy.TabStop = true;
            standBy.UseVisualStyleBackColor = false; // Set the radioButton1 to be a radio button
            standBy.AutoCheck = true;
            standBy.Appearance = Appearance.Button;
            standBy.FlatStyle = FlatStyle.Popup;
            standBy.BackColor = System.Drawing.Color.Silver;
            
            return standBy;
            
        }

        public Label ConfigureStandByLabel()
        {
            // Create and configure the standByLabel
            standByLabel = new Label();
            standByLabel.Text = "StandBy";
            standByLabel.TextAlign = ContentAlignment.MiddleCenter;
            standByLabel.Font = new Font(standByLabel.Font.FontFamily, 8);
            standByLabel.Size = new System.Drawing.Size(77, 20);
            standByLabel.Location = new Point(590, 15);
            return standByLabel;
        }
    }
}
