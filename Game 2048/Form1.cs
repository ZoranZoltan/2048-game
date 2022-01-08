using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Media;
namespace game2048
{
    public partial class Game2048 : Form
    {
       
        SoundPlayer sound = new SoundPlayer(Application.StartupPath+"/andiem.wav");//Pobednička melodija
        SoundPlayer sound2 = new SoundPlayer(Application.StartupPath+"/blip.wav");//Zvuk igrice
        Random Rd = new Random();
        bool iopet = true;
        static ArrayList drugi = new ArrayList();//Lista1
        public Game2048()
        {
            //donji i gornji pokret i levo i desno pomiču suprotno, tj. jedan povećava niz, a drugi ga smanjuje.
            // zajedničke tačke unutar teksta za prikupljanje brojeva i dodavanje postojećeg rezultata.
            //

            InitializeComponent();
            
        }
      
        public void color()
        {// Odredjivanje boje pločica i praznih polja
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    if(Game[i,j].Text==""){
                        Game[i, j].BackColor = System.Drawing.Color.DarkMagenta;
                    }
                    if (Game[i, j].Text == "2")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightGray;
                        Game[i, j].ForeColor = System.Drawing.Color.White;

                    }
                    if (Game[i, j].Text == "4")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Gray;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "8")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Orange;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "16")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.OrangeRed;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "32")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.DarkOrange;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "64")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightPink;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "128")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Red;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "256")
                    {
                        Game[i, j].BackColor = Color.DarkRed;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "512")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.LightBlue;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "1024")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Blue;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                    if (Game[i, j].Text == "2048")
                    {
                        Game[i, j].BackColor = System.Drawing.Color.Green;
                        Game[i, j].ForeColor = System.Drawing.Color.White;
                    }
                   
                }
            }
            
        } 
        
        public void colprocesses() {
            drugi.Clear();

            Label[,] Game = { //Labele
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4;i++ )
            {
                for (int j = 0; j < 4;j++)
                {
                    if(Game[i,j].Text==""){
                        drugi.Add(i*4+j+1);//16 ugnježdjenih i 2 dodate u polje.
                    }
                }
            }
            
            if(drugi.Count>0){
               
                int gospodar = int.Parse(drugi[Rd.Next(0,drugi.Count-1)].ToString());
                int i0 = (gospodar - 1) / 4;
                int j0 = (gospodar - 1) - i0 *4;
                int seria2 = Rd.Next(1,10);//seria2 (Za nasumično dodavanje)
                if (seria2 == 1 || seria2 == 2 || seria2 == 3 || seria2 == 4 || seria2 == 5||seria2==6 )
                {
                    Game[i0, j0].Text = "2";
                }
                else { 
                    Game[i0,j0].Text="4";
                }

            }
            color();
        } 
        public void upMovement() {
            bool upControl = true;
            bool win1 = false;
            bool noviBroj = false;
            Label[,] Game = { //Oznake u nizu
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4;i++ )
            {
                int total = 0;
                for (int j = 0; j < 4;j++ )
                {
                    for (int k = j+1; k < 4;k++ )
                    {
                        if(Game[k,i].Text!=""){
                            if(Game[k,i].Text==Game[j,i].Text){
                                win1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[j, i].Text == "")
                    {
                        total++;//Skuplja iste brojeve u nizu
                    }
                    else {
                        for (int m = j; m >= 0;m-- )
                        {
                            if(Game[m,i].Text==""){
                                noviBroj = true;
                                break;
                            }
                        }
                        if (j + 1 < 4)
                        {
                            bool extra = true;
                            
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (Game[k, i].Text != "")
                                {
                                    if (Game[j, i].Text == Game[k, i].Text)
                                    {
                                        upControl = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[ j,i].Text) * 2).ToString();//Dodaje rezultat
                                      
                                        noviBroj = true;
                                        extra = false;
                                        Game[j, i].Text = (int.Parse(Game[j, i].Text) * 2).ToString();
                                        if(total!=0){
                                            Game[j - total, i].Text = Game[j, i].Text;
                                            Game[j, i].Text = "";
                                            
                                        }
                                        Game[k, i].Text = "";
                                       
                                        break;
                                        
                                    }
                                    break;
                                }
                            }
                            if(extra==true && total!=0){
                                upControl = false;
                                Game[j - total, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        else {
                            if(total!=0){
                                upControl = false;
                                Game[j - total, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        
                       
                    }
                }
            }
            if(upControl==false && win1==true){
                sound.Play();
            }
            if (upControl == false && win1 == false)
            {
                sound2.Play();
            }
            if(noviBroj==true){
                colprocesses();
            }
            
        }//Proces
       
        private void Form1_Load(object sender, EventArgs e)
        {
            colprocesses();
            colprocesses();
            colprocesses();//Odredjivanje brojeva na startu
        }// Prvo otvaranje
        public void moveDown()
        {
            bool controlDown = true;
            bool cont1 = false;
            bool caunt = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int total = 0;
                for (int j = 3; j >=0; j--)
                {
                    for (int k = j - 1; k >= 0;k-- )
                    {
                        if(Game[k,i].Text!=""){
                            if(Game[k,i].Text==Game[j,i].Text){
                                cont1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[j, i].Text == "")
                    {
                        total++;
                    }
                    else
                    {
                        for (int m = j+1; m <= 3; m++)
                        {
                            if (Game[m, i].Text == "")
                            {
                                caunt = true;
                                break;
                            }
                        }
                        if (j-1>=0)
                        {
                            bool extra = true;
                            
                            for (int k = j -1 ; k >= 0; k--)
                            {
                                if (Game[k, i].Text != "")
                                {
                                    if (Game[j, i].Text == Game[k, i].Text)
                                    {
                                        controlDown = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[ j,i].Text) * 2).ToString();
                                       
                                        caunt = true;
                                        extra = false;
                                        Game[j, i].Text = (int.Parse(Game[j, i].Text) * 2).ToString();
                                        if (total != 0)
                                        {
                                            Game[j + total, i].Text = Game[j, i].Text;
                                            Game[j, i].Text = "";
                                            
                                        }
                                        Game[k, i].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extra == true && total != 0)
                            {
                                controlDown = false;
                                Game[j + total, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }
                        else
                        {
                            if (total != 0)
                            {
                                controlDown = false;
                                Game[j + total, i].Text = Game[j, i].Text;
                                Game[j, i].Text = "";
                                
                            }
                        }


                    }
                }
            }
            if (controlDown == false && cont1 == true)
            {
                sound.Play();
            }
            if (controlDown == false && cont1 == false)
            {
                sound2.Play();
            }
            if (caunt == true)
            {
                colprocesses();
            }
        }
        public void moveLeft()
        {
            bool controlLeft=true;
            bool cont1 = false;
            bool caunt = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int total = 0;
                for (int j =0; j <4; j++)
                {

                    for (int k = j + 1; k < 4;k++ )
                    {
                        if(Game[i,k].Text!=""){
                            if(Game[i,j].Text==Game[i,k].Text){
                                cont1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[i,j].Text == "")
                    {
                        total++;
                    }
                    else
                    {
                        for (int m = j-1; m >= 0; m--)
                        {
                            if (Game[i, m].Text == "")
                            {
                                caunt = true;
                                break;
                            }
                        }
                        if (j + 1 < 4)
                        {
                            bool extra = true;
                            
                            for (int k = j + 1; k <4; k++)
                            {
                                if (Game[i,k].Text != "")
                                {
                                    
                                    if (Game[i,j].Text == Game[i,k].Text)
                                    {
                                        controlLeft = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[i, j].Text) * 2).ToString();
                                       
                                        caunt = true;
                                        extra = false;
                                        Game[i,j].Text = (int.Parse(Game[i,j].Text) * 2).ToString();
                                        if (total != 0)
                                        {
                                            Game[i,j - total].Text = Game[i,j].Text;
                                            Game[i,j].Text = "";
                                            
                                        }
                                        Game[i,k].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extra == true && total != 0)
                            {
                                controlLeft = false;
                                Game[i,j - total].Text = Game[i,j].Text;
                                Game[i,j].Text = "";
                               
                            }
                        }
                        else
                        {
                            if (total != 0)
                            {
                                controlLeft = false;
                                Game[i,j - total].Text = Game[i, j].Text;
                                Game[i,j].Text = "";
                                
                            }
                        }


                    }
                }
            }
            if (controlLeft == false && cont1 == true)
            {
                sound.Play();
            }
            if (controlLeft == false && cont1 == false)
            {
                sound2.Play();
            }
            if (caunt == true)
            {
                colprocesses();
            }
        }
        public void moveRight()
        { 
            bool rightControl = true;
            bool cont1=false;
            bool caunt = false;
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            for (int i = 0; i < 4; i++)
            {
                int total = 0;
                for (int j = 3; j >= 0; j--)
                {
                    for (int k = j - 1; k >= 0;k-- )
                    {
                        if(Game[i,k].Text!=""){
                            if(Game[i,k].Text==Game[i,j].Text){
                                cont1 = true;
                            }
                            break;
                        }
                    }
                    if (Game[i,j].Text == "")
                    {
                        total++;
                    }
                    else
                    {
                        for (int m = j + 1; m < 4; m++)
                        {
                            if (Game[i,m].Text == "")
                            {
                                caunt = true;
                                break;
                            }
                        }
                        if (j - 1 >= 0)
                        {
                            bool extra = true;
                            
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (Game[i,k].Text != "")
                                {
                                    
                                    
                                    if (Game[i,j].Text == Game[i,k].Text)
                                    {
                                        rightControl = false;
                                        lblScore.Text = (int.Parse(lblScore.Text) + int.Parse(Game[i, j].Text) * 2).ToString();
                                       
                                        caunt = true;
                                        extra = false;
                                        Game[i,j].Text = (int.Parse(Game[i,j].Text) * 2).ToString();
                                        if (total != 0)
                                        {
                                            Game[i, j+total].Text = Game[ i,j].Text;
                                            Game[i,j].Text = "";
                                            
                                        }
                                        Game[i,k].Text = "";
                                        break;

                                    }
                                    break;
                                }
                            }
                            if (extra == true && total != 0)
                            {
                                rightControl = false;
                                Game[i,j+ total].Text = Game[i,j].Text;
                                Game[ i,j].Text = "";
                                
                            }
                        }
                        else
                        {
                            if (total != 0)
                            {
                                rightControl = false;
                                Game[ i,j + total].Text = Game[ i,j].Text;
                                Game[ i,j].Text = "";
                                
                            }
                        }
                    }
                }
            }
            if (rightControl == false && cont1 == true)
            {
                sound.Play();
            }
            if (rightControl == false && cont1 == false)
            {
                sound2.Play();
            }
            if (caunt == true)
            {
                colprocesses();
            }
        }// Brojevi
        public bool number() {
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };     
            for (int i = 0; i < 4;i++ )
            {
                for (int j = 0; j < 4;j++ )
                {
                    if(Game[i,j].Text==""){
                        return false;
                    }
                    for (int k = j+1; k < 4;k++ )
                    {
                        if(Game[i,j].Text!=""){
                            if(Game[i,j].Text==Game[i,k].Text){
                                return false;
                            }
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Game[j, i].Text == "")
                    {
                        return false;
                    }
                    for (int k = j + 1; k < 4; k++)
                    {
                        if (Game[k,i].Text != "")
                        {
                            if (Game[j,i].Text == Game[k,i].Text)
                            {
                                return false;
                            }
                            break;
                        }
                    }
                }
            }
            return true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (number() == false)
            {
                if (e.KeyCode == Keys.Up)
                {
                    upMovement();

                }
                if (e.KeyCode == Keys.Down)
                {
                    moveDown();
                }
                if (e.KeyCode == Keys.Left)
                {
                    moveLeft();
                }
                if (e.KeyCode == Keys.Right)
                {
                    moveRight();
                }
               

            }
            else {
               
                iopet = false;
                lblGameOver.Visible = true;
                btnNewGame.Visible = true;
                btnExit.Visible = true;
                btnExit.Enabled = true;
                btnNewGame.Enabled = true;
                this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            }
           
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        { 
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            lblScore.Text = "0";
            Label[,] Game = {  
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            lblGameOver.Visible = false;
            btnExit.Visible = false;
            iopet = true;
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            btnExit.Enabled = false;
            for (int i = 0; i < 4;i++ )
            {
                for (int j = 0; j < 4;j++ )
                {
                    Game[i, j].Text = "";
                }
            }
            colprocesses();
            colprocesses();
            colprocesses();
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            
            lblAbout.Visible = false;
            label2.Visible = true;
            lblScore.Visible = true;

            if(iopet==false){
                this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            }
            iopet = true;
            lblScore.Text = "0";
            Label[,] Game = { 
                                {lbl1,lbl2,lbl3,lbl4},
                                {lbl5,lbl6,lbl7,lbl8},
                                {lbl9,lbl10,lbl11,lbl12},
                                {lbl13,lbl14,lbl15,lbl16}
                              };
            lblGameOver.Visible = false;
            btnExit.Visible = false;
            btnNewGame.Visible = false;
            btnNewGame.Enabled = false;
            btnExit.Enabled = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Game[i, j].Visible = true;
                    Game[i, j].Text = "";
                }
            }
            colprocesses();
            colprocesses();
            colprocesses();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNewGame_MouseHover(object sender, EventArgs e)
        {
            btnNewGame.BackColor = System.Drawing.Color.Green;
        }

        private void btnNewGame_MouseLeave(object sender, EventArgs e)
        {
            btnNewGame.BackColor = System.Drawing.Color.Orange;
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            btnExit.BackColor = System.Drawing.Color.Green;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackColor = System.Drawing.Color.Orange;
        }

        private void ptbImage_Click(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lbl3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void lblScore_Click(object sender, EventArgs e)
        {

        }

        private void lblGameOver_Click(object sender, EventArgs e)
        {

        }

        private void gamePlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
          MessageBox.Show("Klizna blok iga za jednog igrača. Koristi strelice da pomeraš pločice.Kada se dve pločice sa istim brojem dodirnu, spajaju se u jednu. Cilj ige je spajati pločice sve dok se ne dobije pločica sa brojem 2048. 2048 je prvobitno napisala u  JavaScript-u i CSS-u web developer Gabriele Cirulli.","Kako da igram",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
        }

        private void ınformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(" Project by Zoran Zoltan Kurešević 2018. email: msz.kuresevic@gmail.com","Information", MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
