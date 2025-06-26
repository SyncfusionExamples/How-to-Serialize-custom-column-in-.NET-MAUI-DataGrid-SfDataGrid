using Syncfusion.Maui.DataGrid;
using static SfDataGridSample.TextImageColumn;
using System.Globalization;
using System.Reflection;
using System.Runtime.Serialization;

namespace SfDataGridSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            dataGrid.CellRenderers.Add("TextImage", new TextImageColumnRenderer());
            dataGrid1.CellRenderers.Add("TextImage", new TextImageColumnRenderer());
            dataGrid.SerializationController = new SerializationControllerCustomExt(dataGrid);
            dataGrid1.SerializationController = new SerializationControllerCustomExt(dataGrid1);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string localPath = Path.Combine(FileSystem.AppDataDirectory, "DataGrid.xml");
            using (var file = File.Create(localPath))
            {
                dataGrid.Serialize(file);
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            string localPath = Path.Combine(FileSystem.AppDataDirectory, "DataGrid.xml");

            using (var file = File.Open(localPath, FileMode.Open))
            {
                dataGrid1.Deserialize(file);
            }
        }
    }

    [DataContract(Name = "SerializableCustomGridColumn")]
    public class SerializableCustomGridColumn : SerializableDataGridColumn
    {
        [DataMember]
        public string? DateMappingName { get; set; }
    }

    public class SerializationControllerCustomExt : DataGridSerializationController
    {
        public SerializationControllerCustomExt(SfDataGrid sfGrid) : base(sfGrid) { }

        protected override SerializableDataGridColumn GetSerializableColumn(DataGridColumn column)
        {
            if (column is TextImageColumn)
            {
                return new SerializableCustomGridColumn();
            }
            return base.GetSerializableColumn(column);
        }

        protected override void StoreColumnProperties(DataGridColumn column, SerializableDataGridColumn serializableColumn)
        {
            base.StoreColumnProperties(column, serializableColumn);

            if (column is TextImageColumn textImageColumn && serializableColumn is SerializableCustomGridColumn customColumn)
            {
                customColumn.DateMappingName = textImageColumn.MappingName;
            }
        }

        protected override DataGridColumn GetColumn(SerializableDataGridColumn serializableColumn)
        {
            if (serializableColumn is SerializableCustomGridColumn)
            {
                return new TextImageColumn();
            }
            return base.GetColumn(serializableColumn);
        }

        protected override void RestoreColumnProperties(SerializableDataGridColumn serializableColumn, DataGridColumn column)
        {
            base.RestoreColumnProperties(serializableColumn, column);

            if (column is TextImageColumn textImageColumn && serializableColumn is SerializableCustomGridColumn customColumn)
            {
                if (!string.IsNullOrEmpty(customColumn.DateMappingName))
                    textImageColumn.MappingName = customColumn.DateMappingName;
            }
        }

        public override Type[] KnownTypes()
        {
            var types = base.KnownTypes().ToList();
            types.Add(typeof(SerializableCustomGridColumn));
            return types.ToArray();
        }

    }

}