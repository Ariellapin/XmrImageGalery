﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageGalery.Extensions
{
    public class Gallary
    {
        IEnumerable<Picture> pictures;
        int position;
        public Picture setImages(string path) {
            FileInfo fi = new FileInfo(path);
            pictures = fi.Directory.GetFiles("*.jpg", SearchOption.AllDirectories).Select(f => new Picture{ 
                date = f.LastWriteTime, 
                name = f.Name,
                path=f.FullName 
            });

            position = 0;
            return pictures.FirstOrDefault();
        }

        public int Delete(bool accept) {
            var delPic = pictures.Where(p => p.delete);
            
            if (accept)
            {
                delPic.ToList().ForEach( f => File.Delete(f.path));
            }
            int deletedpict = delPic.Count();
            pictures = pictures.Where(p => !p.delete);
            return deletedpict;
        }
        public Picture SetDelete() {
            pictures.ElementAt<Picture>(position).delete = true;
            return Next();
        }
        public Picture Next(int count = 1)
        {
            position += count;
            
            if (pictures == null || pictures.Count() == 0)
                return null;
            if (position >= pictures.Count())
                position = 0;

            if (position < 0)
                position = pictures.Count();

            return pictures.ElementAt<Picture>(position);
            
        }
    }
}
