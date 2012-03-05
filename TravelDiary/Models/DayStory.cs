﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace Diary.Models
{
    public class DayStory
    {

        public string Day { get; set; }
        public string Header { get; set; }
        public string Img { get; set; }
        public string Text { get; set; }
        
        public List<Image> Images  { get; set; }
        public List<Reply> Comments  { get; set; }

        //toDo: Exclude data to ext ilayer and dal-class

        public static DayStory GetSingleDay(string date)
        {
            DayStory d = null;
            try {
                var sPath = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + date;
                var diaryText = TextFile.ReadFileWithoutEncoding(sPath + "\\main.txt");
                var header = diaryText.Substring(4, diaryText.IndexOf("-->") - 4);
                var im = Image.GetAllImages(sPath, date)[0].Url;
                //diaryText = diaryText.Replace("\r\n", "<br>").Replace("\r", "<br>").Replace("\n", "<br>");
                d = new DayStory {Header = header, Day = date, Img = im, Text = diaryText, Comments = Reply.GetReplies(sPath), Images = Image.GetAllImages(sPath, date)};
            }
            catch (Exception ex) {
                return null;
                throw ex;
            }
            return d;
        }

        public List<DayStory> GetAllDays()
        {
            var stories = new List<DayStory>();
            var sPath = AppDomain.CurrentDomain.BaseDirectory + "Upload";
            var dirs = new DirectoryInfo(sPath);
            var comparer = new myReverserClass();
            DirectoryInfo[] fldrs = dirs.GetDirectories();
            Array.Sort(fldrs, comparer);
            foreach (var dirInfo in fldrs) {
                var d = GetSingleDay(dirInfo.Name);
                stories.Add(d);
            }
            return stories;
        }

        public static void Save(DayStory d)
        {
            var sPath = AppDomain.CurrentDomain.BaseDirectory + "Upload\\" + d.Day;
            Directory.CreateDirectory(sPath);
            Directory.CreateDirectory(sPath+"\\thumbnails");
            sPath += "\\main.txt";
            TextFile.CreateFile(sPath);
            TextFile.AppendToFile(sPath, d.Text);
        }
    }

      public class myReverserClass : IComparer  {
           public int Compare(object o1, object o2)
            {
            var info1 = (DirectoryInfo) o1;
            var info2 = (DirectoryInfo) o2;

            return DateTime.Compare(info2.CreationTime,info1.CreationTime);
            }
        }

    
}