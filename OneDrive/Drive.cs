using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
namespace OneDrive
{
    public class drive
    {
        public string id;
        public string name;
        public string description;
        public string parent_id;

        public static bool operator ==(drive first, drive second)
        {
            if (first.name == second.name) { return true; }
            else return false;
        }
        public static bool operator ==(drive thisDrive, string fileName)
        {
            if (thisDrive.name == fileName)
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(drive thisDrive, string fileName)
        {
            if (thisDrive.name != fileName)
            {
                return true;
            }
            else return false;
        }
        public static bool operator !=(drive first, drive second)
        {
            if (first.name == second.name) { return false; }
            else return true;
        }
        public override bool Equals(object obj)
        {
            drive tempD = (drive)obj;
            string temp = obj.ToString();
            if (this.name == temp)
            {
                return true;
            }
            else return false;

        }
        public override string ToString()
        {
            return this.name;
        }

        /*{
            
         "id":  "folder.69815dd3faad4564.69815DD3FAAD4564!105",                          
         "name":  "Документы",             
         "parent_id":  "folder.69815dd3faad4564",   
         
        },*/
    }
    public class driveList
    {
        public List<drive> data { get; set; }
    }
    public class descriptionSer
    {
        public string description;
    }
    public class folderName
    {
        public string name;
    }
}
