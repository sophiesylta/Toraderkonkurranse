namespace Toraderkonkurranse.Domene
{
    public class Deltaker
    {
        public Deltaker()
        {
            personer = new List<Person>();
        }
        public Deltaker(string navn, List<Person> personer)
        {
            this.personer = personer;
            this.navn = navn;
        }
        public int deltakerID { get; set; }
        public string navn { get; set; }
        public List<Person> personer { get; set; }

        public Boolean leggTilPerson(Person person)
        {
            //TODO sjekk at det er plass til flere personer
            personer.Add(person);
            return true;
        }

    }
}