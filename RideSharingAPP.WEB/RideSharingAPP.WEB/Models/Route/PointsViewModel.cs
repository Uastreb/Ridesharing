using System.ComponentModel.DataAnnotations;

namespace RideSharingAPP.WEB.Models.Route
{
    public class PointsViewModel
    {
        [Display(Name = "Точка отправления")]
        public string OriginCoordinates { get; set; }
        
        [Display(Name ="Точка прибытия")]
        public string EndCoordinates { get; set; }
    }
}