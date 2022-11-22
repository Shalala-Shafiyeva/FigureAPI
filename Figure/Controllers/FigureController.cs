using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Figure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FigureController : ControllerBase
    {
        private readonly ILogger<FigureController> _logger;
         public FigureController(ILogger<FigureController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ResetFigures")]
        public IEnumerable<Figure> ResetFigures()
        {
            Manager.InitializeDefaultFigures();
            Manager.SaveToFile();
            return Manager.GetListOfFiguresFromFile();
        }

        [HttpGet ("GetFigures")]
        public IEnumerable<Figure> GetFigures()
        {
            
            return Manager.GetListOfFiguresFromFile();
        }

        [HttpGet("GetFigure/{figureId}")]
        public Figure GetFigureWithId(int figureId)
        {
            return Manager.GetListOfFiguresFromFile().Find(s => s.Id == figureId);
        }

        [HttpDelete("DeleteFigure/{figureId}")]
        public IEnumerable<Figure> DeleteWithId(int figureId)
        {

            if (!Manager.FigureList.Any())
            {
                Manager.InitializeFromFile();
            }

            Manager.DeleteFigureFromFile(figureId);

            return Manager.GetListOfFiguresFromFile();
        }

        [HttpPost("CreateCicle/{centerCoordX}/{centerCoordY}/{coordOncircleX}/{coordOncircleY}")]
        public IEnumerable<Figure> CreateCicle(double centerCoordX, double centerCoordY, double coordOncircleX, double coordOncircleY)
        {
            if (!Manager.FigureList.Any())
            {
                Manager.InitializeFromFile();
            }

            List<Point> points = new List<Point>();

            points.Add(new Point(centerCoordX, centerCoordY));
            points.Add(new Point(coordOncircleX, coordOncircleY));

            Manager.FigureList.Add(new Circle(points));
            Manager.SaveToFile();

            return Manager.GetListOfFiguresFromFile();
        }

        [HttpPatch("MoveFigure/{figureId}")]
        public Figure MoveFigure(int figureId, double coordinateX, double coordinateY)
        {
            if (!Manager.FigureList.Any())
            {
                Manager.InitializeFromFile();
            }

            var step = new Point(coordinateX, coordinateY);
            Manager.FigureList[figureId - 1].Move(step);
            Manager.SaveToFile();

            return Manager.FigureList[figureId - 1];
        }

        [HttpPatch("RotateFigure/{figureId}")]
        public Figure RotateFigure(int figureId, double rotationDegree)
        {
            if (!Manager.FigureList.Any())
            {
                Manager.InitializeFromFile();
            }

            Manager.FigureList[figureId - 1].RotateFigure(rotationDegree);
            Manager.SaveToFile();

            return Manager.FigureList[figureId - 1];
        }

        [HttpPatch("ScaleFigure/{figureId}")]
        public Figure ScaleFigure(int figureId, double scaleMultiplayer)
        {
            if (!Manager.FigureList.Any())
            {
                Manager.InitializeFromFile();
            }

            Manager.FigureList[figureId - 1].ScaleFigure(scaleMultiplayer);
            Manager.SaveToFile();

            return Manager.FigureList[figureId - 1];
        }
    }
}
