namespace CN88.Accounts.Site.Features.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SingleSelectionViewModel
    {
        public SingleSelectionViewModel(
            IEnumerable<SelectListItem> items,
            int selectedId)
        {
            Items = items;
            SelectedId = selectedId;
        }

        public IEnumerable<SelectListItem> Items { get; }

        public int SelectedId { get; }
    }
}
