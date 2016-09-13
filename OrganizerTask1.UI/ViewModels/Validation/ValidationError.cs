using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerTask1.UI.ViewModels.Validation
{
    public class ValidationError
    {
        public string ID;
        public string message;
        public string propertyName;

        public ValidationError(string propName, string errorID, string errorMessage)
        {
            propertyName = propName;
            ID = errorID;
            message = errorMessage;
        }

        public override string ToString() { return message; }

        public string Description
        {
            get
            {
                StringBuilder buidler = new StringBuilder();
                return buidler.Append("Property Name='")
                    .Append(propertyName)
                    .Append("', ID='")
                    .Append(ID)
                    .Append("', Msg='")
                    .Append(message)
                    .Append("'").ToString();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ValidationError other = obj as ValidationError;
            if (other == null) return false;
            return Equals(other);
        }

        public bool Equals(ValidationError other)
        {
            if (other == null)
                return false;
            return (this.propertyName == other.propertyName && this.ID == other.ID);
        }

        public override int GetHashCode()
        {
            return propertyName.GetHashCode() ^ ID.GetHashCode();
        }
    }
}
