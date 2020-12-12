using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstran搜尋
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Node>> dic = new Dictionary<string, List<Node>>()
            {
                {"A",new List<Node>{ new Node {Node_name="B",distance=5 },new Node {Node_name="C",distance=1 } } },
                {"B",new List<Node>{ new Node {Node_name="A",distance=5 },new Node {Node_name="C",distance=2},
                new Node{Node_name="D",distance=1 } } },
                {"C",new List<Node>{ new Node{Node_name="A",distance=1},new Node { Node_name="B",distance=2},
                new Node{Node_name="D",distance=4 },new Node{ Node_name="E",distance=8} } },
                {"D",new List<Node>{ new Node {Node_name="B",distance=1 },new Node {Node_name="C",distance=2},
                new Node{ Node_name="E",distance=3},new Node{Node_name="F",distance=6 } } },
                {"E",new List<Node>{ new Node { Node_name="C",distance=8},new Node { Node_name="D",distance=3} } },
                {"F",new List<Node>{ new Node { Node_name="D",distance=6} } }
            };
            string input = Console.ReadLine();
            Dictionary<string, int> result = Dijkstra(dic, new Node { Node_name = input, distance = 0 });
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Key},距離:{item.Value}");
            }
        }

        private static Dictionary<string, int> Dijkstra(Dictionary<string, List<Node>> dic, Node start_node)
        {
            Dictionary<string, int> output = new Dictionary<string, int>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(start_node);
            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                List<Node> origin = new List<Node>();
                foreach (var item in queue)
                {
                    origin.Add(item);
                }
                //IEnumerable<Node> q = null;
                foreach (var item in dic[node.Node_name])
                {
                    if (!output.ContainsKey(item.Node_name))
                    {
                        item.distance += node.distance;
                        if (origin.FirstOrDefault(p => p.Node_name == item.Node_name) == null)
                        {
                            origin.Add(item);
                        }
                        else
                        {
                            foreach (var item1 in origin)
                            {
                                if (item1.Node_name == item.Node_name && item1.distance > item.distance)
                                {
                                    item1.distance = item.distance;
                                }
                            }
                        }
                    }
                }
                //太扯了 C#沒有會自動排序的QUEUE
                queue.Clear();
                foreach (var item in origin.OrderBy(p => p.distance))
                {
                    queue.Enqueue(item);
                }
                output.Add(node.Node_name, node.distance);
            }
            return output;
        }
    }
    class Node
    {
        public string Node_name { get; set; }
        public int distance { get; set; }
    }
}
