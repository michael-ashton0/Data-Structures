using System;
using System.Collections.Generic;

namespace Kevin
{
    class Project_3_Michael_Ashton_Spring_2024
    {
        static void Main()
        {
            Graph list = new Graph();

            //load list
            list.LoadIMDb();

            bool isRunning = true;
            
            while(isRunning == true)
            {
                    //get actors

                    Console.WriteLine($"Enter your first actor: ");
                    string actor1 = Console.ReadLine();

                    if (list.ContainsVertex(actor1) == false)
                    {
                        isRunning = false;
                        return;
                    }

                    Console.WriteLine($"\nEnter your second actor :");
                    string actor2 = Console.ReadLine();

                    if (list.ContainsVertex(actor2) == false)
                    {
                        isRunning = false;
                        return;
                    }
                    //write fun facts

                    Console.WriteLine($"\nFun fact! {actor1} has starred in the following movies: \n");

                    list.Factoid(actor1);
                    Console.WriteLine($"\nWhile {actor2} has starred in the following movies: \n");

                    list.Factoid(actor2);
                    Console.WriteLine();

                    //are the two actors even connected?
                    list.TestConnectedTo(actor1, actor2);

                    if (list.TestConnectedTo(actor1, actor2) == true)
                    {
                        //display the shortest list
                        list.ShortestPath(actor1, actor2);
                    }
                    else
                    {
                        Console.WriteLine();
                    }

                    Console.WriteLine("\nPlay again? (yes/no)");
                    string modifier = Console.ReadLine();
                    modifier.ToLower();

                    if (modifier == "yes")
                    {
                        isRunning = true;
                    }
                    else if (modifier == "no")
                    {
                        Console.WriteLine("\nThanks for playing!");
                        isRunning = false;
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nPlease answer either yes or no - make sure you did not add any whitespace characters");
                        isRunning = false;
                        return;
                    }
            }
        }
    }

    public class Graph
    {
        string tfile = @"C:\Users\Michael Ashton\Downloads\CSC\Data Structures CSC-212\Project_3_Michael_Ashton_Spring_2024\imdb.biglist.txt";
        //string tfile = @"C:\Users\Michael Ashton\Downloads\CSC\Data Structures CSC-212\Project_3_Michael_Ashton_Spring_2024\imdb.top250.txt";

        MathGraph<string> list = new MathGraph<string>();
        /// <summary>
        /// Loads the database
        /// </summary>
        public void LoadIMDb()
        {
            SortedDictionary<string, bool> addedActors = new SortedDictionary<string, bool>();
            SortedDictionary<string, bool> addedMovies = new SortedDictionary<string, bool>();

            foreach (string line in File.ReadLines(tfile))
            {
                string[] parts = line.Split('|');

                if (parts.Length >= 2)
                {
                    string actor = parts[0].Split('(')[0].Trim();
                    string movie = parts[1].Trim();

                    if (!addedActors.ContainsKey(actor))
                    {
                        list.AddVertex(actor);
                        addedActors.Add(actor, true);
                    }

                    if (!addedMovies.ContainsKey(movie))
                    {
                        list.AddVertex(movie);
                        addedMovies.Add(movie, true);
                    }

                    list.AddEdge(actor, movie);
                }
            }
        }
        /// <summary>
        /// Finds the shortest path between two actors
        /// </summary>
        /// <param name="actor1"></param>
        /// <param name="actor2"></param>
        public void ShortestPath(string actor1, string actor2)
        {
            double count = 0;
            List<string> firstpath = list.FindShortestPath(actor1, actor2);

            foreach (string vertex1 in firstpath)
            {
                Console.WriteLine(vertex1);
                count++;
            }
            count = count - 2;
            count = Math.Ceiling(count / 2);
            Console.WriteLine($"\nThe shortest path between these actors contains {count} movies.");
        }
        /// <summary>
        /// looks to see if there is a route at all between the actos
        /// </summary>
        /// <param name="vertex1"></param>
        /// <param name="vertex2"></param>
        /// <returns></returns>
        public bool TestConnectedTo(string vertex1, string vertex2)
        {
            if (list.TestConnectedTo(vertex1, vertex2) == true)
            {        
                return true;
            }
            else
            {
                Console.WriteLine($"Wow! You found two unconnected actors!");
                return false;
            }
        }
        /// <summary>
        /// returns a fun fact (a list of their film appearances)
        /// </summary>
        /// <param name="actor"></param>
        public void Factoid(string actor)
        {
            int count = 0;
            foreach (string vertex in list.EnumAdjacent(actor))
            {
                Console.WriteLine(vertex);
                count++;
            }
        }
        /// <summary>
        /// checks to see if the vertices ar present in the graph
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public bool ContainsVertex(string actor)
        {
            if (list.ContainsVertex(actor) == false)
            {
                Console.WriteLine("That actor does not appear in our database (did you spell their name right?)");
                return false;               
            }
            else
            {
                return true;
            }
        }

    }
}

