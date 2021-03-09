using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Kursach
{

    public class Game
    {
        public Game()
        {
            this.Genres = new HashSet<Genre>();
            this.Igri = new HashSet<Developer>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        //название игры
        public string Link { get; set; }         //ссылка на игру
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<Developer> Igri { get; set; }

        [NotMapped]
        public string Ganr
        {
            get
            {
                string ganri = "";
                foreach (Genre ganre in Genres)
                {
                    ganri += ganre.GName + ", ";
                }
                if (ganri != "")
                {
                    return ganri.Substring(0, ganri.Length - 2);
                }
                else
                    return ganri;
                
            }
        }

        [NotMapped]
        public string Razrabotchik
        {
            get
            {
                string devo = "";
                foreach (Developer dev in Igri)
                {
                    devo += dev.DName + ", ";
                }
                if (devo != "")
                {
                    return devo.Substring(0, devo.Length - 2);
                }
                else
                    return devo;
                
            }
        }
    }
       
    
        
        public class Genre
        {
            public Genre()
            {
                this.Games = new HashSet<Game>();
            }
            [Key]
            public int Id { get; set; }
            public string GName { get; set; }    //название жанра
            public virtual ICollection<Game> Games { get; set; }
        }

        public class Developer
        {
            public Developer()
            {
                this.Igri = new HashSet<Game>();
            }
            [Key]
            public int Id { get; set; }
            public string DName { get; set; } //разработчик
            public virtual ICollection<Game> Igri { get; set; }
        }

    
}
