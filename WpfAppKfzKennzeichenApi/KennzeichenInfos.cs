using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppKfzKennzeichenApi
{
    internal class KennzeichenInfos
    {


        public Kennzeichen[] Property1 { get; set; }
    }

    public class Kennzeichen
    {
        public int id { get; set; }
        public string kennzeichen { get; set; }
        public string ort { get; set; }
        public string kreis { get; set; }
        public string bundesland { get; set; }
        public string wappen { get; set; }
        public override string ToString()
        {
            string str= id+ " " + kennzeichen + " " + ort;
            return str;
        }

        public Kennzeichen(int id, string kennzeichen, string ort, string kreis, string bundesland, string wappen)
        {
            this.id = id;
            this.kennzeichen = kennzeichen;
            this.ort = ort;
            this.kreis = kreis;
            this.bundesland = bundesland;
            this.wappen = wappen;
        }

        public Kennzeichen(string kennzeichen, string ort, string kreis, string bundesland, string wappen)
        {
            this.kennzeichen = kennzeichen;
            this.ort = ort;
            this.kreis = kreis;
            this.bundesland = bundesland;
            this.wappen = wappen;
        }

        public Kennzeichen()
        {
        }
    }
}
