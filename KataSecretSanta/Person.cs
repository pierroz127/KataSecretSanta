namespace KataSecretSanta
{
    public class Person
    {
        public string LastName { get; set; }
       
        public string FirstName { get; set; }

        public string Name
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = (int)2166136261;
                if (!string.IsNullOrEmpty(FirstName)) 
                    hash = hash * 16777619 ^ FirstName.GetHashCode();
                if (!string.IsNullOrEmpty(LastName)) 
                    hash = hash * 16777619 ^ LastName.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            Person other = obj as Person;
            if (other == null)
                return false;

            return FirstName == other.FirstName && LastName == other.LastName;
        }
    }
}
