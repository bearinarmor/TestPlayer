using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace TestPlayer
{
    public partial class Form1 : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern
            Boolean PlaySound(string lpszName, int hModule, int dwFlags);

        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        public MusicSource MusicSourse = new MusicSource();
        public bool IsPlaying = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IsPlaying)
            {
                WMP.URL = @MusicSourse.GetSongUrl();
                WMP.controls.play();
                IsPlaying = !IsPlaying;
            }
            else {
                WMP.controls.stop();
                IsPlaying = !IsPlaying;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.MusicSourse.PreviosSong();
            this.listBox1.SetSelected(this.MusicSourse.CurrentSong(), true);
            if (IsPlaying)
            {
                WMP.controls.stop();
                WMP.URL = @MusicSourse.GetSongUrl();
                WMP.controls.play();
            }
        }

         private void button4_Click(object sender, EventArgs e)
        {
            this.MusicSourse.NextSong();
            this.listBox1.SetSelected(this.MusicSourse.CurrentSong(), true);
            if (IsPlaying)
            {
                WMP.controls.stop();
                WMP.URL = @MusicSourse.GetSongUrl();
                WMP.controls.play();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MusicSourse.GetSongs();
            for(int i=0; i<this.MusicSourse.count(); i++){
                this.listBox1.Items.Add(this.MusicSourse.GetSongName(i));
            }
            this.listBox1.SetSelected(this.MusicSourse.CurrentSong(), true);

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            this.MusicSourse.SetCurrentSong(this.listBox1.SelectedIndex); 
            if (IsPlaying)
            {
                WMP.controls.stop();
                WMP.URL = @MusicSourse.GetSongUrl();
                WMP.controls.play();
            }
        }

                
    }
}
