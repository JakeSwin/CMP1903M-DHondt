using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DHondt
{
    class Program
    {
        static void Main(string[] args)
        {   
            //sets up a string array for the data in the text file
            string[] lines = File.ReadAllLines("Assessment1Data.txt"); 
            string ELECTION_NAME = lines[0];
            int NUM_OF_SEATS = int.Parse(lines[1]);
            int TOTAL_VOTES = int.Parse(lines[2]);
            Party[] all_parties = new Party[lines.Length - 3];

            //takes all data from the array, instantiates objects and places it into an array
            for (int i = 3; i < lines.Length; i++) 
            {
                string[] parts = lines[i].TrimEnd(';').Split(',');
                string name = parts[0];
                int votes = int.Parse(parts[1]);
                string[] seats = parts[2..];
                all_parties[i - 3] = new Party(name, votes, seats);
            }

            //the main DHondt loop which calculates the number of seats for each party
            for (int i = 0; i < NUM_OF_SEATS; i++) 
            {
                int highest_votes = 0;
                Party highest_votes_party = null;
                foreach (Party p in all_parties)
                {
                    int calculated_votes = p.calculated_votes;
                    if (calculated_votes > highest_votes) 
                    {
                        highest_votes = calculated_votes;
                        highest_votes_party = p;
                    }
                }
                highest_votes_party.increment_seats();
            }

            //creats the list that holds each line of the output textfile
            List<string> output_lines = new List<string>();
            output_lines.Add(ELECTION_NAME);

            //looks through each party and if it has 1 or more seat it will add it to the text file
            foreach (Party p in all_parties)
            {
                int num_party_seats = p.num_of_seats;
                if (num_party_seats > 0)
                {
                    string party_output_lines = "";
                    string party_name = p.party_name;
                    string[] party_seats = p.seat_list[..num_party_seats];

                    //creates and formats information to be outputted
                    party_output_lines += party_name + ",";
                    for (int i = 0; i < num_party_seats; i++)
                    {
                        party_output_lines += party_seats[i] += i == num_party_seats - 1 ? ";" : ",";
                    }

                    output_lines.Add(party_output_lines);
                }
            }

            //outputs the text file
            File.WriteAllLines("Assessment1TestResultsOutput.txt", output_lines);
        }
    }

    public class Party 
    {
        private string _party_name;
        private int _total_votes;
        private int _num_of_seats;
        private string[] _seat_list;

        //class constructor
        public Party(string name, int votes, string[] seats) 
        {
            _party_name = name;
            _total_votes = votes;
            _seat_list = seats;
            _num_of_seats = 0;
        }

        //returns the calculated votes from the below equation
        public int calculated_votes 
        {
            get { return _total_votes / (_num_of_seats + 1); }
        }

        //returns the name of the party 
        public string party_name 
        {
            get { return _party_name; }
        }

        //returns the seat list
        public string[] seat_list 
        {
            get { return _seat_list; }
        }

        //returns the name of the seats and sets them to int 'value'
        public int num_of_seats 
        {
            get { return _num_of_seats; }  
            private set { _num_of_seats = value; }
        }

        public void increment_seats()
        {
            num_of_seats += 1;
        }
    }
}
