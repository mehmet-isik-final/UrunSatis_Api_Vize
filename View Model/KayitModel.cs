using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatis.View_Model
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitUrunId { get; set; }
        public string kayitKategoriId { get; set; }
        public UrunModel urunBilgi{ get; set; }
        public KategoriModel kategoriBilgi { get; set; }
    }
}