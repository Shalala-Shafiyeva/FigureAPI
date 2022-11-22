using Newtonsoft.Json;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Figure
{
    public static class Manager
    {
        public static List<Figure> FigureList = new List<Figure>();

        private static string path = @"figure.json";

        public static void SaveToFile()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                Newtonsoft.Json.JsonSerializer jsonSerializer = Newtonsoft.Json.JsonSerializer.
                    Create(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented });
                jsonSerializer.Serialize(sw, FigureList);
            }
        }

        public static List<Figure> GetListOfFiguresFromFile()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                Newtonsoft.Json.JsonSerializer jsonSerializer = Newtonsoft.Json.JsonSerializer.
                    Create(new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects, Formatting = Formatting.Indented });

                List<Figure> result = new List<Figure>();
                result = (List<Figure>)jsonSerializer.Deserialize(sr, result.GetType());

                return result;
            }
        }

        public static void InitializeDefaultFigures()
        {
            List<Point> trianglePoints = new List<Point>();
            trianglePoints.AddRange(new List<Point>
            {
                new Point(0, 0),
                new Point(0, 4),
                new Point(3, 0)
            });
            Triangle triangle = new Triangle(trianglePoints);

            List<Point> circlePoints = new List<Point>();
            circlePoints.AddRange(new List<Point>
            {
                new Point(0, 0),
                new Point(0, 3)
            });
            Circle circle = new Circle(circlePoints);

            FigureList.AddRange(new List<Figure>
            {
                triangle,
               
                circle
            });
        }

        public static void InitializeFromFile()
        {
            FigureList = GetListOfFiguresFromFile();
        }

        public static void DeleteFigureFromFile(int figureId)
        {
            //FigureList.Remove(FigureList[figureId - 1]);
            FigureList.Remove(FigureList!.Find(s => s.Id == figureId));
            SaveToFile();
        }
    }
}
