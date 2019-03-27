using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class User: INotifyPropertyChanged
    {
        private int id;
        [PrimaryKey,AutoIncrement]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        [MaxLength(50)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string lastName;
        [MaxLength(50)]
        public string LasName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
