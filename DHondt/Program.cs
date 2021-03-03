using System;
using System.IO;
using System.Linq;

namespace DHondt
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("Assessment1Data.txt");
            string ELECTION_NAME = lines[0];
            int NUM_OF_SEATS = int.Parse(lines[1]);
            int TOTAL_VOTES = int.Parse(lines[2]);
            Party[] all_parties = new Party[lines.Length - 3];

            for (int i = 3; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                string name = parts[0];
                int votes = int.Parse(parts[1]);
                string[] seats = parts[2..];
                all_parties[i - 3] = new Party(name, votes, seats);
            }

            foreach (Party p in all_parties) //Testing loop that all atributes were added correctly
            {
                Console.WriteLine(p.get_party_name());
                Console.WriteLine(p.get_calculated_votes());
                p.get_seat_list().ToList().ForEach(i => Console.Write(i + " "));
            }
        }
    }

    public class Party
    {
        private string party_name;
        private int total_votes;
        private int num_of_seats;
        private string[] seat_list;

        public Party(string name, int votes, string[] seats)
        {
            party_name = name;
            total_votes = votes;
            seat_list = seats;
            num_of_seats = 0;
        }

        public int get_calculated_votes()
        {
            return total_votes / (num_of_seats + 1);
        }

        public string get_party_name()
        {
            return party_name;
        }

        public string[] get_seat_list()
        {
            return seat_list;
        }

        public void increment_seats()
        {
            num_of_seats++;
        }
    }
}
