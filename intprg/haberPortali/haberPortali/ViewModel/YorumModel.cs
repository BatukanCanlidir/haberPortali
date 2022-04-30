using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haberPortali.ViewModel
{
    public class YorumModel
    {
        public int yorumId { get; set; }
        public string yorumIcerik { get; set; }
        public int yorumUyeId { get; set; }
        public int yorumHaberId { get; set; }
        public System.DateTime yorumTarih { get; set; }
    }
}