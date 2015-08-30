using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TestPlayer
{
    public class MusicSource
    {
        private List<Music> Music = new List<Music>();
        private int CurrentSongIndex=0;
        private void GetSongNameAndUrl(string SongUrl)
        {
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(SongUrl);
                int i = 0;
                string DounloadUrl;
                while (i < htmlCode.Length-22)
                {
                    //ищем имя
                    //тут у меня возникли проблемы с кодировкой русских названий. пришлось отказаться
                    /*if (htmlCode.Substring(i, 7) == "\"name\">")
                    {
                        i = i + 7;
                        int j = i;
                        while (htmlCode.Substring(j, 1) != "<")//первая часть
                        {
                            j++;
                        };
                        string SongName;
                        SongName= htmlCode.Substring(i, j - i);
                        j = i;
                        while (htmlCode.Substring(j, 1) != ">")//пропускаем </span>
                        {
                            j++;
                        };
                        i = j + 1; j = i;
                        while (htmlCode.Substring(j, 1) != "\t")//вторая часть
                        {
                            j++;
                        };
                        SongName =SongName + htmlCode.Substring(i, j - i);
                        i = j;
                    };*/
                    //ищем ссылку
                    if (htmlCode.Substring(i, 21) == "http://dl.zaycev.net/")
                    {
                        int j = i;
                        while (htmlCode.Substring(j, 1) != "\"")
                        {
                            j++;
                        };
                        DounloadUrl = htmlCode.Substring(i, j - i);
                        //вырежем название на английском из имени файла
                        j = DounloadUrl.Length;
                        while (DounloadUrl.Substring(j-1, 1) != "_")
                        {
                            j--;
                        };
                        int k = j;
                        while (DounloadUrl.Substring(k, 1) != "/")
                        {
                            k--;
                        };
                        string Name = DounloadUrl.Substring(k+1, j - k-2);
                        Music.Add(new Music { name = Name, url = DounloadUrl });
                    };
                    i++;
                };
            }
        }
        public void GetSongs(){
            using (WebClient client = new WebClient()){
                string htmlCode = client.DownloadString("http://zaycev.net/");
                int i=0;
                bool flagAgainsDoubles = true;
                string SongUrl;
                while(i<htmlCode.Length-8){
                    if(htmlCode.Substring(i, 7) == "/pages/") {
                        int j = i;
                        while (htmlCode.Substring(j,1)!="\""){
                            j++;
                        };
                        SongUrl = "http://zaycev.net" + htmlCode.Substring(i, j - i);
                        if (flagAgainsDoubles){
                            GetSongNameAndUrl(SongUrl);
                            flagAgainsDoubles = !flagAgainsDoubles;
                        }
                        else {
                            flagAgainsDoubles = !flagAgainsDoubles;
                        }
                        i = j;
                    };
                    i++;
                };
            }
        }
        public string GetSongName(){
            string name = Music.ElementAt(CurrentSongIndex).name;
            return name;
        }
        public string GetSongName(int index)
        {
            string name = Music.ElementAt(index).name;
            return name;
        }
        public int count()
        {
            int i = this.Music.Count;
            return i;
        }
        public string GetSongUrl(){
            string url=Music.ElementAt(CurrentSongIndex).url;
            return url;
        }
        public List<string> GetSongsList(){
            List<string> SongList = new List<string>();
            for (int i=0; i < Music.Count; i++) {
                SongList.Add(this.GetSongName() + "\n");
            }
            return SongList;
        }
        public void NextSong(){
            if (CurrentSongIndex < Music.Count)
            {
                CurrentSongIndex++;
            }
        }
        public void PreviosSong() {
            if (CurrentSongIndex > 0) {
                CurrentSongIndex--;
            }
        }
        public void SetCurrentSong(int i) {
            if (i >= 0 && i < Music.Count) {
                this.CurrentSongIndex = i;
            }
        }
        public int CurrentSong() {
            return this.CurrentSongIndex;
        }
    }
}
