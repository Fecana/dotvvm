using System;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace DotVVM.Samples.BasicSamples.ViewModels.ControlSamples.GridView
{
    public class GridViewSizableViewModel : DotvvmViewModelBase
    {

        public int? SelectedRowId { get; set; }

        private static IQueryable<CustomerData> GetData()
        {
            return new[]
            {
                new CustomerData() { CustomerId = 1, Name = "John Doe", BirthDate = DateTime.Parse("1976-04-01") },
                new CustomerData() { CustomerId = 2, Name = "John Deer", BirthDate = DateTime.Parse("1984-03-02") },
                new CustomerData() { CustomerId = 3, Name = "Johnny Walker", BirthDate = DateTime.Parse("1934-01-03") },
                new CustomerData() { CustomerId = 4, Name = "Jim Hacker", BirthDate = DateTime.Parse("1912-11-04") },
                new CustomerData() { CustomerId = 5, Name = "Joe E. Brown", BirthDate = DateTime.Parse("1947-09-05") },

            }.AsQueryable();
        }


        public GridViewSizableViewModel()
        {
            CustomersDataSet = new GridViewDataSet<CustomerData>() { PageSize = 10 };
        }

        public GridViewDataSet<CustomerData> CustomersDataSet { get; set; }

        public override Task PreRender()
        {
            // fill dataset
            if (!Context.IsPostBack)
            {
                CustomersDataSet.LoadFromQueryable(GetData());
            }
            return base.PreRender();
        }

        public void SelectCustomer(int id)
        {
            SelectedRowId = id;
        }

    }
}