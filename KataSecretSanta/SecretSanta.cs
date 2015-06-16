using System;
using System.Collections.Generic;
using System.Linq;

namespace KataSecretSanta
{
    public class SecretSanta
    {
        private Dictionary<Person, int> _guestIndexes;
 
        public List<Person> Guests { get; private set; }

        /// <summary>
        /// Empty parameterless constructor
        /// </summary>
        public SecretSanta()
        { }

        /// <summary>
        /// Constructor with a list of guests already built 
        /// </summary>
        /// <param name="guests"></param>
        public SecretSanta(List<Person> guests)
        {
            Guests = guests;
        }

        /// <summary>
        /// Parse a string that contains the full name of all the guests 
        /// who will receive and offer a gift. 
        /// This string is composed of lines and each line contains the name of a Guests
        /// The name of the guests are supposed to be simple: 
        /// one string for the first name, one space and one string for the last name
        /// </summary>
        /// <param name="file"></param>
        public void Parse(string file)
        {
            Guests = file.Split('\n')
                .Select(s =>
                {
                    var tab = s.Trim().Split(' ');
                    return new Person {FirstName = tab[0], LastName = tab[1]};
                }).ToList();
        }

        /// <summary>
        /// Step 1: This method picks up a list of secret santa for each guest
        /// The only constraint is that a guest can't be his own secret santa
        /// </summary>
        /// <returns></returns>
        public List<Person> OfferGiftsWithNoConstraint()
        {
            _guestIndexes = Guests.ToIndexDictionary();
            var santas = new Person[Guests.Count];
            
            var shuffledGuests = new List<Person>(Guests);
            shuffledGuests.Shuffle();
            for (int n = 0; n < shuffledGuests.Count; n++)
            {
                var guest = shuffledGuests[n];
                var santa = shuffledGuests[(n + 1) % shuffledGuests.Count];
                int guestIndex = _guestIndexes[guest];
                santas[guestIndex] = santa;
            }
            return santas.ToList();
        }

        /// <summary>
        /// Step 2: This methods picks up a santa for each guest who shouldn't be 
        /// part of the same family
        /// </summary>
        /// <returns></returns>
        public List<Person> OfferGiftsNotInTheSameFamily()
        {
            if (!Guests.Any())
                return new List<Person>();

            var santas = new Person[Guests.Count];
            _guestIndexes = Guests.ToIndexDictionary();

            // Group the guest by family, ordered by the number of members
            List<Person>[] families = Guests
                .GroupBy(p => p.LastName)
                .Select(p => p.ToList())
                .OrderByDescending(l => l.Count)
                .ToArray();

            if (families[0].Count > Guests.Count/2)
            {
                throw new Exception(
                    string.Format("Invalid list of guests... There are too many people in the {0} family",
                    families[0][0].LastName));
            }
            Random rnd = new Random((int)DateTime.Now.Ticks);

            // Pick up the first guest who will receive a gift from the 
            // biggest family
            Person guest = families[0][rnd.Next(families[0].Count)];

            for (int i = 0; i < Guests.Count; i++)
            {
                Person santa = pickUpFromOtherBiggestFamily(families, guest, rnd);
                santas[_guestIndexes[guest]] = santa;
                // Now the santa becomes a guest
                guest = santa;
            }
            // The last guest needs a santa.
            // This santa is the last guests who hasn't been selected as a santa yet
            santas[_guestIndexes[guest]] = getLastSanta(santas);
            return santas.ToList();
        }

        /// <summary>
        /// Always pick up a santa in the members of the family containing the biggest 
        /// number of members and different from the guest's family
        /// </summary>
        /// <param name="families"></param>
        /// <param name="guest"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private Person pickUpFromOtherBiggestFamily(List<Person>[] families, Person guest, Random rnd)
        {
            var family = families.Where(f => f.Count > 0 && f[0].LastName != guest.LastName)
                .OrderByDescending(f => f.Count)
                .First();
            // The santa is randomly chosen in the family
            Person santa = family[rnd.Next(family.Count)];
            // When a santa has been selected, he's removed from his family
            family.Remove(santa);
            return santa;
        }

        private Person getLastSanta(IEnumerable<Person> santas)
        {
            var set = new HashSet<Person>(santas.Where(s => s != null));
            return Guests.Except(set).First();
        }
    }
}
