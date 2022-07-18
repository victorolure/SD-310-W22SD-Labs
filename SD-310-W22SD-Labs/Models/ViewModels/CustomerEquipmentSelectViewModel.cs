using Microsoft.AspNetCore.Mvc.Rendering;

namespace SD_310_W22SD_Labs.Models.ViewModels
{
    public class CustomerEquipmentSelectViewModel
    {
        public List<SelectListItem>CustomerSelect { get; set; }
        public List<SelectListItem> EquipmentSelect { get; set; }

        public CustomerEquipmentSelectViewModel(List<Customer> customers, List<Equipment> equipments)
        {
            CustomerSelect = new List<SelectListItem>();
            EquipmentSelect = new List<SelectListItem>();
            customers.ForEach(c => CustomerSelect.Add(new SelectListItem(c.UserName, c.Id.ToString())));
            equipments.ForEach(e => EquipmentSelect.Add(new SelectListItem(e.Name, e.Id.ToString())));
        }
    }
}
