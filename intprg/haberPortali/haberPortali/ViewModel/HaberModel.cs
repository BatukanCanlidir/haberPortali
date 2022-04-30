using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haberPortali.ViewModel
{
    public class HaberModel
    {
        public int haberId { get; set; }
        public string haberBaslik { get; set; }
        public string haberDetay { get; set; }
        public System.DateTime haberTarih { get; set; }
        public int haberKategoriId { get; set; }
        public int haberUyeId { get; set; }
        public int haberOkuma { get; set; }
    }
}