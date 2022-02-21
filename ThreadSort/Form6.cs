using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSort {
    public class Form6 : Form {
        public const int velikost = 15;
        public ProgressBar[] pBars = new ProgressBar[velikost];

        public int rowHeight = 10; int spaceBetweenRows = 10;
        public int rowWidth = 200;
        public int countPB = velikost;
        public int margin = 10;
        public int[] data;
        public Form6(int[] data) {
            int i;
            for (i = 0; i < countPB; i++) {
                pBars[i] = new ProgressBar();
                //this.SuspendLayout();
                // 
                // pb1
                // 
                this.data = data;
                this.pBars[i].Location = new System.Drawing.Point(margin, rowHeight + (i * (rowHeight + spaceBetweenRows)));
                this.pBars[i].Name = "pb" + i;
                this.pBars[i].Size = new System.Drawing.Size(rowWidth, rowHeight);
                this.pBars[i].TabIndex = 0 + i;
                this.Controls.Add(this.pBars[i]);
            }
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(rowWidth + 2 * margin, ((countPB+1) * (rowHeight + spaceBetweenRows))+margin);

            this.Name = "Form6";
            this.ResumeLayout(false);
            //showData();
        }
        public void showData(int index1, int index2) {
            int i = 0;
            for (i = 0; i < data.Length; i++) {
                this.pBars[i].Value = data[i];
                if (i == index1) {
                    pBars[i].ForeColor = Color.Red;
                } else if (i == index2) {
                    pBars[i].ForeColor = Color.Green;
                } else {
                    pBars[i].ForeColor = Color.Blue;
                }
                this.pBars[i].Refresh();
            }
        }

    }

}
