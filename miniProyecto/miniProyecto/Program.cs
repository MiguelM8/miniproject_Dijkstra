namespace Dijkstra
{
    class Graph
    {
        Dictionary<char, Dictionary<char, int>> vertices = new Dictionary<char, Dictionary<char, int>>();

        public void add_vertex(char name, Dictionary<char, int> edges)
        {
            vertices[name] = edges;
        }

        public List<char> shortest_path(char start, char finish)
        {
            var previous = new Dictionary<char, char>();
            var distances = new Dictionary<char, int>();
            var nodes = new List<char>();

            List<char> path = null;

            foreach (var vertex in vertices)
            {
                if (vertex.Key == start)
                {
                    distances[vertex.Key] = 0;
                }
                else
                {
                    distances[vertex.Key] = int.MaxValue;
                }

                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);

                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == finish)
                {
                    path = new List<char>();
                    while (previous.ContainsKey(smallest))
                    {
                        path.Add(smallest);
                        smallest = previous[smallest];
                    }

                    break;
                }

                if (distances[smallest] == int.MaxValue)
                {
                    break;
                }

                foreach (var neighbor in vertices[smallest])
                {
                    var alt = distances[smallest] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = smallest;
                    }
                }
            }

            return path;
        }
    }

    class MainClass
    {



        public static void Main(string[] args)
        {
         

            Console.WriteLine("Ingrese todos los nodos del Grafo:");
            var lista_nodos = Console.ReadLine().Split(',').ToArray();

            Console.WriteLine("Ingresa el nodo de inicio y final separado de una coma.");
            var inicio_final = Console.ReadLine().Split(',');

        
            var posiciones = new Dictionary<char, Dictionary<char, int>>();


            foreach (var nodo in lista_nodos)
            {

                Console.WriteLine($"\nIngresa todos los nodos adyacentes de \"{nodo}\", ejemplo -> NODO;DISTANCIA:");
                var nodos_adyacentes = Console.ReadLine();
                if (nodos_adyacentes.Length < 1)
                {
                    //si no pone nada continuar.
                    continue;
                }
                /*
                   los adyacentes, separados por coma

                Despues separamos por punto y coma para obtener la distancia:
                NODO y DISTANCIA.
                */

                var lista_adyacentes = nodos_adyacentes.Split(',');
                //corroborar si no tienen adyacentes.

                var adyacentes = new Dictionary<char, int>();

                foreach (var nodo_adyacente in lista_adyacentes)
                {
                    var valores = nodo_adyacente.Split(';');
                    char valor_nodo = valores[0].ToCharArray()[0];
                    int.TryParse(valores[1], out int valor_distancia);

                    adyacentes.Add(valor_nodo, valor_distancia);

                    Console.WriteLine($"- Adyacente de \"{nodo}\": {valor_nodo} con una distancia de {valor_distancia}");
                }
                posiciones.Add(nodo.ToCharArray()[0], adyacentes);
            }



            Graph g = new Graph();

            foreach(var (nodo, adyacentes) in posiciones)
            {
                g.add_vertex(nodo, adyacentes);
            }


            var inicio = inicio_final[0].ToCharArray()[0];
            var fin = inicio_final[1].ToCharArray()[0];


            var camino_corto = g.shortest_path(inicio, fin);
            camino_corto.Add(inicio); //agregamos el nodo de inicio
            camino_corto.Reverse(); //volteamos la lista.

            var camino = string.Join(" -> ", camino_corto);

            Console.WriteLine($"\n\n[!] El camino mas corto de \"{inicio}\" a \"{fin}\" es: \n{camino}");
        }
    }
}